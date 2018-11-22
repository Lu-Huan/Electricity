using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Role
{
    public void Load(int BoxID,Vector3 pos)
    {
        transform.position = pos;
        BoxInfo boxInfo =StaticData.Instance.GetBoxInfo(BoxID);
        MaxHp = boxInfo.MaxHp;
        Hp = MaxHp;
        HpChanged += Mess_HpChanged;
    }
    private void  Mess_HpChanged(int arg1, int arg2)
    {
        GetHPBar();
        Hp = Hp;
        HpChanged -= Mess_HpChanged;
    }
    public override void OnDead(Role role)
    {
        base.OnDead(role);
        gameObject.SetActive(false);
    }
}
