using UnityEngine;
using System.Collections;

public class YM3Bullte : Bullet
{
    //目标
    public Monster Target { get; private set; }

    //移动方向
    private Vector3 Direction;
    public GameObject Point;
    //线渲染器
    LineRenderer lineRenderer;
    public override void Load(int bulletID, int level, Monster monster)
    {
        lineRenderer = GetComponent<LineRenderer>();
        ID = bulletID;
        Level = level;
        Target = monster;
        BulletInfo info = Game.Instance.StaticData.GetBulletInfo(bulletID);
        BaseSpeed = info.BaseSpeed;
        BaseAttack = info.BaseAttack;
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
        lineRenderer.SetPosition(1, Direction * 0.15f);
        //打中目标
        if (Vector3.Distance(transform.position, Target.transform.position) <= Consts.DotClosedDistance)
        {
            //敌人受伤
            Target.Damage(Demage);

            //爆炸
            Explode();

            /*GameObject po = Instantiate(Point);
            po.transform.position = transform.position;*/
            /*Point.transform.position = transform.position;*/
            Point.SetActive(true);
        }
    }
}