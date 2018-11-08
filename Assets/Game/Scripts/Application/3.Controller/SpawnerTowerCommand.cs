using UnityEngine;
using UnityEditor;

public class SpawnerTowerCommand : Controller
{
    public override void Execute(object data)
    {
        Spawner spawner = GetModel<Spawner>();

        SpawnTowerArgs a = data as SpawnTowerArgs;
        spawner.SpawnTower(a.TowerID, a.Position);
    }
}