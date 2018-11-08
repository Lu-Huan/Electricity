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

        //注册视图（View）
        switch (e.SceneIndex)
        {
            case 0: //Init

                break;
            case 1://Start
                break;
            case 2://Select
                break;
            case 3://Level
                GameObject Character = GameObject.Find("Character");
                RegisterView(GameObject.Find("Map").GetComponent<MapV>());
                RegisterView(GameObject.Find("Canvas").GetComponent<UI_Game2>());
                RegisterView(Character.GetComponent<MainCharacter>());
                GetModel<GameModel>().MainCharater = Character;
                MapModel mapModel =GetModel<MapModel>();
                GetModel<Spawner>().mapM = mapModel;
                mapModel.Draw();
                break;
            case 4:
                break;
        }
    }
}