using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : View
{
    public float Speed=2;
    //private Animation Animation;
    private bool IsWalk = true;
    private bool IsShop = false;
    private bool IsJump=false;
    MapModel mapModel;
    Actions Actions;
    PlayerController PlayerController;
    int index=0;
    int Playerindex
    {
        set
        {
            index = value% PlayerController.arsenal.Length;
        }
        get
        {
            return index;
        }
    }


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
    }
    public override void HandleEvent(string eventName, object data)
    {
        if (eventName == Consts.E_SpawnTower)
        {
            //Animation.Play("idle");
            Actions.Stay();
            IsShop = false;
        }
        else if(eventName==Consts.E_ExitShop)
        {
            Actions.Stay();
            IsShop = false;
        }
    }

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
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetMouseButtonDown(1))
        {
            Playerindex++;
            PlayerController.SetArsenal(PlayerController.arsenal[Playerindex].name);
            
        }
        if (IsShop)
        {

            return;
        }
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
        if (Input.GetMouseButton(0))
        {
            Actions.Attack();
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (!IsJump)
        {
            if (Mathf.Abs(x) + Mathf.Abs(z) < 0.1f)
            {
                if (IsWalk)
                {
                    //Animation.Play("idle");
                    Actions.Stay();
                    IsWalk = false;
                }
                return;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Actions.Run();
                Speed = 3f;
                IsWalk = true;
            }
            else
            {
                Actions.Walk();
                IsWalk = true;
            }
        }
        Vector3 dir = new Vector3(x, 0, z);
        Vector3 Start = new Vector3(0, 0, 1);
        transform.position += dir * Time.deltaTime * Speed;

        float angle = Vector3.Angle(Start, dir); //求出两向量之间的夹角
        Vector3 normal = Vector3.Cross(Start, dir);//叉乘求出法线向量  
        angle *= Mathf.Sign(Vector3.Dot(normal, new Vector3(0, 1, 0)));  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向 
        //float current= Mathf.Lerp(transform.localRotation.y, angle, 0.5f);
        transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
    private void JumpBack()
    {
        IsJump = false;
    }
}
