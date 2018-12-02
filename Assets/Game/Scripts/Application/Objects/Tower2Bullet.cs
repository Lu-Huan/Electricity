using System.Collections.Generic;
using UnityEngine;

public class Tower2Bullet : Bullet
{
    public Monster Target;
    public Tower From;
    private float m_uptimer = 0;
    public float m_RotSpeed;
    List<Monster> RangeMonster=new List<Monster>();
    Vector3 posGround;
    void Start()
    {

    }

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
        if (m_IsExploded)
        {
            return;
        }
        if (Target==null|| Vector3.Distance(transform.position, Target.Position) < 0.2||transform.position.y < 0.2f)
        {
            Explode();
            GameObject go=Game.Instance.ObjectPool.Spawn("Bomb");
            go.transform.position = transform.position+new Vector3(0,0.5f,0);
            Debug.Log(go.transform.position);
            for (int i = 0; i < RangeMonster.Count; i++)
            {
                RangeMonster[i].Damage(From, BaseDamage);
            }
            RangeMonster.Clear();
            BaseSpeed = 4f;
        }
        if (m_uptimer < 0.5f)
        {
            m_uptimer += Time.deltaTime;

            BaseSpeed -=  5f* Time.deltaTime;

            transform.position += transform.forward * BaseSpeed * Time.deltaTime;

        }
        else
        {
            Vector3 target;
            // 追踪
            if (Target == null)
            {
                target = posGround;
            }
            else
            {
                target = (Target.Position - transform.position).normalized;
            }
            posGround = target;
            float a = Vector3.Angle(transform.forward, target) / m_RotSpeed;

            if (a > 0.1f || a < -0.1f)

                transform.forward = Vector3.Slerp(transform.forward, target, Time.deltaTime / a).normalized;

            else
            {
                BaseSpeed +=2* Time.deltaTime;

                transform.forward = Vector3.Slerp(transform.forward, target, 1).normalized;
            }
            transform.position += transform.forward * BaseSpeed * Time.deltaTime;


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Monster")
        {
            RangeMonster.Add(other.GetComponent<Monster>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            RangeMonster.Remove(other.GetComponent<Monster>());
        }
    }
}

