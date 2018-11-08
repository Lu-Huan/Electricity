using UnityEngine;
using UnityEditor;

public class NextRoundCommand : Controller
{
    public override void Execute(object data)
    {
        RoundModel roundModel = GetModel<RoundModel>();
        roundModel.StartRound();
    }
}