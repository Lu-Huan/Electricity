using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_YM3 : Gun {

    public Transform ShootPoint;
    public override void Shoot(Role Target)
    {
        GameObject bu = Game.Instance.ObjectPool.Spawn("Ym3Bullet");
        bu.transform.position = ShootPoint.position;
        bu.GetComponent<YM3Bullet>().Load(2, 1, Target, this);
    }
}
