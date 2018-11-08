using UnityEngine;
using UnityEditor;

public class Damage : Controller
{

    public override void Execute(object data)
    {
        
        Monster monster = data as Monster;
        Debug.Log(monster.name + "造成伤害:" + monster.damage);
        RoundModel roundModel = GetModel<RoundModel>();
        roundModel.End.Damage(monster.damage);
    }
}