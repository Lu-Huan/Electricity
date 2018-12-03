using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : View {

    public List<MainCharacter> mainCharacters;
    public override string Name
    {
        get
        {
           return  Consts.V_CharController;
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_ChangeMainCharater);
    }
    public override void HandleEvent(string eventName, object data)
    {
        if (eventName==Consts.E_ChangeMainCharater)
        {
            SetChar();
        }
    }

    private void SetChar()
    {
        int index = GetModel<GameModel>().Current_role;
        for (int i = 0; i < mainCharacters.Count; i++)
        {
            mainCharacters[i].enabled = (i == index);
        }
    }

    // Use this for initialization
    void Awake () {
        SetChar();
    }
}
