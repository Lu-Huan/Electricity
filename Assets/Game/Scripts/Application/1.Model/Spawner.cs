using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Model
{
    public MapModel mapM;
    public List<Monster> monsters=new List<Monster>();
    

    public override string Name
    {
        get
        {
            return Consts.V_Spanwner;
        }
    }

    public void SpawnMonster(int MonsterID)
    {
        string prefabName = "Monster" + MonsterID;
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);


        GameObject Hp = Game.Instance.ObjectPool.Spawn("Hp");
        Hp.GetComponent<UI_HP>().Init(go.transform);
        Hp.transform.SetParent(GameObject.Find("Canvas").transform);



        Monster Mo = go.GetComponent<Monster>();
        Mo.Reached += monster_Reached;
        Mo.HpChanged += monster_HpChanged;
        Mo.Dead += monster_Dead;
        Mo.Load(mapM.GetMapPath);
        monsters.Add(Mo);
    }

    private void monster_HpChanged(int arg1, int arg2)
    {

    }

    void monster_Reached(Monster monster)
    {
        //掉血
        SendEvent(Consts.E_Damage, monster);
        Debug.Log(monster.name + "敌人到了终点");

        //怪物死亡
        monster.Hp = 0;
    }

    void monster_Dead(Role Ro)
    {
        //怪物回收
        Monster Mo = Ro.GetComponent<Monster>();
        Game.Instance.ObjectPool.Unspawn(Ro.gameObject);
        SendEvent(Consts.E_MonsterDead, Mo);
        monsters.Remove(Mo);
        if (monsters.Count==0)
        {
            Debug.Log("运行下一个关卡");
            SendEvent(Consts.E_NextRound);
        }
    }

    public void SpawnTower(int TowerID, Vector3 pos)
    {
        string prefabName = "Tower" + TowerID;
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);
        go.transform.position = pos;

        Tower tower = go.GetComponent<Tower>();
        tower.Load(TowerID);
        //SendEvent(Consts.E_CompleteSpawnTower, tower);//发送完成创建的消息

        GameObject Hp = Game.Instance.ObjectPool.Spawn("Hp");
        Hp.GetComponent<UI_HP>().Init(go.transform);
        Hp.transform.SetParent(GameObject.Find("Canvas").transform);

        Tile tile = mapM.GetTile(pos);
        tile.Data = go;
        Debug.Log("建造了一座塔:" + go.name);
    }
          
}
