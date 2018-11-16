using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game2 : View
{

    private Animator Round_An;
    private Transform Round_Tr;
    private MapV MapV;
    public Image UI_BarrelHP_BG;
    public Image UI_BarrelHP_HP;
    public Animator Shop;
    private Transform End = null;
    public Vector2 offect = new Vector2(50, 0);
    public Text RoundMessage;
    public Text Enemy;
    public Text CurrentRound;
    private RoundModel roundModel;
    private int TotalEnemy;
    public string ResourceDir;
    public Transform[] Grid;
    public Text MoneyText;
    public int Money
    {
        set
        {
            money = value;
            for (int i = 0; i < towerPrice.Length; i++)
            {
                if (towerPrice[i] > money)
                {
                    towerText[i].color = Color.black;
                }
                else
                {
                    towerText[i].color = new Color(0, 241, 238);
                }
            }
        }
        get
        {
            return money;
        }
    }
    [SerializeField] private int money = 0;
    public float CurrentMoney = 0;
    public int[] towerPrice;
    public Text[] towerText;
    private Animator NoMoney;
    private Text RoundX;
    public GameObject Win;
    public override string Name
    {
        get
        {
            return Consts.V_UI_Game2;
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_BugTower);
        AttentionEvents.Add(Consts.E_ExitShop);
        AttentionEvents.Add(Consts.E_Damage);
        AttentionEvents.Add(Consts.E_StartRound);
        AttentionEvents.Add(Consts.E_NextRound);
        AttentionEvents.Add(Consts.E_MonsterDead);
        AttentionEvents.Add(Consts.E_SpawnMonsterGroups);
        AttentionEvents.Add(Consts.E_CompleteSpawnMonster);
        AttentionEvents.Add(Consts.E_CompleteSpawnTower);
        AttentionEvents.Add(Consts.E_CompleteInitMap);
        AttentionEvents.Add(Consts.E_AllRoundsComplete);
    }
    // Use this for initialization
    void Start()
    {
        NoMoney = transform.Find("NoMoney").GetComponent<Animator>();
        Shop = GameObject.Find("Shop").GetComponent<Animator>();
        MoneyText = transform.Find("Money").GetComponent<Text>();
        MoneyText.text = money.ToString();
        Round_Tr = transform.Find("Round");
        Round_An = Round_Tr.GetComponent<Animator>();
        Grid = Round_Tr.GetComponentsInChildren<Transform>(true);
        RoundMessage = GameObject.Find("Level_Round").GetComponent<Text>();
        Enemy = GameObject.Find("Enemy").GetComponent<Text>();
        CurrentRound = GameObject.Find("CurrentRound").GetComponent<Text>();
        MapV = GameObject.Find("Map").GetComponent<MapV>();


        RoundX = transform.Find("Roundx").GetComponent<Text>();


    }
    /*private void UI_RoundEvent_ShowComplete()//地图生成后展示关卡和回合信息
    {

        Round.SetTrigger("StartRound");//
    }*/



    // Update is called once per frame
    void Update()
    {
        if (End == null)
        {
            return;
        }
        Vector2 end = Camera.main.WorldToScreenPoint(End.position);
        UI_BarrelHP_BG.transform.position = end + offect;
        float t = Screen.width / 2;
        offect.x = 45f * (UI_BarrelHP_BG.transform.position.x - t) / t;
        offect.y = 46f + 36 * UI_BarrelHP_BG.transform.position.y / Screen.height;

        CurrentMoney = Mathf.Lerp(CurrentMoney, money + 1, Time.deltaTime * 5);
        MoneyText.text = ((int)CurrentMoney).ToString();
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_MonsterDead:
                E_MonsterDead(data);
                break;
            case Consts.E_Damage:
                E_Damage();
                break;
            case Consts.E_BugTower:
                Shop.SetBool("Up", true);
                break;
            case Consts.E_ExitShop:
                Shop.SetBool("Up", false);
                break;
            case Consts.E_StartRound:
                E_StartRound(data);
                break;
            case Consts.E_SpawnMonsterGroups:
                E_SpawnMonsterGroups(data);
                break;
            case Consts.E_CompleteInitMap:
                E_CompleteInitMap();
                break;
            case Consts.E_AllRoundsComplete:
                Win.SetActive(true);
                break;
        }
    }
    private void E_Damage()
    {
        float hp = roundModel.End.Hp;
        float maxhp = roundModel.End.MaxHp;
        UI_BarrelHP_HP.fillAmount = hp / maxhp;
    }
    private void E_StartRound(object data)
    {
        roundModel = GetModel<RoundModel>();
        MapModel mapModel = GetModel<MapModel>();
        End = mapModel.end.transform;
        StartRoundArgs startRoundArgs = data as StartRoundArgs;
        int index = startRoundArgs.RoundIndex + 1;
        int MT = startRoundArgs.MonsterTotal;
        int TE = TotalEnemy;

        RoundX.GetComponent<Animator>().SetTrigger("Roundx");
        RoundX.text = "Round " + index;
        RoundMessage.text = "回合:" + index + "/" + startRoundArgs.RoundTotal;
        CurrentRound.text = "剩余敌人:     /" + MT;
        TotalEnemy = MT;
        Enemy.text = "                " + TotalEnemy;
    }
    private void E_MonsterDead(object data)
    {
        TotalEnemy--;
        Enemy.text = "                " + TotalEnemy;
        Monster monster = data as Monster;
        Money += monster.price;
    }
    private void E_SpawnMonsterGroups(object data)
    {

        MonsterGroup Group = data as MonsterGroup;
        for (int i = 2; i < Grid.Length; i += 2)
        {
            if ((i) / 2 - 1 != Group.monsterID)
            {
                Grid[i].gameObject.SetActive(false);
            }
            else
            {
                Grid[i].gameObject.SetActive(true);
                Grid[i + 1].GetComponent<Text>().text = "X   " + Group.count;
            }
        }
        Round_An.SetTrigger("StartRound");
    }
    /*private void E_NextRound()
    {

    }*/
    private void E_CompleteInitMap()
    {
        Debug.Log("显示关卡信息");
        Debug.Log("开始出怪");
        SendEvent(Consts.E_StartLevel, null);//建立一个关卡
        SendEvent(Consts.E_CountDownComplete);
    }
    public void SpawnTower(int ID)
    {
        SendEvent(Consts.E_ExitShop);

        if (towerPrice[ID] <= money)
        {
            GameModel gameModel = GetModel<GameModel>();
            SpawnTowerArgs args = new SpawnTowerArgs
            {
                TowerID = ID,
                Position = gameModel.MainCharater.transform.position
            };
            SendEvent(Consts.E_SpawnTower, args);
            Money -= towerPrice[ID];
        }
        else
        {
            NoMoney.SetTrigger("Show");
        }
    }

    //异步加载新场景

    public void LoadNewScene()
    {
        Game.Instance.LoadScene(4);
    }

}
