using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Monster0 : Monster
{
    private Animator ani;
    Dictionary<Tower, int> towers = new Dictionary<Tower, int>();
    public Transform Body;
    public Transform Left;
    public Transform Right;
    public Tower Targe;
    private float angle;
    private float cur = 0;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        //Body = transform.Find("botBody");
    }
    private void OnEnable()
    {
        GetComponent<Role>().Dead += Monster0_Dead;
        GetComponent<Monster>().Attacked += Monster0_Attacked;
        Body.transform.localEulerAngles = new Vector3(0, 0, 0);


    }

    private void Monster0_Attacked(Tower from, int hit)
    {
        if (towers.ContainsKey(from))
        {
             towers[from] += hit;
        }
        else
        {
            towers.Add(from, hit);
        }
        if (Targe == null && towers[from] >= MaxHp / 3)
        {
            IsAttack = true;
            ani.SetBool("Attack", true);
            Targe = from;
            Targe.Dead += Targe_Dead;
            GameObject angry = Game.Instance.ObjectPool.Spawn("UI_angry");
            angry.transform.SetParent(GameObject.Find("Canvas").transform);
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            angry.transform.position = pos + new Vector2(-40, 50);
            Rotate();
        }
    }

    private void Targe_Dead(Role obj)
    {
        towers.Remove(obj.GetComponent<Tower>());
        Targe = null;
        IsAttack = false;
        ani.SetBool("Attack", false);
        cur = 0;
    }

    private void Rotate()
    {
        Debug.Log("修正方向");
        angle = transform.localEulerAngles.y;
        Vector3 Orientation;
        if (angle == 90)
        {
            Orientation = new Vector3(0, 0, -1);
        }
        else if (angle == -90)
        {
            Orientation = new Vector3(0, 0, 1);
        }
        else if (angle == 180)
        {
            Orientation = new Vector3(-1, 0, 0);
        }
        else
        {
            Orientation = new Vector3(1, 0, 0);
        }
        Vector3 dir = Targe.transform.position - transform.position;
        dir.y = 0;
        cur = 0;
        angle = Vector3.Angle(dir, Orientation);
        Vector3 normal = Vector3.Cross(dir, Orientation);//叉乘求出法线向量  
        angle *= Mathf.Sign(Vector3.Dot(normal, Vector3.down));  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向 
    }
    protected override void Update()
    {
        if (IsAttack)
        {
            if (Mathf.Abs(angle - cur) > 5)
            {
                cur = Mathf.Lerp(cur, angle, Time.deltaTime * 3);
                Body.transform.localEulerAngles = new Vector3(0, cur, 0);
            }
        }
        else
        {
            Body.localEulerAngles = Vector3.Lerp(new Vector3(0, 0, 0), Body.localEulerAngles, Time.deltaTime * 2);
            base.Update();
        }
    }

    private void Shoot()
    {
        if (Targe != null)
        {
            GameObject m1 = Game.Instance.ObjectPool.Spawn("Rocket01_Red");

            GameObject m2 = Game.Instance.ObjectPool.Spawn("Rocket01_Red");
            m1.transform.position = Left.position;
            m2.transform.position = Right.position;

            m1.GetComponent<Missile>().Load(3, 1, Targe, null);
            m2.GetComponent<Missile>().Load(3, 1, Targe, null);
        }
        else
        {
            IsAttack = false;
            ani.SetBool("Attack", false);
            cur = 0;
        }
    }
    private void SerchTarge()
    {

    }
    private void Monster0_Dead(Role obj)
    {
        
        Targe = null;

        //遍历Value
        towers.Clear();
        ani.SetTrigger("Dead");
    }
}