using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class StartLevelCommand : Controller
{
    public override void Execute(object data)
    {
        //StartLevelArgs e = data as StartLevelArgs;

        //第一步
        /*GameModel gModel = GetModel<GameModel>();
        gModel.StartLevel(e.LevelIndex);*/

        //第二步
        RoundModel rModel = GetModel<RoundModel>();
        Debug.Log("我创建了两个回合");
        //rModel.LoadLevel(gModel.PlayLevel);
        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(0,2),
            new MonsterGroup(1,2),
            new MonsterGroup(0,3)
        });

        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(0,2),
            new MonsterGroup(1,2),
            new MonsterGroup(0,3)
        });

    }
}