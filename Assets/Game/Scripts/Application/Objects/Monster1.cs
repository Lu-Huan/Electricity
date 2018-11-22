using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Monster1 : Monster
{
    private Animation ani;
    private void Start()
    {
        ani = GetComponent<Animation>();

    }
    private void OnEnable()
    {
        Dead += Monster1_Dead;
        Attacked += Monster1_Attacked;
    }

    private void Monster1_Attacked(Tower arg1, int arg2)
    {
        if (Hp <= MaxHp * 2 / 3)
        {
            ani["walk"].speed = 1.5f;
            m_MoveSpeed = 1.5f;
        }
        else if (Hp <= MaxHp / 3)
        {
            ani["walk"].speed = 2f;
            m_MoveSpeed = 2f;
        }
    }

    private void Monster1_Dead(Role obj)
    {
        ani["walk"].speed = 1f;
        ani.Play("Monster1Dead");
        Invoke("WaitDeadanimation", 1f);
    }
}