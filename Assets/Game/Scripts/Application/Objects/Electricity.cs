using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Electricity : Role
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    #endregion

    #region 属性
    #endregion

    #region 方法
    public override void Damage(int hit)
    {
        if (!IsDead)
        {
            //m_Animator.SetTrigger("IsDamage");
            Debug.Log("能源受伤");
        }
        base.Damage(hit);
    }

   protected override void OnDead(Role role)
    {
        Debug.Log("终点死亡");
        //m_Animator.SetBool("IsDead", true);
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void OnSpawn()
    {
        base.OnSpawn();
       /* m_Animator = GetComponent<Animator>();
        m_Animator.Play("Idle");

        //LuoboInfo info = Game.Instance.StaticData.GetLuoboInfo();
        //MaxHp = info.Hp;
        //Hp = info.Hp;*/
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
        /*m_Animator.ResetTrigger("IsDamage");
        m_Animator.SetBool("IsDead", false);*/
    }
    #endregion

    #region 帮助方法
    #endregion
}