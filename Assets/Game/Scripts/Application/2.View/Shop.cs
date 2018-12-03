using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : View
{
    public RectTransform RawImage;
    private Vector3 StartToucePos;
    private Vector2 CurrentTouceDir;
    public List<ShopGun> Guns;
    float TarPos_x;
    public GameObject CameraGun;
    public Button Left;
    public Button Right;

    public Button Buy;
    public Button trial;
    bool Istrial = false;

    //装备 和卸载
    public GameObject ToggleGroup;
    private int m_Gunindex = 0;

    public Text Price;
    public Text Attribute;
    public Text Imformation;
    public Text GunName;
    private bool rotating = false;
    public Text NoMoney;
    public int GunIndex
    {
        set
        {
            if (value == 0)
            {
                Left.gameObject.SetActive(false);
            }
            else if (value == Guns.Count - 1)
            {
                Right.gameObject.SetActive(false);
            }
            else
            {
                Left.gameObject.SetActive(true);
                Right.gameObject.SetActive(true);
            }
            m_Gunindex = value;



        }
        get
        {
            return m_Gunindex;
        }
    }
    public override string Name
    {
        get
        {
            return Consts.V_Shop;
        }
    }

    public bool GoMove { get; private set; }

    public override void HandleEvent(string eventName, object data)
    {
        throw new System.NotImplementedException();
    }
    public override void RegisterEvents()
    {
        base.RegisterEvents();
    }

    // Use this for initialization
    void Start()
    {
        CameraGun.transform.position = new Vector3(400, 300, CameraGun.transform.position.z);
        for (int i = 0; i < Guns.Count; i++)
        {
            Guns[i].Already_have = Game.Instance.StaticData.HaveGun[Guns[i].ID].Have;
            Guns[i].Equiped = Game.Instance.StaticData.HaveGun[Guns[i].ID].InBag;
        }

        transform.Find("Back").GetComponent<Button>().onClick.AddListener(() =>
        {
            SendEvent(Consts.E_ExitGunShop);
        });
        ToggleGroup.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            Uninstall();
        });

        ToggleGroup.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            Equip();
        });
        SortGun();
        GunIndex = 0;
        Show();
    }


    private void Equip()
    {
        SendEvent(Consts.E_EquipGun, Guns[m_Gunindex]);
    }
    private void Uninstall()
    {

        SendEvent(Consts.E_UninstallGun, Guns[m_Gunindex]);
    }
    public void BugGun()
    {
        int ele = PlayerPrefs.GetInt("ElectricEnergy", -1);
        if (Guns[m_Gunindex].Price <= ele)
        {
            Game.Instance.StaticData.HaveGun[m_Gunindex].Have = true;
            Game.Instance.StaticData.SaveHaveGun();

            Guns[m_Gunindex].Already_have = true;
            ele -= Guns[m_Gunindex].Price;
            PlayerPrefs.SetInt("ElectricEnergy", ele);
            transform.parent.GetComponent<UI_Game1>().ReMoney();
            Show();
        }
        else
        {
            NoMoney.gameObject.SetActive(true);
            Invoke("Hide", 1f);
        }
    }
    void Hide()
    {
        NoMoney.gameObject.SetActive(false);
    }
    public void Trial()
    {
        Istrial = true;
        SendEvent(Consts.E_TrialGun, Guns[m_Gunindex]);
    }
    /// <summary>
    /// 用于排列枪
    /// </summary>
    private void SortGun()
    {
        for (int i = 0; i < Guns.Count; i++)
        {
            Guns[i].transform.position = new Vector3(400 + i * 2, 300, Guns[i].transform.position.z);
        }
    }
    private void Update()
    {

        /*if (Input.touchCount == 1)
        {
            //开始记录
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                //如果在图片范围内
                if (RectTransformUtility.RectangleContainsScreenPoint(RawImage, Input.touches[0].position))
                {
                    StartToucePos = Input.touches[0].position;

                }
            }
            if (RectTransformUtility.RectangleContainsScreenPoint(RawImage, Input.touches[0].position))
            {
                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    CurrentTouceDir = Input.touches[0].deltaPosition;
                    Guns[GunIndex].transform.Rotate(Vector3.up, CurrentTouceDir.x * 2);
                }
            }
        }*/
        if (!rotating)
        {
            Guns[GunIndex].transform.Rotate(0, 0.5f, 0);
        }
        if (Input.GetMouseButtonDown(0) &&
            RectTransformUtility.RectangleContainsScreenPoint(RawImage, Input.mousePosition))
        {
            StartToucePos = Input.mousePosition;
            rotating = true;
        }

        if (Input.GetMouseButtonUp(0))
            rotating = false;

        if (rotating && Input.GetMouseButton(0))
            Guns[GunIndex].transform.Rotate(0, -300f * (Input.mousePosition - StartToucePos).x / Screen.width, 0);

        StartToucePos = Input.mousePosition;


        if (!GoMove)
        {
            return;
        }
        float x = CameraGun.transform.position.x;
        x = Mathf.Lerp(x, TarPos_x, Time.deltaTime * 3f);
        if (Mathf.Abs(x - TarPos_x) < 0.1f)
        {
            x = TarPos_x;
            GoMove = false;
            Show();
        }
        CameraGun.transform.position = new Vector3(x, 300f, 902.25f);

    }
    private void Show()
    {
        GunName.text = Guns[m_Gunindex].name;
        Price.text = Guns[m_Gunindex].Price.ToString();
        // string UseChar= PlayerPrefs.GetString("Char"+ Guns[m_Gunindex].Char+ "Name",null);
        bool Have = Guns[m_Gunindex].Already_have;
        trial.gameObject.SetActive(!Have);
        Buy.gameObject.SetActive(!Have);


        Price.transform.parent.gameObject.SetActive(!Have);
        ToggleGroup.SetActive(Have);
        if (Have)
        {
            bool Equip = Guns[m_Gunindex].Equiped;
            ToggleGroup.transform.GetChild(0).gameObject.SetActive(Equip);

            ToggleGroup.transform.GetChild(1).gameObject.SetActive(!Equip);
        }



        Imformation.text = Guns[m_Gunindex].Imformation;
        string Attack = "伤害:";
        for (int i = 0; i < Guns[m_Gunindex].Attack; i++)
        {
            Attack += "//";
        }
        string ShootRate = "射速:";
        for (int i = 0; i < Guns[m_Gunindex].ShootRate; i++)
        {
            ShootRate += "/";
        }
        string Accurate = "精准:";
        for (int i = 0; i < Guns[m_Gunindex].Accurate; i++)
        {
            Accurate += "/";
        }
        string Distance = "距离:";
        for (int i = 0; i < Guns[m_Gunindex].Distance; i++)
        {
            Distance += "/";
        }
        Attribute.text = Attack + "\r\n" + "\r\n" + ShootRate + "\r\n" + "\r\n" + Accurate + "\r\n" + "\r\n" + Distance;
    }
    public void Move(int dir)
    {
        if (Istrial)
        {
            Uninstall();
            Istrial = false;
        }

        if (!GoMove)
        {
            TarPos_x = CameraGun.transform.position.x + dir;
            if (dir > 0)
            {
                GunIndex++;
            }
            else
            {
                GunIndex--;
            }
            GoMove = true;
        }
    }
}
