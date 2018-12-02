using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Start1 : View {
    public override string Name
    {
        get
        {
            return Consts.V_Start1;
        }
    }
    bool IsStart=false;
    public void Start1()
    {
        int value= PlayerPrefs.GetInt("ElectricEnergy", -1);
        /*if (value==-1)
        {
            Debug.Log("首次创建");
            PlayerPrefs.SetInt("ElectricEnergy", 100);
            value = PlayerPrefs.GetInt("ElectricEnergy", -1);
        }
            Debug.Log(value);*/
        
        Debug.Log(value);
        GameModel gameModel = GetModel<GameModel>();
        gameModel.Gold = 0;
        SendEvent(Consts.E_CreatMap);
        Game.Instance.LoadScene(2);
    }
    public void Update()
    {
        if (IsStart)
        {
            return;
        }
        if (Input.touchCount!=0)
        {
            IsStart = true;
            Start1();
        }
    }
    public override void HandleEvent(string eventName, object data)
    {
        
    }
}
