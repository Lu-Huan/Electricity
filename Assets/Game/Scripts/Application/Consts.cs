using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Consts
{
    //目录
    /* public static readonly string LevelDir = Application.dataPath + @"\Game\Resources\Res\Levels";
     public static readonly string MapDir = Application.dataPath + @"\Game\Resources\Res\Maps";
     public static readonly string CardDir = Application.dataPath + @"\Game\Resources\Res\Cards";*/


    public const int MapWidth = 14;//地图宽
    public const int MapHeight = 10;//地图高
    //参数
    public const string GameProgress = "GameProgress";
    public const float DotClosedDistance = 0.55f;//在这个距离内就作为子弹命中
    public const float RangeClosedDistance = 0.7f;

    //Model
    public const string M_GameModel = "M_GameModel";
    public const string M_RoundModel = "M_RoundModel";
    public const string M_MapModel = "M_MapModel";


    //View
    public const string V_Start1 = "V_Start1";
    public const string V_Game1 = "V_Game1";
    public const string V_Select = "V_Select";
    public const string V_Board = "V_Board";
    public const string V_ShowLevelImformation = "V_ShowLevelImformation";
    public const string V_Win = "V_Win";
    public const string V_Lost = "V_Lost";
    public const string V_Sytem = "V_Sytem";
    public const string V_Complete = "V_Complete";
    public const string V_Spanwner = "V_Spanwner";
    public const string V_TowerPopup = "V_TowerPopup";
    public const string V_UI_Game2 = "UI_Game2";
    public const string V_MainCharacter = "V_MainCharacter";
    public const string V_MapV = "V_MapV";
    public const string V_Asynchronous = "V_Asynchronous";
    public const string V_CharacterPanel = "V_CharacterPanel";
    public const string V_Statistics="V_ Statistics";
    public const string V_Shop = "V_Shop";
    public const string V_TriggerShop = "V_TriggerShop";
    public const string V_TargetEffectShow = "V_TargetEffectShow";

    //Controller
    public const string E_StartUp = "E_StartUp";

    public const string E_EnterScene = "E_EnterScene"; //SceneArgs
    public const string E_ExitScene = "E_ExitScene";//SceneArgs

    public const string E_CreatLevel = "E_CreatLevel"; //StartLevelArgs
    public const string E_EndLevel = "E_EndLevel";//EndLevelArgs

    public const string E_CountDownComplete = "E_CountDownComplete";
    public const string E_CompleteInitMap = "E_CompleteMap";
    public const string E_CreatMap = "E_CreatMap";



    public const string E_StartRound = "E_StartRound";//StartRoundArgs
    public const string E_NextRound = "E_NextRound";

    public const string E_SpawnMonster = "E_SpawnMonster";//SpawnMonsterArgs
    public const string E_CompleteSpawnMonster = "E_SpawnMonster";
    public const string E_SpawnMonsterGroups = "E_SpawnMonsterGroups";
    public const string E_MonsterDead = "E_MonsterDead";
    public const string E_SelectObject = "E_SelectObject";

    public const string E_SpawnTower = "E_SpawnTower";//SpawnTowerArgs
    public const string E_CompleteSpawnTower = "E_CompleteSpawnTower";
    public const string E_UpgradeTower = "E_UpgradeTower";//UpgradeTowerArgs
    public const string E_SellTower = "E_SellTower";//SellTowerArgs

    public const string E_ShowCreate = "E_ShowCreate";//ShowCreatorArgs
    public const string E_ShowUpgrade = "E_ShowUpgrade";//ShowUpgradeArgs
    public const string E_HidePopup = "E_HidePopup";
    public const string E_Damage = "E_Damage";   
    public const string E_EnterShop = "E_BugTower";
    public const string E_ExitShop = "E_ExitShop";
    public const string E_AsynchronousLoading = "E_AsynchronousLoading";
    public const string E_ChangeGunBack = "E_ChangeGun";
    public const string E_BugGun = "E_BugGun";
    public const string E_ExitGunShop = "E_ExitGunShop";
    public const string E_SelectTarget = "E_SelectTarget";
    public const string E_ChangeGunRequest = "E_ChangeGunRequest";
    
}

public enum GameSpeed
{
    One,
    Two
}

public enum MonsterType
{
    Monster0,
    Monster1,
    Monster2,
    Monster3,
    Monster4,
    Monster5
}