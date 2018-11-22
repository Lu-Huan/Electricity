using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnterSceneComand : Controller
{
    public override void Execute(object data)
    {
        SceneArgs e = data as SceneArgs;
        Debug.Log("进入场景" + e.SceneIndex);
        //注册视图（View）
        MapModel mapModel;
        switch (e.SceneIndex)
        {
            case 0: //Init

                break;
            case 1://Start
                break;
            case 2:
                RegisterView(GameObject.Find("Character").GetComponent<MainCharacter>());
                break;
            case 3://Level
                GameObject Character = GameObject.Find("Character");
                RegisterView(GameObject.Find("Map").GetComponent<MapV>());
                RegisterView(GameObject.Find("Canvas").GetComponent<UI_Game2>());
                RegisterView(Character.GetComponent<MainCharacter>());

                GetModel<GameModel>().MainCharater = Character;
                mapModel =GetModel<MapModel>();

                GetModel<Spawner>().mapM = mapModel;
                mapModel.Draw();
                break;
        }
    }
}