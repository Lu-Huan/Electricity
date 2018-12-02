using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : Tower
{
    private Transform head;
    private Animator animator;
    public Transform[] ShootPoint;
    int i=0;
    int Index
    {
        set
        {
            i = value % 8;
        }
        get
        {
            return i;
        }
    }

    private bool bool_fire;
    private void Awake()
    {
        head = transform.Find("_B01_z");
        animator = GetComponent<Animator>();
        animator.SetBool("IsAttck", false);
        Tower_Trigger Tower1_Ser = GetComponent<Tower_Trigger>();
        Tower1_Ser.IN += Tower1_Ser_IN;
        Tower1_Ser.OUT += Tower1_Ser_OUT;
        Dead += Tower1_Dead;
    }

    private void Tower1_Dead(Role obj)
    {
        head.gameObject.SetActive(false);
    }
    private void Tower1_Ser_OUT(Monster obj)
    {
        MonstersRemove(obj);
    }

    private void Tower1_Ser_IN(Monster obj)
    {
        MonstersAdd(obj);
    }



    protected override void Update()
    {
        LookAt(m_Target);
    }
    protected override void LookAt(Monster target)
    {
        if (target == null)
        {
            Idle();
            return;
        }
        Vector3 dir = target.Position - transform.position;
        dir.y = 0;
        float angle = Vector3.Angle(dir, new Vector3(0, 0, 1));
        angle *= Mathf.Sign(dir.x);
        head.localEulerAngles = new Vector3(0, 0, angle);
        Fire();
    }
    public void Idle()
    {
        if (bool_fire)
        {
            animator.SetBool("IsAttck", false);
            bool_fire = false;
        }
    }
    public void Fire()
    {
        if (!bool_fire)
        {
            bool_fire = true;
            animator.SetBool("IsAttck", true);
        }
    }
    public void ShootMiisle()
    {
        Shot(m_Target);
    }
    public override void Shot(Monster monster)
    {
        
        Index++;
        GameObject bu= Game.Instance.ObjectPool.Spawn(UseBulletName);
        Vector3 pos= ShootPoint[Index].position;
        Quaternion Rot = ShootPoint[Index].rotation;
        bu.transform.position = pos;
        bu.transform.rotation = Rot;
        bu.GetComponent<Tower2Bullet>().Load(3, 1, monster, null);
    }
}

