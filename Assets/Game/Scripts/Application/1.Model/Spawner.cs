using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Model
{
    public MapModel mapM;
    public List<Monster> ListMonsters=new List<Monster>();
    public List<Tower> ListTowers = new List<Tower>();
    public int[] Already_dead=new int [2];


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

        Monster Mo = go.GetComponent<Monster>();
        Mo.Reached += monster_Reached;
        Mo.HpChanged += monster_HpChanged;
        Mo.Dead += monster_Dead;
        Mo.Load(mapM.GetMapPath);
        ListMonsters.Add(Mo);
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
        if (Ro.GetComponent<Monster0>()==null)
        {
            Already_dead[1]++;
        }
        else
        {
            Already_dead[0]++;
        }
        Monster Mo = Ro.GetComponent<Monster>();
        SendEvent(Consts.E_MonsterDead, Mo);
        ListMonsters.Remove(Mo);
        if (ListMonsters.Count==0)
        {
            SendEvent(Consts.E_NextRound);
        }
    }

    public void SpawnTower(int TowerID, Vector3 pos)
    {
        string prefabName = "Tower" + TowerID;
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);
        go.transform.position = new Vector3(pos.x, go.transform.position.y, pos.z);

        Tower tower = go.GetComponent<Tower>();
        tower.Load(TowerID);
        ListTowers.Add(tower);
        tower.Dead += Tower_Dead;
        //SendEvent(Consts.E_CompleteSpawnTower, tower);//发送完成创建的消息



        Tile tile = mapM.GetTile(pos);
        tile.Data = go;
        Debug.Log("建造了一座塔:" + go.name);
    }

    private void Tower_Dead(Role obj)
    {
        obj.GetComponent<Tower>();

    }
}
