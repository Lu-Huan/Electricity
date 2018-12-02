using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShopGun:MonoBehaviour
{
    public int Id;
    private Transform Gun;
    private string GunName;
    public int Attack;
    public float ShootRate;
    public float Accurate;
    public float Distance;
    public int Price;
    public string Imformation;
    public int Char;
    private void Awake()
    {
        Gun = transform;
        GunName = gameObject.name;
    }
}


