using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;

public class MapV : View
{
    //地板预制件
    public GameObject End;
    private GameObject end;
    public GameObject path;
    //箭头预制件
    public GameObject Arrow;

    public GameObject Door;
    private GameObject door;


    public GameObject Plane;
    public GameObject[] Mess;

    public Transform Player;




    public bool DrawGizmos = false;
    private void Awake()
    {
        MapModel mapModel = GetModel<MapModel>();
        mapModel.TileHeight = TileHeight;
        mapModel.TileWidth = TileWidth;
        mapModel.End = End;
        mapModel.path = path;
        //箭头预制件
        mapModel.Arrow = Arrow;
        mapModel.Door = Door;
        mapModel.Plane = Plane;
        mapModel.Box = Mess;
        mapModel.Player = Player;
    }
    #region Unity回调
    /// <summary>
    /// 辅助画线
    /// </summary>
    void OnDrawGizmos()
    {
        if (!DrawGizmos)
            return;

        //格子颜色
        Gizmos.color = Color.green;

        //绘制行
        for (int row = 0; row <= MapHeight; row++)
        {
            Vector3 from = new Vector3(-TileWidth / 2, 0, -TileHeight / 2 + row * TileHeight);
            Vector3 to = new Vector3(-TileWidth / 2 + MapWidth, 0, -TileHeight / 2 + row * TileHeight);
            Gizmos.DrawLine(from, to);
        }

        //绘制列
        for (int col = 0; col <= MapWidth; col++)
        {
            Vector3 from = new Vector3(-TileWidth / 2 + col * TileWidth, 0, MapHeight - TileHeight / 2);
            Vector3 to = new Vector3(-TileWidth / 2 + col * TileWidth, 0, -TileHeight / 2);
            Gizmos.DrawLine(from, to);
        }
    }
    #endregion

    public override string Name
    {
        get
        {
            return Consts.V_MapV;
        }
    }

    int MapHeight { get; set; }
    int MapWidth { get; set; }

    int TileHeight { get; set; }
    int TileWidth { get; set; }
    #region 事件
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterShop:
                DrawGizmos = true;
                break;
            case Consts.E_ExitShop:
                DrawGizmos = false;
                break;
        }
    }
    #endregion
}