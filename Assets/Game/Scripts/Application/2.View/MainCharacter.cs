using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainCharacter : View
{
    public float Speed = 2;
    private bool IsWalk = true;
    private bool IsShop = false;
    private bool IsJump = false;
    MapModel mapModel;
    Actions Actions;
    PlayerController PlayerController;
    private Role targe = null;
    public GameObject SelectedEffect;//选中怪物后的效果显示
    [SerializeField] private float ShootRate = 4;
    private float Timer = 0;
    [SerializeField] private string GunName = "empty";
    [SerializeField] private int GunID = 0;
    [SerializeField] private float Distance = 1;
    private bool Shoot = false;
    private bool Close = false;
    private bool Colder = false;
    int index = 0;
    private Transform bullteRight = null;
    private Transform bullteLeft = null;
    private bool IsRight = true;
    private Transform YM27;
    public int sceneindex;
    int Playerindex
    {
        set
        {
            index = value % PlayerController.arsenal.Length;
            GunInfo gunInfo = Game.Instance.StaticData.GetGunInfo(index);
            GunName = gunInfo.PrefabName;
            GunID = gunInfo.ID;
            ShootRate = gunInfo.ShootRate;
            Distance = gunInfo.ShootingDistance;
        }
        get
        {
            return index;
        }
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
            Actions.Stay();
            IsShop = false;
        }
        else if(eventName==Consts.E_EnterScene)
        {
            Debug.Log("收到");
            SceneArgs sceneArgs = data as SceneArgs;

            sceneindex = sceneArgs.SceneIndex ;
 
        }
    }
    #endregion

    #region 对PlayerController的委托
    private void GunYM3Left(Transform obj)
    {
        bullteLeft = obj;
    }

    private void GunYM27(Transform obj)
    {
        YM27 = obj;
    }

    private void GunYM3right(Transform obj)
    {
        bullteRight = obj;
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
        Playerindex = 0;
        PlayerController.GunYM3Right += GunYM3right;
        PlayerController.GunYM3Left += GunYM3Left;
        PlayerController.GunYM27 += GunYM27;
        // SelectedEffect = GameObject.Find("SelectedEffect");
    }


    // Update is called once per frame
    void Update()
    {
        TargeEffectShow();
        //冷却计时器
        Timer += Time.deltaTime;
        if (Timer >= 1f / ShootRate)
        {
            Colder = false;
            Timer = 0;
        }

        
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (IsShop)
            {
                SendEvent(Consts.E_ExitShop);
                IsShop = false;
            }
            else
            {
                Tile tile = mapModel.GetTile(transform.position);
                if (!tile.IsPath && tile.Data == null)
                {
                    SendEvent(Consts.E_BugTower);
                    IsShop = true;
                    transform.position = tile.Position;
                }
            }
        }
        if (IsShop)
        {
            return;
        }
        GetSomeKey();

        Vector3 dir = ShootOrWalk();
        AdjustmentDirection(dir);
    }

    private Vector3 ShootOrWalk()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (!IsJump)
        {
            if (Mathf.Abs(x) + Mathf.Abs(z) < 0.02f)
            {
                if (IsWalk)
                {
                    Actions.Stay();
                    IsWalk = false;

                }
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Actions.Run();
                Speed = 3f;
                IsWalk = true;
                if (Shoot)
                {
                    Shoot = false;
                }

            }
            else
            {
                Actions.Walk();
                IsWalk = true;
                if (Shoot)
                {
                    Shoot = false;
                }
            }
        }
        Vector3 dir = new Vector3(x, 0, z);

        if (IsWalk)
        {
            transform.position += dir * Time.deltaTime * Speed;
            if (Playerindex == 3)
            {
                YM27.GetComponent<XLine>().Close();
            }
        }
        else if (Shoot)
        {
            if (targe != null)
            {
                dir = targe.Position - transform.position;
                dir.y = 0;
                float dis = Mathf.Sqrt(dir.x * dir.x + dir.z * dir.z);
                if (dis > Distance)
                {
                    Close = true;
                }
                if (Close)
                {
                    dir = Vector3.Normalize(dir) * 1.4f;
                    transform.position += dir * Time.deltaTime * Speed;
                    Actions.Walk();
                    float s = Distance * 2f / 3f;
                    if (Playerindex == 0)
                    {
                        s = 0.1f;
                    }
                    if (dis <= s)
                    {
                        Close = false;
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

        return dir;
    }
    /// <summary>
    /// 获取按键
    /// </summary>
    private void GetSomeKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Actions.Jump();
            Invoke("JumpBack", 1f);
            IsJump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Actions.Sitting();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Playerindex++;
            PlayerController.SetArsenal(PlayerController.arsenal[Playerindex].name);
        }
        else if (Input.GetMouseButton(0))//左键射击
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer("MonsterOrMess")))
            {
                targe = hit.collider.gameObject.GetComponent<Role>();
                targe.Dead += Targe_Dead;
                SendEvent(Consts.E_SelectObject, targe);
            }
            if (!Shoot)
            {
                GetTarge();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Actions.Aiming();
        }
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
    /// 实列化子弹
    /// </summary>
    private void InstanceBullte()
    {
        if (Playerindex == 1)
        {
            GameObject bu = Game.Instance.ObjectPool.Spawn("Ym3Bullet");
            bu.transform.position = bullteRight.position;
            bu.GetComponent<YM3Bullet>().Load(2, 1, targe,this);
        }
        else if (Playerindex == 2)
        {
            GameObject bu = Game.Instance.ObjectPool.Spawn("Ym3Bullet");
            if (IsRight)
            {
                bu.transform.position = bullteRight.position;
            }
            else
            {
                bu.transform.position = bullteLeft.position;
            }
            bu.GetComponent<YM3Bullet>().Load(2, 1, targe,this);
            IsRight = !IsRight;
        }
        else if (Playerindex == 3)
        {
            Vector3 offet = targe.Position + new Vector3(0, 0.2f, 0) - YM27.position;
            float angle = Vector3.Angle(offet, Vector3.up);
            YM27.localEulerAngles = new Vector3(angle, 0, 0);
            YM27.GetComponent<XLine>().CanShoot();
        }
    }
    /// <summary>
    /// 显示选中怪物的效果
    /// </summary>
    private void TargeEffectShow()
    {
        if (sceneindex==2)
        {
            return;
        }
        if (targe != null)
        {
            SelectedEffect.transform.position = targe.Position + new Vector3(0, 0.1f, 0);
        }
        else
        {
            SelectedEffect.transform.position = new Vector3(0, -5, 0);
        }
    }
    /// <summary>
    /// 得到一个目标
    /// </summary>
    private void GetTarge()
    {
        Spawner spawner = GetModel<Spawner>();
        if (spawner.ListMonsters.Count > 0)
        {
            targe = spawner.ListMonsters[0];//这里可能要改
            Shoot = true;
            targe.Dead += Targe_Dead;
        }
        else
        {
            targe = null;
            Actions.Attack();
        }
    }

    private void Targe_Dead(Role obj)
    {
        GetTarge();
    }

    private void JumpBack()
    {
        IsJump = false;
    }


}
