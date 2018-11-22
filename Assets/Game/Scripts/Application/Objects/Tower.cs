using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Tower : Role
{
    public int ID { get; private set; }
    public Transform ShootPoint;
    public float ShotRate { get; private set; }
    public float GuardRange { get; private set; }
    public int BasePrice { get; private set; }
    public string UseBulletName { get; private set; }

    public Tile Tile { get; private set; }
    public List<Monster> monsters = new List<Monster>();
    public Monster m_Target = null;
    public Monster m_BeSelect=null;
    // Animator m_Animator;
    //int m_Level;
    float m_LastShotTime = 0;
    protected void MonstersAdd(Monster mo)
    {
        monsters.Add(mo);
        mo.Dead += Mo_Dead;
        if (m_BeSelect==mo)
        {
            m_Target = m_BeSelect;
        }
        else if (m_Target == null)
        {
            m_Target = monsters[0];
        }
    }
    public void HaveTarge(Monster monster)
    {
        if (monsters.Contains(monster))
        {
            m_Target = monster;
        }
        else
        {
            m_BeSelect = monster;
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
        BulletInfo bullteinfo = Game.Instance.StaticData.GetBulletInfo(info.UseBulletID);
        UseBulletName = bullteinfo.PrefabName;
        MaxHp = info.MaxHp;
        Hp = MaxHp;

        GetHPBar();
    }



    public virtual void Shot(Monster monster)
    {
        GameObject go= Game.Instance.ObjectPool.Spawn(UseBulletName);
        go.transform.position = ShootPoint.position;
        go.GetComponent<Tower0Bullet>().Load(0, 1, monster,this);
    }

    protected virtual void LookAt(Monster target) { }

    public override void OnDead(Role role)
    {
        //base.OnDead(role);
        OnUnspawn();
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        m_Target = null;
        m_LastShotTime = 0;
        ShotRate = 0;
        BasePrice = 0;
        //gameObject.SetActive(false);

    }

}