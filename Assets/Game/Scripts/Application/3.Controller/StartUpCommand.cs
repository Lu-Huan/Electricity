﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using LitJson;

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
        RegisterController(Consts.E_CreatLevel, typeof(CreatLevelCommand));//开始关卡
        RegisterController(Consts.E_NextRound, typeof(NextRoundCommand));//下一个回合
        RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));//结束关卡
        RegisterController(Consts.E_CountDownComplete, typeof(CountDownCompleteCommand));//倒计时结束
        RegisterController(Consts.E_SelectObject, typeof(SelectObjectCommand));
        RegisterController(Consts.E_CreatMap, typeof(CreatMapCommand));

        RegisterController(Consts.E_SpawnMonster, typeof(SpawnerMonsterCommand));//生成怪
        RegisterController(Consts.E_SpawnTower, typeof(SpawnerTowerCommand));//生成塔
        RegisterController(Consts.E_Damage, typeof(Damage));

        //初始化
       GameModel gModel = GetModel<GameModel>();
        gModel.Initialize();
        if (-1==PlayerPrefs.GetInt("CurrentChar", -1))
        {
            //拥有的人物
            PlayerPrefs.SetInt("Char" + 0, 1);
            PlayerPrefs.SetInt("Char" + 1, 0);
            PlayerPrefs.SetInt("Char" + 2, 0);
            PlayerPrefs.SetInt("Char" + 3, 0);
            //当前的角色
            PlayerPrefs.SetInt("CurrentChar", 0);
            //能量
            PlayerPrefs.SetInt("ElectricEnergy", 100);
        }
        gModel.Current_role = PlayerPrefs.GetInt("CurrentChar", -1);   
        //进入开始界面
        Game.Instance.LoadScene(1);
    }
}