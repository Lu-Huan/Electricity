using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class CreatMapCommand : Controller
{
    public override void Execute(object data)
    {
        Debug.Log("创建新地图");
        MapModel mapModel = GetModel<MapModel>();
        mapModel.LoadMap(30, 50);
        int scene = SceneManager.GetActiveScene().buildIndex;
        if (scene == 3)
        {
            mapModel.Draw();
        }
    }
}

