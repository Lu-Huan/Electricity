using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

class CreatLevelCommand : Controller
{
    public override void Execute(object data)
    {
        //StartLevelArgs e = data as StartLevelArgs;

        //第一步

        GameModel gameModel = GetModel<GameModel>();
        gameModel.PushProgress();
        //第二步

        RoundModel rModel = GetModel<RoundModel>();
        Debug.Log("我创建了回合");
        rModel.ClearRound();
        //rModel.LoadLevel(gModel.PlayLevel);


        for (int i = 0; i < gameModel.GameProgress; i++)
        {
            for (int j = 0; j < 3; j++)
            {


                float ra = UnityEngine.Random.Range(0, 10);
                Thread.Sleep(100);
                if (ra <= 5)
                {
                    rModel.CreateRound(new List<MonsterGroup> {
                    new MonsterGroup(0,2*gameModel.GameProgress),
                    new MonsterGroup(1,gameModel.GameProgress),
                     });
                }
                else
                {
                    rModel.CreateRound(new List<MonsterGroup> {
                    new MonsterGroup(0,gameModel.GameProgress),
                    new MonsterGroup(1,2*gameModel.GameProgress),
                     });
                }
            }
        }



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