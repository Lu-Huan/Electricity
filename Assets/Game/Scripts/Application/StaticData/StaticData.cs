﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Text.RegularExpressions;
using System;

# region 数据类
public class Arsenal
{
    public string name;
    public GameObject rightGun;
    public GameObject leftGun;
    public RuntimeAnimatorController controller;
}
class Person
{
    public List<Character> Persons = new List<Character>();
}
class Bag_Gun
{
    public List<HaveGun> haveGuns = new List<HaveGun>();
}
public class HaveGun
{
    public int ID;
    public bool Have;
    public bool InBag;
}
public class Character
{
    public string Name;
    public int[] EquipGunId;
    public string Imformation;
}

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

    public GunType GunType;
}
public class BoxInfo
{
    public int ID;
    public int MaxHp;
}
#endregion

public class StaticData : Singleton<StaticData>
{
    #region 全局游戏数据
    Dictionary<int, MonsterInfo> m_Monsters = new Dictionary<int, MonsterInfo>();
    Dictionary<int, TowerInfo> m_Towers = new Dictionary<int, TowerInfo>();
    Dictionary<int, BulletInfo> m_Bullets = new Dictionary<int, BulletInfo>();
    List<GunInfo> m_Guns = new List<GunInfo>();
    Dictionary<int, BoxInfo> m_Box = new Dictionary<int, BoxInfo>();
    //在资源文件夹里
    public RuntimeAnimatorController[] Controllers { private set; get; }//角色控制器
    //public Dictionary<string, GameObject> GunPrefabs { private set; get; }// 枪的预制
    //存在Json数据中
    private List<Character> m_people = new List<Character>();
    private List<HaveGun> m_haveGun = new List<HaveGun>();
    #endregion
    public List<HaveGun> HaveGun
    {
        get
        {
            return m_haveGun;
        }
        set
        {
            m_haveGun = value;

        }
    }
    public void SaveHaveGun()
    {
        Bag_Gun bag_Gun = new Bag_Gun
        {
            haveGuns = HaveGun
        };

        Debug.Log("写入枪的数据");
        string GunData = JsonMapper.ToJson(bag_Gun);

        Saver.WriteJsonString(GunData, Saver.GunDataPath);
    }
    public void SavePeople()
    {

        Person person = new Person
        {
            Persons = People
        };
        string Data = JsonMapper.ToJson(person);//数据转化为字符串
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        var ss = reg.Replace(Data, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

        Saver.WriteJsonString(ss, Saver.CharacterDataPath);//写入
        Debug.Log("写入人物数据");
    }
    public List<Character> People
    {
        get
        {
            return m_people;
        }
        set
        {
            m_people = value;

        }
    }
    protected override void Awake()
    {
        base.Awake();
        InitMonsters();
        InitTowers();
        InitBullets();
        InitGuns();
        InitMess();
        PlayerPrefs.DeleteAll();
        //是否第一次加载
        if (PlayerPrefs.GetInt("IsFrist", -1) == -1)
        {
            //拥有的人物
            PlayerPrefs.SetInt("Char" + 0, 1);
            PlayerPrefs.SetInt("Char" + 1, 0);
            PlayerPrefs.SetInt("Char" + 2, 0);
            PlayerPrefs.SetInt("Char" + 3, 0);
            //当前的角色    
            PlayerPrefs.SetInt("CurrentChar", 0);
            //能量
            PlayerPrefs.SetInt("ElectricEnergy", 10000);

            PlayerPrefs.SetInt("IsFrist", 0);
            InitPeople();
            InitHaveGun();
        }




        ReadPeopleData();
        ReadGunData();
        //加载所有控制器
        string path = "Controllers";
        Controllers = Resources.LoadAll<RuntimeAnimatorController>(path);

        /*path = "Prefabs/Guns";
        GameObject[] GunPrefabAll = Resources.LoadAll<GameObject>(path);

        foreach (GameObject item in GunPrefabAll)
        {
            Game.Instance.ObjectPool.
        }*/
    }
    #region 初始化游戏数据
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
        m_Towers.Add(1, new TowerInfo() { ID = 1, PrefabName = "Tower1", MaxLevel = 3, BasePrice = 2, ShotRate = 8f, GuardRange = 3f, UseBulletID = 1, MaxHp = 50 });
        m_Towers.Add(2, new TowerInfo() { ID = 2, PrefabName = "Tower2", MaxLevel = 3, BasePrice = 2, ShotRate = 8f, GuardRange = 3f, UseBulletID = 3, MaxHp = 50 });
    }

