using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Monster : Role
{
    #region 常量
    public const float CLOSED_DISTANCE = 0.1f;
    #endregion

    #region 事件
    public event Action<Monster> Reached;
    #endregion

    #region 字段
    public MonsterType MonsterType = MonsterType.Monster0;//怪物类型    
    public Vector3 Orientation;
    public float m_MoveSpeed = 2;//移动速度（米/秒）
    List<Vector3> m_Path = null; //路径
    int m_PointIndex = 0; //当前索引
    bool m_IsReached = false;//是否到达终点
    public int damage;
    public int price;
    #endregion

    #region 属性
    public float MoveSpeed
    {
        get { return m_MoveSpeed; }
        set { m_MoveSpeed = value; }
    }
    #endregion

    #region 方法
    public void Load(List<Vector3> path)
    {
        m_Path = path;
        MoveNext();
        MonsterInfo info = Game.Instance.StaticData.GetMonsterInfo((int)MonsterType);
        MaxHp = info.MaxHp;
        Hp = info.MaxHp;
        MoveSpeed = info.MoveSpeed;
        damage = info.damage;
        price = info.Price;
    }
    void MoveTo(Vector3 position)
    {
        transform.position = new Vector3(position.x, transform.position.y, position.z);
    }

    void MoveNext()
    {
        MoveTo(m_Path[m_PointIndex]);
        m_PointIndex++;
        if (m_PointIndex == m_Path.Count)
        {
            //到达终点
            m_IsReached = true;

            //触发到达终点事件
            if (Reached != null)
                Reached(this);
        }
        else
        {
            Vector3 dir = m_Path[m_PointIndex] - m_Path[m_PointIndex - 1];
            float angle = Vector3.Angle(Orientation, dir); //求出两向量之间的夹角
            Vector3 normal = Vector3.Cross(Orientation, dir);//叉乘求出法线向量  

            //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向 
            angle *= Mathf.Sign(Vector3.Dot(normal, new Vector3(0, 1, 0)));

            transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }
    #endregion

    #region Unity回调
    void Update()
    {
        //到达了终点
        if (m_IsReached)
            return;

        //当前位置
        Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
        //目标位置
        if (m_PointIndex == 0)
        {
            return;
        }
        Vector3 targe = m_Path[m_PointIndex];
        //方向
        Vector3 driection = m_Path[m_PointIndex] - m_Path[m_PointIndex - 1];

        //计算距离
        float dis = Vector3.Distance(pos, targe);
        if (dis <= CLOSED_DISTANCE)
        {
            //到达目标
            MoveNext();
        }
        else
        {
            transform.position += driection * Time.deltaTime * m_MoveSpeed;
        }
    }
    #endregion

    #region 事件回调
    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();

        m_Path = null;
        m_PointIndex = 0;
        m_IsReached = false;
        m_MoveSpeed = 0;
        Reached = null;
    }
    #endregion

    #region 帮助方法
    #endregion
}