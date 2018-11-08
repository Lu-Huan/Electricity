﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class StartUpCommand : Controller
{
    public override void Execute(object data)
    {
        //注册模型（Model）
        RegisterModel(new GameModel());
        RegisterModel(new RoundModel());
        RegisterModel(new MapModel());
        RegisterModel(new Spawner());

        //注册命令（Controller）
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneComand));//进入场景
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));//退出场景
        RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));//开始关卡
        RegisterController(Consts.E_NextRound, typeof(NextRoundCommand));//下一个关卡
        RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));//结束关卡
        RegisterController(Consts.E_CountDownComplete, typeof(CountDownCompleteCommand));//倒计时结束


        RegisterController(Consts.E_SpawnMonster, typeof(SpawnerMonsterCommand));//生成怪
        RegisterController(Consts.E_SpawnTower, typeof(SpawnerTowerCommand));//生成塔
        RegisterController(Consts.E_UpgradeTower, typeof(UpgradeTowerCommand)); //升级塔
        RegisterController(Consts.E_SellTower, typeof(SellTowerCommand)); //卖掉塔
        RegisterController(Consts.E_Damage, typeof(Damage));

        //初始化
        GameModel gModel = GetModel<GameModel>();
        gModel.Initialize();

        //进入开始界面
        Game.Instance.LoadScene(3);
    }
}