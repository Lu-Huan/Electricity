using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game1 : View
{

    private Button Man;
    private Button Monster;

    private Text ElectricEnergy;

    public override string Name
    {
        get
        {
            return Consts.V_Game1;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        ElectricEnergy = transform.Find("ElectricEnergy").transform.Find("value").GetComponent<Text>();
        ElectricEnergy.text = PlayerPrefs.GetInt("ElectricEnergy", -1).ToString();

        Monster = transform.Find("Monster").GetComponent<Button>();
        Man = transform.Find("Man").GetComponent<Button>();
        Man.onClick.AddListener(() =>
        {
            Game.Instance.LoadScene(5);
        });
        Monster.onClick.AddListener(() =>
        {
            Game.Instance.LoadScene(6);
        });

    }

    // Update is called once per frame
    
    public void Exit()
    {
        Application.Quit();
    }
}
