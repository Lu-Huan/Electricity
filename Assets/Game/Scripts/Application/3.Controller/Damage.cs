using UnityEngine;
using UnityEditor;

public class Damage : Controller
{

    public override void Execute(object data)
    {
        
        Monster monster = data as Monster;
        RoundModel roundModel = GetModel<RoundModel>();
        roundModel.End.Damage(monster.damage);
    }
}