using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundModel : Model
{

    #region 常量
    public const float ROUND_INTERVAL = 3f; //回合间隔时间
    public const float SPAWN_INTERVAL = 1f; //出怪间隔时间
    #endregion

    #region 事件
    #endregion

    #region 字段
    List<Round> m_Rounds = new List<Round>();//该关卡所有的出怪信息
    int m_RoundIndex = 0;             //当前回合的索引
    Coroutine m_Coroutine;
    public Electricity End;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.M_RoundModel; }
    }

    public int RoundIndex
    {
        get { return m_RoundIndex; }
    }

    public int RoundTotal
    {
        get { return m_Rounds.Count; }
    }
    #endregion

    #region 方法
    public void InitEnd(Electricity end)
    {
        End = end;
        End.Hp = 100;
        End.Dead += End_Dead;
    }
    public void ClearRound()
    {
        m_Rounds.Clear();
        m_RoundIndex = 0;
    }
    private void End_Dead(Role obj)
    {
        EndLevelArgs endLevelArgs = new EndLevelArgs
        {
            IsDead = false,
            IsSuccess = false
        };
        SendEvent(Consts.E_EndLevel,endLevelArgs);
    }

    /// <summary>
    /// 开始一个回合
    /// </summary>
    public void StartRound()
    {
        if (m_RoundIndex == m_Rounds.Count)
        {
            EndLevelArgs endLevelArgs = new EndLevelArgs
            {
                IsDead = false,
                IsSuccess = true
            };
            Debug.Log("关卡结束");
            SendEvent(Consts.E_EndLevel,endLevelArgs);
        }
        else
        {
            m_Coroutine = Game.Instance.StartCoroutine(RunRound());
        }
    }
    /// <summary>
    /// 用于停止协程
    /// </summary>
    public void StopRound()
    {
        Game.Instance.StopCoroutine(m_Coroutine);
    }
    int i=0;
    /// <summary>
    /// 创建一个回合
    /// </summary>
    /// <param name="monsterGroups">一个MonsterGroup的集合</param>
    public void CreateRound(List<MonsterGroup> monsterGroups)
    {
        i++;
        Debug.Log("建立回合"+i);
        Round NewRound = new Round(monsterGroups);
        m_Rounds.Add(NewRound);
    }
    /// <summary>
    /// 协程:运行一个回合
    /// </summary>
    /// <returns></returns>
    IEnumerator RunRound()
    {
        //回合开始
        yield return new WaitForSeconds(ROUND_INTERVAL);
        StartRoundArgs e = new StartRoundArgs
        {
            RoundIndex = m_RoundIndex,
            RoundTotal = RoundTotal,
            MonsterTotal = m_Rounds[m_RoundIndex].TotalMonster
        };
        Debug.Log("回合开始");
        SendEvent(Consts.E_StartRound, e);//发送一个回合开始的事件

        List<MonsterGroup> round = m_Rounds[m_RoundIndex].TheRound;
        SpawnMonsterArgs x = new SpawnMonsterArgs();
        for (int k = 0; k < round.Count; k++)
        {
            MonsterGroup group = round[k];

            SendEvent(Consts.E_SpawnMonsterGroups, group);//发送一个显示怪物群的消息
            for (int j = 0; j < group.count; j++)
            {
                //出怪间隙
                yield return new WaitForSeconds(SPAWN_INTERVAL);
                //出怪事件
                x.MonsterID = group.monsterID;
                //Debug.Log("生成怪物"+ group.monsterID);
                if (x == null)
                {
                    Debug.Log("x无效");
                }
                else
                {
                    SendEvent(Consts.E_SpawnMonster, x); //发送一个生成怪物的命令
                }
            }
        }
        m_RoundIndex++;
    }
    #endregion
}
