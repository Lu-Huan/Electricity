using UnityEngine;
using System.Collections;
//This script executes commands to change character animations
public class Tower0_Actions : Tower
{
    private Transform head;
    private Animator animator;
    public Transform ShootPoint;
    //private Quaternion rotate_State;
    private bool bool_fire;
    private void Awake()
    {
        head = transform.Find("acs17_part1_Gun0").Find("head");
        animator = transform.Find("acs17_part1_Gun0").GetComponent<Animator>();
        Tower_Trigger Tower0_Ser = GetComponent<Tower_Trigger>();
        Tower0_Ser.IN += Tower0_Ser_IN;
        Tower0_Ser.OUT += Tower0_Ser_OUT;
        Dead += Tower0_Actions_Dead;
    }

    private void Tower0_Actions_Dead(Role obj)
    {
        head.gameObject.SetActive(false);
    }

    private void Tower0_Ser_OUT(Monster obj)
    {
        MonstersRemove(obj);
    }

    private void Tower0_Ser_IN(Monster obj)
    {
        MonstersAdd(obj);
    }

    void LateUpdate()
    {

    }

    protected override void LookAt(Monster target)
    {
        if (target==null)
        {
            Idle();
            return;
        }
        Vector3 dir = target.Position - transform.position;
        dir.y = 0;
        float angle = Vector3.Angle(dir, new Vector3(1, 0, 0));
        angle *= Mathf.Sign(dir.z);
        head.localEulerAngles = new Vector3(-90, -angle, 0);
        Fire();
    }
    public void Idle()
    {
        if (bool_fire)
        {
            animator.SetTrigger("Idle");
            bool_fire = false;
        }
    }
    public void Fire()
    {
        if (!bool_fire)
        {
            bool_fire = true;
            animator.SetTrigger("Fire");
        }
    }
    public override void Shot(Monster monster)
    {
        GameObject go = Game.Instance.ObjectPool.Spawn(UseBulletName);
        go.transform.position = ShootPoint.position;
        go.GetComponent<Tower0Bullet>().Load(0, 1, monster, this);
    }
}