using UnityEngine;
using System.Collections;
//This script executes commands to change character animations
public class Tower0_Actions : Tower
{
    private Transform head;
    private Animator animator;
    //private Quaternion rotate_State;
    private bool bool_fire;
    private void Awake()
    {
        head = transform.Find("acs17_part1_Gun0").Find("head");
        animator = transform.Find("acs17_part1_Gun0").GetComponent<Animator>();
        Tower0_Ser Tower1_Ser = GetComponent<Tower0_Ser>();
        Tower1_Ser.IN += Tower1_Ser_IN;
        Tower1_Ser.OUT += Tower1_Ser_OUT;
        Dead += Tower0_Actions_Dead;
    }

    private void Tower0_Actions_Dead(Role obj)
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
        base.Shot(monster);
    }
}