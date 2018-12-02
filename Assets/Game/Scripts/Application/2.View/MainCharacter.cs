using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainCharacter : View
{
    //人物的属性
    public float Speed = 2;
    private bool IsWalk = true;
    private bool IsShop = false;

    MapModel mapModel;
    Actions Actions;

    private float ShootRate = 4;
    private float Timer = 0;
    [SerializeField] private string GunName = "empty";
    [SerializeField] private float Distance = 1;
    private bool IsShoot = false;
    private bool IsClose = false;
    private bool Colder = false;
    private bool BuyGun = false;
    private bool[] HaveGun;
    public GameObject SelectedEffect;//选中怪物后的效果显示
    public int sceneindex;
    private Role m_target;
    public Role Target
    {
        get
        {
            return m_target;
        }
        set
        {
            Target = value;
            SendEvent(Consts.E_SelectTarget, m_target);
        }
    }

    /// <summary>
    /// 继承使用
    /// </summary>
    protected Transform bullteRight = null;
    protected Transform bullteLeft = null;
    protected bool IsRight = true;
    protected PlayerController PlayerController;

    int index = 0;
    protected int Gunindex
    {
        set
        {
            index = value % PlayerController.arsenal.Length;
            if (index < 0)
            {
                index = PlayerController.arsenal.Length - 1;
            }
            GunInfo gunInfo = Game.Instance.StaticData.GetGunInfo(index);
            GunName = gunInfo.PrefabName;
            ShootRate = gunInfo.ShootRate;
            Distance = gunInfo.ShootingDistance;
        }
        get
        {
            return index;
        }
    }
    public void Load(int ID)
    {
        Character character = Game.Instance.StaticData.GetCharacter(ID);
        HaveGun = character.HaveGun;
    }
    #region MVC
    public override string Name
    {
        get
        {
            return Consts.V_MainCharacter;
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_SpawnTower);
        AttentionEvents.Add(Consts.E_ExitShop);
        AttentionEvents.Add(Consts.E_EnterScene);
        AttentionEvents.Add(Consts.E_EnterShop);
        AttentionEvents.Add(Consts.E_ChangeGunRequest);
        AttentionEvents.Add(Consts.E_BugGun);
        AttentionEvents.Add(Consts.E_ExitGunShop);
    }
    public override void HandleEvent(string eventName, object data)
    {
        if (eventName == Consts.E_SpawnTower)
        {
            Actions.Stay();
            IsShop = false;
        }
        else if (eventName == Consts.E_ExitShop)
        {
            IsShop = false;
        }
        else if (eventName == Consts.E_EnterScene)
        {
            Debug.Log("收到");
            SceneArgs sceneArgs = data as SceneArgs;

            sceneindex = sceneArgs.SceneIndex;

        }
        else if (eventName == Consts.E_EnterShop)
        {
            Tile tile = mapModel.GetTile(transform.position);
            if (!tile.IsPath && tile.Data == null)
            {
                IsShop = true;
                transform.position = tile.Position;
            }
        }
        else if (eventName == Consts.E_ChangeGunRequest)
        {
            ChangGunRequset changGunRequset = data as ChangGunRequset;
            if (changGunRequset.ID == -1)
            {
                if (changGunRequset.IsRight)
                {
                    Gunindex++;
                    while (!HaveGun[Gunindex])
                    {
                        Gunindex++;
                    }
                }
                else
                {
                    Gunindex--;
                    while (!HaveGun[Gunindex])
                    {
                        Gunindex--;
                    }
                }
            }
            else
            {
                Gunindex = changGunRequset.ID;

            }
            Debug.Log(Gunindex);
            ChangeGun();
        }
        else if (eventName == Consts.E_BugGun)
        {
            BuyGun = true;
            transform.position = new Vector3(3.62f, 0, -7.47f);
        }
        else if (eventName == Consts.E_ExitGunShop)
        {
            BuyGun = false;
        }
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        Actions = GetComponent<Actions>();
        PlayerController = GetComponent<PlayerController>();
        //Animation = GetComponent<Animation>();
        //Animation.Play("idle");
        Actions.Stay();
        mapModel = GetModel<MapModel>();
        Gunindex = 0;

    }


    // Update is called once per frame
    void Update()
    {
        //冷却计时器
        Timer += Time.deltaTime;
        if (Timer >= 1f / ShootRate)
        {
            Colder = false;
            Timer = 0;
        }

        if (IsShop)
        {
            return;
        }
        GetSomeKey();

        ShootOrWalk();
    }

    void ShootOrWalk()
    {
        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        float z = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 dir = new Vector3(x, 0, z);

        if (Mathf.Abs(x) + Mathf.Abs(z) < 0.02f)
        {
            if (IsWalk)
            {
                Actions.Stay();
                IsWalk = false;
            }
        }
        /*else if (Input.GetKey(KeyCode.LeftShift))//加速暂时不需要
        {
            AdjustmentDirection(dir);
            Actions.Run();
            Speed = 3f;
            IsWalk = true;
            if (Shoot)
            {
                Shoot = false;
            }

        }*/
        else
        {
            AdjustmentDirection(dir);
            Actions.Walk();
            if (!IsWalk)
            {
                /*if (Gunindex == 3)
                {
                    YM27.GetComponent<XLine>().Close();
                }*/
                IsWalk = true;
                IsShoot = false;
            }
        }
        if (IsWalk)
        {
            if (!BuyGun)
            {
                transform.position += dir * Time.deltaTime * Speed;
            }
        }
        else if (IsShoot)
        {
            if (Target == null)
            {
                return;
            }

            dir = Target.Position - transform.position;
            dir.y = 0;
            AdjustmentDirection(dir);
            float dis = Mathf.Sqrt(dir.x * dir.x + dir.z * dir.z);
            if (dis > Distance)
            {
                IsClose = true;
            }
            if (IsClose)
            {
                dir = Vector3.Normalize(dir) * 1.4f;
                transform.position += dir * Time.deltaTime * Speed;
                Actions.Walk();
                float s = Distance * 2f / 3f;
                if (Gunindex == 0)
                {
                    s = 0.1f;
                }
                if (dis <= s)
                {
                    IsClose = false;
                }
            }
            else
            {
                if (!Colder)
                {
                    Actions.Attack();
                    Colder = true;
                    Timer = 0;
                    InstanceBullte();
                }
            }
        }
    }
    /// <summary>
    /// 获取按键
    /// </summary>
    private void GetSomeKey()
    {
        //移植后不需要的动作
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Actions.Jump();
            Invoke("JumpBack", 1f);
            IsJump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Actions.Sitting();
        }*/
        if (CrossPlatformInputManager.GetButton("Shoot"))//左键射击
        {
            GetTarge();
        }
        else if (Input.touchCount == 1)//安卓点击屏幕事件（一个手指）
        {
            //射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            //点到了到可以射击的物体
            if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer("MonsterOrMess")))
            {
                Target = hit.collider.gameObject.GetComponent<Role>();
                Target.Dead += Targe_Dead;
                SendEvent(Consts.E_SelectObject, Target);
                IsShoot = true;
            }
        }
        /*else if (Input.GetMouseButtonDown(1))
        {
            Actions.Aiming();
        }*/
    }
    protected virtual void InstanceBullte() { }

    private void ChangeGun()
    {
        PlayerController.SetArsenal(PlayerController.arsenal[Gunindex].name);
        ChangGunBackArgs args = new ChangGunBackArgs
        {
            GunID = index,
            GunName = GunName
        };
        SendEvent(Consts.E_ChangeGunBack, args);
    }

    /// <summary>
    /// 调整方向
    /// </summary>
    /// <param name="dir"></param>
    private void AdjustmentDirection(Vector3 dir)
    {
        Vector3 Start = new Vector3(0, 0, 1);
        float angle = Vector3.Angle(Start, dir); //求出两向量之间的夹角
        Vector3 normal = Vector3.Cross(Start, dir);//叉乘求出法线向量  
        angle *= Mathf.Sign(Vector3.Dot(normal, new Vector3(0, 1, 0)));  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向 
        transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    /// <summary>
    /// 选定一个目标
    /// </summary>
    private void GetTarge()
    {
        Spawner spawner = GetModel<Spawner>();
        if (spawner.ListMonsters.Count > 0)
        {
            Target = spawner.ListMonsters[0];//这里可能要改
            IsShoot = true;
            Target.Dead += Targe_Dead;
        }
        else
        {
            Target = null;
            Actions.Attack();
        }
    }

    private void Targe_Dead(Role obj)
    {
        GetTarge();
    }
}
