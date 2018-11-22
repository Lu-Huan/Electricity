using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CountDownCompleteCommand : Controller
{
    public override void Execute(object data)
    {
        Debug.Log("开始出怪");
        //开始出怪
        MapModel mapModel = GetModel<MapModel>();
        
        RoundModel rModel = GetModel<RoundModel>();
        rModel.InitEnd(mapModel.end.GetComponent<Electricity>());
        rModel.StartRound();
    }
}