using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EndLevelCommand : Controller
{
    public override void Execute(object data)
    {
        EndLevelArgs e = data as EndLevelArgs;
        GameModel gm = GetModel<GameModel>();
        RoundModel rModel = GetModel<RoundModel>();

        //停止出怪
        rModel.StopRound();

        //停止游戏
        //gm.EndLevel(e.IsSuccess);
        if (e.IsSuccess)
        {
            gm.Gold += GetView<UI_Game2>().Money*2+300;
        }
        else
        {
            int m=GetView<UI_Game2>().Money -40;
            m=m < 0 ? 0 : m;
            gm.Gold+= m;
        }
        Debug.Log("这里保存金币");
    }
}