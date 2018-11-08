using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Tower : Role
{
    public int ID { get; private set; }

    public float ShotRate { get; private set; }
    public float GuardRange { get; private set; }
    public int BasePrice { get; private set; }
    public int UseBulletID { get; private set; }

    public Tile Tile { get; private set; }
    public List<Monster> monsters = new List<Monster>();
    public Monster m_Target = null;
    // Animator m_Animator;
    //int m_Level;
    float m_LastShotTime = 0;
    protected void MonstersAdd(Monster mo)
    {
        monsters.Add(mo);
        mo.Dead += Mo_Dead;
        if (m_Target == null)
        {
            m_Target = monsters[0];
        }
    }

    private void Mo_Dead(Role obj)
    {
        Monster monster = obj.GetComponent<Monster>();
        if (monsters.Contains(monster))
        {
            MonstersRemove(monster);
        }

    }

    protected void MonstersRemove(Monster a)
    {
        monsters.Remove(a);
        if (monsters.Count == 0)
        {
            m_Target = null;
        }
        else
        {
            m_Target = monsters[0];
        }
    }
    /*protected virtual void Awake()
    {
        //m_Animator = GetComponent<Animator>();
    }*/

    void Update()
    {
        //朝向目标
        LookAt(m_Target);
        if (m_Target == null)
        {
            return;
        }


        //发射判断
        if (Time.time >= m_LastShotTime + 1f / ShotRate)
        {
            Debug.Log("发射子弹");
            //创建子弹
            Shot(m_Target);

            //记录发射时间
            m_LastShotTime = Time.time;
        }
    }

    public void Load(int towerID)
    {
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);
        ShotRate = info.ShotRate;
        BasePrice = info.BasePrice;
        GuardRange = info.GuardRange;
        UseBulletID = info.UseBulletID;
        MaxHp = info.MaxHp;
        Hp = MaxHp;
    }

    public virtual void Shot(Monster monster)
    {
        monster.Damage(10);
    }

    protected virtual void LookAt(Monster target) { }

    public override void OnSpawn()
    {

    }

    public override void OnUnspawn()
    {
        // m_Animator.ResetTrigger("IsAttack");
        // m_Animator.Play("Idle");
        m_Target = null;
        m_LastShotTime = 0;



        /*Level = 0;
        MaxLevel = 0;*/
        ShotRate = 0;
        BasePrice = 0;
    }

}