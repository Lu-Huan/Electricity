using UnityEngine;
using UnityEngine.UI;

public class UI_Statistics : View
{
    private int gold = 0;
    private float Cur=0;
    bool IsStart = false;
    public int Gold
    {
        set
        {
            gold = value;
            if (value != 0)
            {
                IsStart = true;
            }
        }
        get
        {
            return gold;
        }
    }
    public Text GoldText;
    public Text[] MonsterNum;

    public override void RegisterEvents()
    {
        //AttentionEvents.Add(Consts.E_EndLevel);
    }

    public override string Name
    {
        get
        {
            return Consts.V_Statistics;
        }
    }
    private void OnEnable()
    {
            Spawner spawner = GetModel<Spawner>();
            MonsterNum[0].text = "X   " + spawner.Already_dead[0];
            MonsterNum[1].text = "X   " + spawner.Already_dead[1];
            Gold = GetModel<GameModel>().Gold;
    }
    private void Update()
    {
        if (IsStart)
        {
            Cur= Mathf.Lerp(Cur, gold + 1, Time.deltaTime*2);
            if (Mathf.Abs(Cur - gold) < 0.5f)
            {
                Cur = gold;
                IsStart = false;
            }
            GoldText.text = " + " + (int)Cur;
        }
    }
    public override void HandleEvent(string eventName, object data)
    {

    }
}

