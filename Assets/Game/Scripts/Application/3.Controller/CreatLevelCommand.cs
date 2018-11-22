using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CreatLevelCommand : Controller
{
    public override void Execute(object data)
    {
        //StartLevelArgs e = data as StartLevelArgs;

        //第一步


        //第二步
        RoundModel rModel = GetModel<RoundModel>();
        Debug.Log("我创建了回合");
        rModel.ClearRound();
        //rModel.LoadLevel(gModel.PlayLevel);
        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(0,1)
        });

       /* rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(1,1)
        });

        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(1,1)
        });

        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(0,1),
            new MonsterGroup(0,1),
            new MonsterGroup(1,1)
        });

        rModel.CreateRound(new List<MonsterGroup> {
            new MonsterGroup(0,10),
            new MonsterGroup(0,10),
            new MonsterGroup(1,10),
            new MonsterGroup(1,10),
            new MonsterGroup(0,10),
        });*/
    }
}