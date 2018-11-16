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
        Debug.Log("我创建了回合");
        //rModel.LoadLevel(gModel.PlayLevel);
        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(0,2),
            new MonsterGroup(1,5),
            new MonsterGroup(0,3)
        });

        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(0,3),
            new MonsterGroup(1,6),
            new MonsterGroup(0,9)
        });

        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(1,1),
            new MonsterGroup(1,3),
            new MonsterGroup(0,10)
        });

        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(1,1),
            new MonsterGroup(1,3),
            new MonsterGroup(0,20)
        });

        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(0,10),
            new MonsterGroup(0,10),
            new MonsterGroup(1,10),
            new MonsterGroup(1,10),
            new MonsterGroup(0,10),
            new MonsterGroup(0,10),
            new MonsterGroup(0,10),
            new MonsterGroup(0,10)
        });
    }
}