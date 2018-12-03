using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Game2 : View
{
    private Animator Round_An;
    private Transform Round_Tr;
    public Image UI_BarrelHP_BG;
    public Image UI_BarrelHP_HP;
    private Animator Shop;
    private Transform End = null;
    public Vector2 offect = new Vector2(50, 0);
    private Text RoundMessage;
    private Text Enemy;
    private Text CurrentRound;
    //模型
    private RoundModel roundModel;
    private MapModel MapModel;
    private Transform MainChar;

    private int TotalEnemy;
    public string ResourceDir;
    public Transform[] Grid;
    private Text MoneyText;
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
    private Slider loadingSlider;
    public GameObject Win;
    public GameObject Fail;
    public GameObject Dead;
    public GameObject Load;
    private Text GunName;
    private Image Gun;
    public Sprite[] Guns;

    private Image BulidTower;
    bool CanBulid = false;
    bool IsShop = false;
    //统计面板
    private GameObject Statistics;
    public int Gunindex
    {
        set
        {

        }
    }
    public override string Name
    {
        get
        {
            return Consts.V_UI_Game2;
        }
    }

    // Use this for initialization
    void Start()
    {
        Gun = transform.Find("Gun").GetComponent<Image>();
        GunName = Gun.transform.Find("Text").GetComponent<Text>();
        Gun.transform.Find("Left").GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangGunRequset changGunRequset = new ChangGunRequset
            {
                IsRight = false,
                GunID = -1
            };
            SendEvent(Consts.E_ChangeGunRequest,changGunRequset);
        });

        Gun.transform.Find("Right").GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangGunRequset changGunRequset = new ChangGunRequset
            {
                IsRight = true,
                GunID = -1
            };
            SendEvent(Consts.E_ChangeGunRequest, changGunRequset);
        });

        NoMoney = transform.Find("NoMoney").GetComponent<Animator>();

        Shop = GameObject.Find("Shop").GetComponent<Animator>();
        MoneyText = GameObject.Find("Money").GetComponent<Text>();
        MoneyText.text = money.ToString();
        Round_Tr = GameObject.Find("Round").transform;
        Round_An = Round_Tr.GetComponent<Animator>();

        Grid = Round_Tr.GetComponentsInChildren<Transform>(true);
        RoundMessage = GameObject.Find("Level_Round").GetComponent<Text>();
        Enemy = GameObject.Find("Enemy").GetComponent<Text>();
        CurrentRound = GameObject.Find("CurrentRound").GetComponent<Text>();
        RoundX = transform.Find("Roundx").GetComponent<Text>();
        loadingSlider = Load.transform.Find("LoadingBar").GetComponent<Slider>();
        BulidTower = transform.Find("BulidTower").GetComponent<Image>();
        BulidTower.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuyTower();
        });
        MapModel = GetModel<MapModel>();

        MainChar = GetModel<GameModel>().MainCharater.transform;

        Statistics = transform.Find("Statistics").gameObject;
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

        Tile tile = MapModel.GetTile(MainChar.position);
        if (tile.Data != null || tile.IsPath)
        {
            BulidTower.transform.Find("Image").gameObject.SetActive(true);
            CanBulid = false;
        }
        else
        {
            BulidTower.transform.Find("Image").gameObject.SetActive(false);
            CanBulid = true;
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_ChangeGunBack);
        AttentionEvents.Add(Consts.E_EnterShop);
        AttentionEvents.Add(Consts.E_ExitShop);
        AttentionEvents.Add(Consts.E_Damage);
        AttentionEvents.Add(Consts.E_StartRound);
        AttentionEvents.Add(Consts.E_NextRound);
        AttentionEvents.Add(Consts.E_MonsterDead);
        AttentionEvents.Add(Consts.E_SpawnMonsterGroups);
        AttentionEvents.Add(Consts.E_CompleteSpawnMonster);
        AttentionEvents.Add(Consts.E_CompleteSpawnTower);
        AttentionEvents.Add(Consts.E_CompleteInitMap);
        AttentionEvents.Add(Consts.E_EndLevel);

    }
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_ChangeGunBack:
                E_ChangeGunBack(data);
                break;
            case Consts.E_MonsterDead:
                E_MonsterDead(data);
                break;
            case Consts.E_Damage:
                E_Damage();
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
            case Consts.E_EndLevel:
                E_EndLevel(data);
                break;
        }
    }

    private void E_ChangeGunBack(object data)
    {
        ChangGunBackArgs gunInfo = data as ChangGunBackArgs;
        Gun.sprite = Guns[gunInfo.GunID];
        GunName.text = gunInfo.GunName;
    }

    private void E_EndLevel(object data)
    {
        Statistics.SetActive(true);
        EndLevelArgs endLevelArgs = data as EndLevelArgs;
        if (endLevelArgs.IsSuccess)
        {
            Win.SetActive(true);
        }
        else if (endLevelArgs.IsDead)
        {
            Dead.SetActive(true);
        }
        else
        {
            Fail.SetActive(true);
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

        RoundX.GetComponent<Animator>().SetTrigger("Roundx");
        RoundX.text = "Round " + index;
        RoundMessage.text = "Round:" + index + "/" + startRoundArgs.RoundTotal;
        CurrentRound.text = "NPC:     /" + MT;
        TotalEnemy = MT;
        Enemy.text = TotalEnemy.ToString();
    }
    private void E_MonsterDead(object data)
    {
        TotalEnemy--;
        Enemy.text = TotalEnemy.ToString();
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
    private void E_CompleteInitMap()
    {
        SendEvent(Consts.E_CreatLevel, null);//建立一个关卡
        SendEvent(Consts.E_CountDownComplete);
    }
    private void BuyTower()
    {
        if (CanBulid)
        {
            if (IsShop)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }
    private void CloseShop()
    {
        IsShop = false;
        SendEvent(Consts.E_ExitShop);
        Shop.SetBool("Up", false);
    }

    private void OpenShop()
    {
        IsShop = true;
        SendEvent(Consts.E_EnterShop);
        Shop.SetBool("Up", true);
    }

    public void SpawnTower(int ID)
    {
        CloseShop();

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


    public void LoadNewLevel()
    {
        Load.SetActive(true);
        StartCoroutine(UILoad());
        SceneArgs sceneArgs = new SceneArgs
        {
            SceneIndex = 3
        };
        SendEvent(Consts.E_ExitScene,sceneArgs);
        SendEvent(Consts.E_CreatMap);
    }
    public void BackGame1()
    {
        Game.Instance.LoadScene(2);
    }
    IEnumerator UILoad()
    {
        for (int i = 1; i <= 20; i++)
        {
            loadingSlider.value = i * 5;
            yield return null;
        }
        Load.SetActive(false);
        loadingSlider.value = 0;
    }
}
