using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CountDownCompleteCommand : Controller
{
    public override void Execute(object data)
    {
        //开始出怪
        MapModel mapModel = GetModel<MapModel>();
        
        RoundModel rModel = GetModel<RoundModel>();
        rModel.End = mapModel.end.GetComponent<Electricity>();
        rModel.StartRound();
    }
}