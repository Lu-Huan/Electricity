using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticData : Singleton<StaticData>
{
    //Dictionary<int, LuoboInfo> m_Luobos = new Dictionary<int, LuoboInfo>();
    Dictionary<int, MonsterInfo> m_Monsters = new Dictionary<int, MonsterInfo>();
    Dictionary<int, TowerInfo> m_Towers = new Dictionary<int, TowerInfo>();
    Dictionary<int, BulletInfo> m_Bullets = new Dictionary<int, BulletInfo>();

    protected override void Awake()
    {
        base.Awake();
        //InitLuobos();
        InitMonsters();
        InitTowers();
        InitBullets();
    }

    /*void InitLuobos()
    {
        m_Luobos.Add(0, new LuoboInfo() { ID = 0, Hp = 2 });
    }*/

    void InitMonsters()
    {
        m_Monsters.Add(0, new MonsterInfo() { ID = 0, MaxHp = 90, MoveSpeed = 1f, Price = 5, damage = 15 });
        m_Monsters.Add(1, new MonsterInfo() { ID = 1, MaxHp = 30, MoveSpeed = 1.5f, Price = 5, damage = 5 });
        m_Monsters.Add(2, new MonsterInfo() { ID = 2, MaxHp = 15, MoveSpeed = 2f, Price = 5 });
        m_Monsters.Add(3, new MonsterInfo() { ID = 3, MaxHp = 20, MoveSpeed = 2f, Price = 10 });
        m_Monsters.Add(4, new MonsterInfo() { ID = 4, MaxHp = 20, MoveSpeed = 2f, Price = 15 });
        m_Monsters.Add(5, new MonsterInfo() { ID = 5, MaxHp = 100, MoveSpeed = 0.5f, Price = 20 });
    }

    void InitTowers()
    {
        m_Towers.Add(0, new TowerInfo() { ID = 0, PrefabName = "Tower0", NormalIcon = "Bottle/Bottle01", DisabledIcon = "Bottle/Bottle00", MaxLevel = 3, BasePrice = 10, ShotRate = 2, GuardRange = 3f, UseBulletID = 0 ,MaxHp=50});
        m_Towers.Add(1, new TowerInfo() { ID = 1, PrefabName = "Tower1", NormalIcon = "Fan/Fan01", DisabledIcon = "Fan/Fan00", MaxLevel = 3, BasePrice = 2, ShotRate = 0.3f, GuardRange = 3f, UseBulletID = 1,MaxHp=50 });
    }

    void InitBullets()
    {
        m_Bullets.Add(0, new BulletInfo() { ID = 0, PrefabName = "BallBullet", BaseSpeed = 5f, BaseAttack = 1 });
        m_Bullets.Add(1, new BulletInfo() { ID = 1, PrefabName = "FanBullet", BaseSpeed = 2f, BaseAttack = 1 });
    }

    /* public LuoboInfo GetLuoboInfo()
     {
         return m_Luobos[0];
     }*/

    public MonsterInfo GetMonsterInfo(int monsterID)
    {
        return m_Monsters[monsterID];
    }

    public TowerInfo GetTowerInfo(int towerID)
    {
        return m_Towers[towerID];
    }

    public BulletInfo GetBulletInfo(int bulletID)
    {
        return m_Bullets[bulletID];
    }
}