    void InitBullets()
    {
        m_Bullets.Add(0, new BulletInfo() { ID = 0, PrefabName = "Tower0Bullet", BaseDamage = 5, BaseSpeed = 6 });
        m_Bullets.Add(1, new BulletInfo() { ID = 1, PrefabName = "Tower0Bullet", BaseDamage = 4, BaseSpeed = 9 });
        m_Bullets.Add(2, new BulletInfo() { ID = 2, PrefabName = "YM3Bullet", BaseDamage = 3, BaseSpeed = 5 });
        m_Bullets.Add(3, new BulletInfo() { ID = 3, PrefabName = "Tower2Bullet", BaseDamage = 10, BaseSpeed = UnityEngine.Random.Range(3.5f, 4.5f) });
    }
    void InitGuns()
    {
        m_Guns.Add(new GunInfo() { ID = 0, PrefabName = "Empty", ShootRate = 3, ShootingDistance = 1f, GunType = GunType.FreeHand });
        m_Guns.Add(new GunInfo() { ID = 1, PrefabName = "Pistol_YM3", ShootRate = 3, ShootingDistance = 5f, GunType = GunType.OnePistol });
        m_Guns.Add(new GunInfo() { ID = 2, PrefabName = "Pistol_YM3S", ShootRate = 3, ShootingDistance = 5f, GunType = GunType.TwoPistol });
        m_Guns.Add(new GunInfo() { ID = 3, PrefabName = "Rifle_YM27", ShootRate = 10, ShootingDistance = 7f, GunType = GunType.Rifle });
        m_Guns.Add(new GunInfo() { ID = 4, PrefabName = "AssaultRifle", ShootRate = 8, ShootingDistance = 7f, GunType = GunType.Rifle });
        m_Guns.Add(new GunInfo() { ID = 5, PrefabName = "SilencedPistol", ShootRate = 7, ShootingDistance = 7f, GunType = GunType.OnePistol });
        m_Guns.Add(new GunInfo() { ID = 6, PrefabName = "SilencedPistolS", ShootRate = 7, ShootingDistance = 7f, GunType = GunType.TwoPistol });
        m_Guns.Add(new GunInfo() { ID = 7, PrefabName = "SniperGun", ShootRate = 1, ShootingDistance = 10f, GunType = GunType.Rifle });
        m_Guns.Add(new GunInfo() { ID = 8, PrefabName = "SubmachineGun", ShootRate = 11, ShootingDistance = 7f, GunType = GunType.OnePistol });
        m_Guns.Add(new GunInfo() { ID = 9, PrefabName = "SubmachineGunS", ShootRate = 11, ShootingDistance = 7f, GunType = GunType.TwoPistol });
    }
    void InitHaveGun()
    {
        Bag_Gun bag_Gun = new Bag_Gun();
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 0, Have = true, InBag = true });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 1, Have = false, InBag = false });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 2, Have = false, InBag = false });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 3, Have = false, InBag = false });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 4, Have = false, InBag = false });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 5, Have = false, InBag = false });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 6, Have = false, InBag = false });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 7, Have = false, InBag = false });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 8, Have = false, InBag = false });
        bag_Gun.haveGuns.Add(new HaveGun() { ID = 9, Have = false, InBag = false });

        string GunData = JsonMapper.ToJson(bag_Gun);

        Saver.WriteJsonString(GunData, Saver.GunDataPath);
    }

    void ReadGunData()
    {
        string GunData = Saver.ReadJsonString(Saver.GunDataPath);
        Bag_Gun bag_Gun = JsonMapper.ToObject<Bag_Gun>(GunData);
        m_haveGun = bag_Gun.haveGuns;
    }
    void InitPeople()
    {
        Person person = new Person();
        person.Persons.Add(new Character()
        {
            Name = "零",
            EquipGunId = new int[] { 0, -1, -1, -1 },
            Imformation = "机械少女"
        });
        person.Persons.Add(new Character()
        {
            Name = "龙峻",
            EquipGunId = new int[] { 0, -1, -1, -1 },
            Imformation = "冷静的工程师"
        });
        person.Persons.Add(new Character()
        {
            Name = "收割者",
            EquipGunId = new int[] { 0, -1, -1, -1 },
            Imformation = "摘下面具，那么他是？"
        });
        person.Persons.Add(new Character()
        {
            Name = "？",
            EquipGunId = new int[] { 0, -1, -1, -1 },
            Imformation = "？？？"
        });

        //string Data= JsonUtility.ToJson(new Person() { Persons=m_people});
        string Data = JsonMapper.ToJson(person);//数据转化为字符串
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        var ss = reg.Replace(Data, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

        Saver.WriteJsonString(ss, Saver.CharacterDataPath);//写入
    }
    void ReadPeopleData()
    {
        string Data = Saver.ReadJsonString(Saver.CharacterDataPath);//读取字符串
        Person person = JsonMapper.ToObject<Person>(Data);//字符串转化为数据
        m_people = person.Persons;
    }
    #endregion


    #region 获取数据的函数接口
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
    public Character GetCharacter(int CharacterID)
    {
        return m_people[CharacterID];
    }
    #endregion

}