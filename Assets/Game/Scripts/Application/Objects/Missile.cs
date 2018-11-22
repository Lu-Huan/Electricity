using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Missile:Bullet
{
    Tower Target;
    public GameObject Point;
    Vector3 Direction;
    public override void Load(int bulletID, int level, object tower, object from)
    {

        ID = bulletID;
        Level = level;
        Target = (Tower)tower;
        BulletInfo info = Game.Instance.StaticData.GetBulletInfo(bulletID);
        BaseSpeed = info.BaseSpeed;
        BaseDamage = info.BaseDamage;
        GetAngle();
    }
    private void GetAngle()
    {
        Vector3 dir = Target.transform.position-transform.position;
        dir.y = 0;
        Vector3 Start = new Vector3(1, 0, 0);
        float angle = Vector3.Angle(Start, dir); //求出两向量之间的夹角
        angle *= Mathf.Sign(-dir.z);  //修正旋转方向 
        //transform.Rotate(Vector3.up, angle);
        transform.localEulerAngles = new Vector3(0, angle, -70);
    }
    protected override void Update()
    {
        //已爆炸无需跟踪
        if (m_IsExploded)
            return;

        //目标检测
        if (Target == null)
        {
            return;
        }
        transform.position += Direction * Speed * Time.deltaTime;

        Direction = (Target.Position + new Vector3(0, 0.5f, 0) - transform.position).normalized;
        //打中目标
        if (Vector3.Distance(transform.position, Target.transform.position) <= Consts.DotClosedDistance)
        {
            //敌人受伤
            Target.Damage(null, Demage);

            //爆炸
            Explode();

            Point.SetActive(true);
        }
    }
}

