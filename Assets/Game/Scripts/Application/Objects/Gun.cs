using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public enum GunType
{
    FreeHand,
    OnePistol,
    TwoPistol,
    Rifle
}
[Serializable]
public  class Gun : MonoBehaviour
{
    public int ID;
    public string Name;
    public GunType GunType;
    public virtual void Shoot(Role Target) { }
}

