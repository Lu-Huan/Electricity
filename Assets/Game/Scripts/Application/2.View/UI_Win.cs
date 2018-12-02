using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Win : View
{

    public override string Name
    {
        get
        {
            return Consts.V_Win;
        }
    }

    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("win");
    }


    public override void HandleEvent(string eventName, object data)
    {
        throw new System.NotImplementedException();
    }
}
