using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterPanel : View
{
    public GameObject[] character;
    public Transform weaponsPanel;
    public Transform actionsPanel;
    public Transform camerasPanel;
    public Button buttonPrefab;
    public Slider motionSpeed;
    private GameObject Lock;
    public Button Select;
    public GameObject CurrSelect;
    public List<List<Button>> buttons = new List<List<Button>>();

    public Text CharName;
    public Text About;
    private int Charindex = 0;
    public int Index
    {
        set
        {
            if (value < 0)
            {
                Charindex = character.Length - 1;
            }
            else if (value >= character.Length)
            {
                Charindex = 0;
            }
            else
            {
                Charindex = value;
            }
        }
        get
        {
            return Charindex;
        }
    }


    public override string Name
    {
        get
        {
            return Consts.V_CharacterPanel;
        }
    }

    Actions actions;
    PlayerController controller;
    Camera[] cameras;

    void Awake()
    {
        Lock = GameObject.Find("LockButton");
        Select.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("CurrentChar", Index);
            GetModel<GameModel>().Current_role = Index;
            ShowData();
        });
        Select.gameObject.SetActive(false);
        CurrSelect.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            buttons.Add(new List<Button>());
            Initialize(i);
        }
        SetButton();
        Index = 0;
        ShowData();
    }

    void Initialize(int index)
    {
        actions = character[index].GetComponent<Actions>();
        controller = character[index].GetComponent<PlayerController>();

       /* for (int i = 0; i < controller.arsenal.Count; i++)
        {
            CreateWeaponButton(controller.arsenal[i].name, index,i);
        }*/
    }

    private void SetButton()
    {
        CreateActionButton("站立", "Stay");
        CreateActionButton("行走","Walk");
        CreateActionButton("跑","Run");
        CreateActionButton("下蹲", "Sitting");
        CreateActionButton("跳跃", "Jump");
        CreateActionButton("瞄准","Aiming");
        CreateActionButton("攻击", "Attack");
        CreateActionButton("受伤", "Damage");
        CreateActionButton("死亡", "Death");

        cameras =FindObjectsOfType<Camera>();
        var sort = from s in cameras orderby s.name select s;

        foreach (Camera c in sort)
            CreateCameraButton(c);

        camerasPanel.GetChild(0).GetComponent<Button>().onClick.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">武器的名字</param>
    /// <param name="id">所属哪个buttons集合</param>
   /* void CreateWeaponButton(string name, int Listid,int GunId)
    {
        Button button = CreateButton(name, weaponsPanel);
        button.onClick.AddListener(() => controller.SetArsenal(GunId));
        buttons[Listid].Add(button);
    }*/

    void CreateActionButton(string name)
    {
        CreateActionButton(name, name);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">动作名字</param>
    /// <param name="message">对应的Action的函数消息</param>
    void CreateActionButton(string name, string message)
    {
        Button button = CreateButton(name, actionsPanel);
        button.onClick.AddListener(() => actions.SendMessage(message, SendMessageOptions.DontRequireReceiver));
    }

    void CreateCameraButton(Camera c)
    {
        Button button = CreateButton(c.name, camerasPanel);
        button.onClick.AddListener(() =>
        {
            ShowCamera(c);
        });
    }

    Button CreateButton(string name, Transform group)
    {
        GameObject obj =Instantiate(buttonPrefab.gameObject);
        obj.name = name;
        obj.transform.SetParent(group);
        obj.transform.localScale = Vector3.one;
        Text text = obj.transform.GetChild(0).GetComponent<Text>();
        text.text = name;
        return obj.GetComponent<Button>();
    }

    void ShowCamera(Camera cam)
    {
        foreach (Camera c in cameras)
            c.gameObject.SetActive(c == cam);
    }

    void Update()
    {
        Time.timeScale = motionSpeed.value;
    }
    public void ChangeChar(int i)
    {
        Index += i;
        ShowData();
    }  
    /// <summary> 
    /// 根据数据展示面板
    /// </summary>
    private void ShowData()
    {
        Character ch =Game.Instance.StaticData.GetCharacter(Charindex);
        CharName.text= ch.Name;
        About.text = ch.Imformation;
        for (int i = 0; i < character.Length; i++)
        {
            if (i != Charindex)
            {
                foreach (Button item in buttons[i])
                {
                    item.gameObject.SetActive(false);
                }
                character[i].SetActive(false);
            }
            else
            {
                foreach (Button item in buttons[i])
                {
                    item.gameObject.SetActive(true);
                }
                character[i].SetActive(true);
            }
        }
        actions = character[Charindex].GetComponent<Actions>();
        controller = character[Charindex].GetComponent<PlayerController>();

        int s = PlayerPrefs.GetInt("Char" + Charindex, -1);
        if (s == -1)
        {
            Debug.Log(Charindex);
        }
        else
        {
            if (s == 0)
            {
                Lock.gameObject.SetActive(true);
                Select.gameObject.SetActive(false);
                CurrSelect.SetActive(false);
            }
            else
            {
                Lock.gameObject.SetActive(false);
                int currentChart = GetModel<GameModel>().Current_role;
                if (currentChart!=Charindex)
                {
                    Select.gameObject.SetActive(true);
                    CurrSelect.SetActive(false);
                }
                else
                {
                    Select.gameObject.SetActive(false);
                    CurrSelect.SetActive(true);
                }
            }
        }
    }
    public void Back()
    {
        Game.Instance.LoadScene(2);
    }

    public override void HandleEvent(string eventName, object data)
    {

    }
}
