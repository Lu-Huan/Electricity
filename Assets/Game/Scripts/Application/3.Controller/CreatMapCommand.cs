using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CreatMapCommand : Controller
{
    public override void Execute(object data)
    {
        Debug.Log("创建新地图");
        MapModel mapModel = GetModel<MapModel>();
        mapModel.LoadMap(30, 50);
        mapModel.Draw();
    }
}

