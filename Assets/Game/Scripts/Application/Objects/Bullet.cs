using UnityEngine;
using System.Collections;

public abstract class Bullet : ReusbleObject, IReusable
{
    //类型
    public int ID { get;  set; }
    //等级
    public int Level { get; set; }
    //基本速度
    public float BaseSpeed { get;  set; }
    //基本攻击力
    public int BaseAttack { get;  set; }

    //移动速度
    public float Speed { get { return BaseSpeed * Level; } }
    //攻击力
    public int Demage { get { return BaseAttack * Level; } }
    //延迟回收时间(秒)
    public float DelayToDestory = 0.3f;

    //是否爆炸
    protected bool m_IsExploded = false;

    //动画组件
    Animator m_Animator;


    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }

    public virtual void Load(int bulletID, int level, Monster monster)
    {

    }

    public virtual void Explode()
    {
        //标记已爆炸
        m_IsExploded = true;
        StartCoroutine(DestoryCoroutine());
    }

    IEnumerator DestoryCoroutine()
    {
        //延迟
        yield return new WaitForSeconds(DelayToDestory);

        //回收
        Game.Instance.ObjectPool.Unspawn(gameObject);
    }

    public override void OnSpawn()
    {

    }

    public override void OnUnspawn()
    {
        m_IsExploded = false;
    }
}
