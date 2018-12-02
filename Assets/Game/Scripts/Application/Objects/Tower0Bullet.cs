using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class Tower0Bullet : Bullet
{
    public Monster Target;
    public Tower From;
    //移动方向
    private Vector3 Direction;
    public GameObject Point;
    //线渲染器
    public override void Load(int bulletID, int level, object monster, object from)
    {
        ID = bulletID;
        Level = level;
        Target = monster as Monster;
        From = from as Tower;
        BulletInfo info = Game.Instance.StaticData.GetBulletInfo(bulletID);
        BaseSpeed = info.BaseSpeed;
        BaseDamage = info.BaseDamage;
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
        //lineRenderer.SetPosition(1, Direction * 0.3f);
        //打中目标
        if (Vector3.Distance(transform.position, Target.transform.position) <= Consts.DotClosedDistance)
        {
            //敌人受伤
            Target.Damage(From,Demage);

            //爆炸
            Explode();

            Point.SetActive(true);
        }
    }
}
