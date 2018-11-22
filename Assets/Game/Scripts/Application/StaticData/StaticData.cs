using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterInfo
{
    public int ID;
    public int MaxHp;
    public float MoveSpeed;
    public int Price;
    public int damage;
}
public class BulletInfo
{
    public int ID;
    public string PrefabName;
    public float BaseSpeed; //基本速度
    public int BaseDamage;  //基本攻击力
}
public class TowerInfo
{
    public int ID;
    public string PrefabName;
    public int MaxLevel;
    public int BasePrice;
    public float ShotRate;
    public float GuardRange;
    public int UseBulletID;
    public int MaxHp;
}
public class GunInfo
{
    public int ID;
    public string PrefabName;
    public float ShootRate;
    public float ShootingDistance;
}
public class BoxInfo
{
    public int ID;
    public int MaxHp;
}
public class StaticData : Singleton<StaticData>
{
    Dictionary<int, MonsterInfo> m_Monsters = new Dictionary<int, MonsterInfo>();
    Dictionary<int, TowerInfo> m_Towers = new Dictionary<int, TowerInfo>();
    Dictionary<int, BulletInfo> m_Bullets = new Dictionary<int, BulletInfo>();
    Dictionary<int, GunInfo> m_Guns = new Dictionary<int, GunInfo>();
    Dictionary<int, BoxInfo> m_Box = new Dictionary<int, BoxInfo>();
    protected override void Awake()
    {
        base.Awake();
        InitMonsters();
        InitTowers();
        InitBullets();
        InitGuns();
        InitMess();
    }

    void InitMonsters()
    {
        m_Monsters.Add(0, new MonsterInfo() { ID = 0, MaxHp = 90, MoveSpeed = 1f, Price = 2, damage = 15 });
        m_Monsters.Add(1, new MonsterInfo() { ID = 1, MaxHp = 40, MoveSpeed = 1f, Price = 2, damage = 5 });
        //m_Monsters.Add(2, new MonsterInfo() { ID = 2, MaxHp = 15, MoveSpeed = 2f, Price = 5 });
        //m_Monsters.Add(3, new MonsterInfo() { ID = 3, MaxHp = 20, MoveSpeed = 2f, Price = 10 });
        // m_Monsters.Add(4, new MonsterInfo() { ID = 4, MaxHp = 20, MoveSpeed = 2f, Price = 15 });
        // m_Monsters.Add(5, new MonsterInfo() { ID = 5, MaxHp = 100, MoveSpeed = 0.5f, Price = 20 });
    }
    void InitMess()
    {
        m_Box.Add(0, new BoxInfo() { ID = 0, MaxHp = 12 });
        m_Box.Add(1, new BoxInfo() { ID = 1, MaxHp = 10 });
        m_Box.Add(2, new BoxInfo() { ID = 2, MaxHp = 5 });
    }
    void InitTowers()
    {
        m_Towers.Add(0, new TowerInfo() { ID = 0, PrefabName = "Tower0", MaxLevel = 3, BasePrice = 15, ShotRate = 4, GuardRange = 3f, UseBulletID = 0, MaxHp = 50 });
        // m_Towers.Add(1, new TowerInfo() { ID = 1, PrefabName = "Tower1", MaxLevel = 3, BasePrice = 2, ShotRate = 0.3f, GuardRange = 3f, UseBulletID = 1, MaxHp = 50 });
    }

    void InitBullets()
    {
        m_Bullets.Add(0, new BulletInfo() { ID = 0, PrefabName = "Tower0Bullet", BaseDamage = 5, BaseSpeed = 6 });
        m_Bullets.Add(1, new BulletInfo() { ID = 1, PrefabName = "Tower1Bullet", BaseDamage = 1, BaseSpeed = 2 });
        m_Bullets.Add(2, new BulletInfo() { ID = 2, PrefabName = "YM3Bullet", BaseDamage = 3, BaseSpeed = 5 });
        m_Bullets.Add(3, new BulletInfo() { ID = 3, PrefabName = "Rocket01_Red", BaseDamage = 5, BaseSpeed = 5 });
    }
    void InitGuns()
    {
        m_Guns.Add(0, new GunInfo() { ID = 0, PrefabName = "Empty", ShootRate = 3, ShootingDistance = 1f });
        m_Guns.Add(1, new GunInfo() { ID = 1, PrefabName = "SciFiPistol_YM-3", ShootRate = 3, ShootingDistance = 5f });
        m_Guns.Add(2, new GunInfo() { ID = 2, PrefabName = "SciFiPistol_YM-3S", ShootRate = 3, ShootingDistance = 5f });
        m_Guns.Add(3, new GunInfo() { ID = 3, PrefabName = "SciFiRifle_YM-27", ShootRate = 10, ShootingDistance = 7f });
    }
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
    public GunInfo GetGunInfo(int GunID)
    {
        return m_Guns[GunID];
    }
    public BoxInfo GetBoxInfo(int BoxID)
    {
        return m_Box[BoxID];
    }
}