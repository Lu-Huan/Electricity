
using UnityEngine;

class CountDownCompleteCommand : Controller
{
    public override void Execute(object data)
    {
        GetModel<Spawner>().Already_dead[0] = 0;
        GetModel<Spawner>().Already_dead[1] = 0;
        Debug.Log("开始出怪");
        //开始出怪
        MapModel mapModel = GetModel<MapModel>();

        RoundModel rModel = GetModel<RoundModel>();
        rModel.InitEnd(mapModel.end.GetComponent<Electricity>());
        rModel.StartRound();
    }
}