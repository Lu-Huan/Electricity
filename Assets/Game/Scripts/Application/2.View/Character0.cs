using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Character0 : MainCharacter
{
    private Transform YM27;//换
    /// <summary>
    /// 实列化子弹
    /// </summary>
    protected override void InstanceBullte()
    {
        if (Gunindex == 1)
        {
            GameObject bu = Game.Instance.ObjectPool.Spawn("Ym3Bullet");
            bu.transform.position = bullteRight.position;
            bu.GetComponent<YM3Bullet>().Load(2, 1, Target, this);
        }
        else if (Gunindex == 2)
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
            bu.GetComponent<YM3Bullet>().Load(2, 1, Target, this);
            IsRight = !IsRight;
        }
        else if (Gunindex == 3)
        {
            Vector3 offet = Target.Position + new Vector3(0, 0.2f, 0) - YM27.position;
            float angle = Vector3.Angle(offet, Vector3.up);
            YM27.localEulerAngles = new Vector3(angle, 0, 0);
            YM27.GetComponent<XLine>().CanShoot();
        }
    }

    private void Start()
    {
        PlayerController.GunYM3Right += GunYM3right;
        PlayerController.GunYM3Left += GunYM3Left;
        PlayerController.GunYM27 += GunYM27;
    }
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
}

