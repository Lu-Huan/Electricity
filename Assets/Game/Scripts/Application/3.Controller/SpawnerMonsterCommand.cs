using UnityEngine;
using UnityEditor;

public class SpawnerMonsterCommand : Controller
{
    public override void Execute(object data)
    {
        Spawner spawner = GetModel<Spawner>();
        SpawnMonsterArgs c = data as SpawnMonsterArgs;

        spawner.SpawnMonster(c.MonsterID);
    }
}