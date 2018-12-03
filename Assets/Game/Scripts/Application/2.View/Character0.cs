using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Character0 : MainCharacter
{
    //RuntimeAnimatorController
    private Transform YM27;
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
            angle = Mathf.Clamp(angle, 90, 100);
            YM27.localEulerAngles = new Vector3(angle, 0, 0);
            YM27.GetComponent<XLine>().CanShoot();
        }
    }

    private void Start()
    {
        //PlayerController.arsenal.Add(new Arsenal())
    }
    #region 得到位置
    protected override void ChangeGun()
    {
        base.ChangeGun();

    }
    private void GunYM3Left()
    {
        bullteLeft = PlayerController.leftGunBone.GetChild(0).Find("Start");
    }
    private void GunYM27()
    {
        YM27 = PlayerController.rightGunBone.GetChild(0);
    }

    private void GunYM3right()
    {
        bullteRight = PlayerController.rightGunBone.GetChild(0).Find("Start");
    }
     #endregion
}

