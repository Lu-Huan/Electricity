using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Collections;

public class MapModel : Model
{
    public MapModel()
    {
        CreateTile();
    }
    #region 常量
    public  int MapWidth = Consts.MapWidth;    //地图宽
    public  int MapHeight = Consts.MapHeight;  //地图高
    #endregion

    #region 字段
    public float TileWidth = 1;//格子宽
    public float TileHeight = 1;//格子高

    private List<Tile> MapTiles = new List<Tile>();//格子集合
    private List<Vector3> MapPath = new List<Vector3>();//路径集合
    private int Complexity;

    public int complexity
    {
        set
        {
            Complexity = Mathf.Clamp(value, 0, 100);
        }
        get
        {
            return Complexity;
        }
    }



    //地板预制件
    public GameObject End;
    public GameObject end;
    public GameObject path;
    //箭头预制件
    public GameObject Arrow;

    public GameObject Door;
    private GameObject door;


    public GameObject Plane;
    public GameObject[] Box;

    public Transform Player;




    public int RoadLength = 35;


    #endregion

    #region 对外接口
    /// <summary>
    /// 生成一条路径
    /// </summary>
    /// <param name="legth">路径长</param>
    /// <param name="complexity">复杂度</param>
    public void LoadMap(int legth, int complexity)
    {
        RoadLength = legth;
        Complexity = complexity;
        //初始化格子
        InitTile();
        //清除当前状态
        Clear();

        //得到随机路径
        CreateRandomPath();

        //设置怪物的路径
        SetTilePath();
    }

    //获得格子的方法
    //根据位置获得索引 
    public Tile GetTile(Vector3 position)
    {
        //对主角坐标进行转换
        int tileX = Mathf.RoundToInt(position.x);
        int tileY = Mathf.RoundToInt(position.z);
        
        return GetTile(tileX, tileY);
    }

    public Tile GetTile(int tileX, int tileY)
    {
        //计算索引
        //根据格子索引号获得格子
        int index = tileX * MapHeight + tileY;
        if (index < 0 || index >= MapTiles.Count)
            throw new IndexOutOfRangeException("格子索引越界");
        return MapTiles[index];
    }

    public List<Vector3> GetMapPath
    {
        get
        {
            return MapPath;
        }

    }

    public List<Tile> getsetMapTiles
    {
        set
        {
            MapTiles = value;
        }
        get
        {
            return MapTiles;
        }

    }

    public override string Name
    {
        get
        {
            return Consts.M_MapModel;
        }
    }
    public void Draw()
    {
        //开启协程

        SetGround();
        //Game.Instance.StartCoroutine(SetPlayer());//设置主角

        Game.Instance.StartCoroutine(SetArrow());//设置路径箭头

        //Game.Instance.StartCoroutine(SetGround());//设置地图杂物   
       
    }
    #endregion

    #region 内部封装

    //用于深搜的数据
    private int CurrentLength = 0;//当前长度

    private int[,] Path = new int[Consts.MapWidth, Consts.MapHeight];//路径表

    public int[,] dir = new int[4, 2] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };//方向

    private Stack<Vector2> Road = new Stack<Vector2>();//用栈来存路径

    /// <summary>
    /// 产生随机路径
    /// 算法入口
    /// </summary>
    private void CreateRandomPath()
    {

        MapInit();

        int x = UnityEngine.Random.Range(1, MapWidth);
        Thread.Sleep(200);

        int y = UnityEngine.Random.Range(1, MapHeight);
        //= CreateRandomPoint(true);
        /*Vector2 End = CreateRandomPoint(false, (int)Start.x);

        Path[(int)End.x, (int)End.y] = 2;*/
        DFS_Start(x, y);
        //Debug.LogFormat("开始:({0},{1})",x,y);
        //Debug.Log(End);
        Array array = Road.ToArray();
        Road.Clear();
        List<Vector3> Pa = new List<Vector3>();
        foreach (Vector2 item in array)
        {
            Pa.Add(new Vector3(item.x, 0, item.y));
        }
        for (int i = 0; i < Pa.Count - 1; i++)
        {
            //Debug.Log("Pa"+Pa[i]);
            Vector3 v1 = Pa[i];
            Vector3 v2 = Pa[i + 1];
            MapPath.Add(v1);
            if (Mathf.Abs((v1 - v2).x + (v1 - v2).z) == 2)
            {
                MapPath.Add((v2 + v1) / 2);
            }
        }
        if (Pa.Count > 1)
        {
            MapPath.Add(Pa[Pa.Count - 1]);
        }

        SetTilePath();
    }

    /// <summary>
    /// 设置怪的路径
    /// </summary>
    private void SetTilePath()
    {
        foreach (Vector3 item in MapPath)
        {
            Tile tile = GetTile((int)item.x, (int)item.z);
            tile.IsPath = true;
        }
    }

    /// <summary>
    /// 生成随机点（添加限制）
    /// </summary>
    /// <param name="frist"></param>
    /// <param name="x1"></param>
    /// <returns></returns>
    private Vector2 CreateRandomPoint(bool frist, int x1 = 0)
    {
        Vector2 Point = new Vector2();
        int t = UnityEngine.Random.Range(0, 4);
        Thread.Sleep(200);
        int x;
        if (frist)
        {
            x = UnityEngine.Random.Range(0, MapWidth);
        }
        else if (x1 < MapWidth / 3)
        {
            x = UnityEngine.Random.Range(MapWidth / 2, MapWidth);
        }
        else
        {
            x = UnityEngine.Random.Range(0, MapWidth / 2);
        }
        Thread.Sleep(200);
        int y = UnityEngine.Random.Range(0, MapHeight);
        Thread.Sleep(200);
        switch (t)
        {
            case 0:
                Point = new Vector2(0, y);
                break;
            case 1:
                Point = new Vector2(x, 0);
                break;
            case 2:
                Point = new Vector2(MapWidth - 1, y);
                break;
            case 3:
                Point = new Vector2(x, MapHeight - 1);
                break;
            default:
                throw new IndexOutOfRangeException("随机数异常");
        }
        return Point;
    }

    /// <summary>
    /// 清除所有信息
    /// </summary>
    private void Clear()
    {
        foreach (Tile t in MapTiles)
        {
            if (t.IsPath)
                t.IsPath = false;
        }
        MapPath.Clear();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void MapInit()
    {
        CurrentLength = 0;
        Road.Clear();
        for (int i = 0; i < MapWidth; i++)
            for (int j = 0; j < MapHeight; j++)
                Path[i, j] = 0;
        Clear();
    }

    /// <summary>
    /// 设置搜索模式（+1或+2）
    /// </summary>
    /// <returns></returns>
    private int RandomMode()
    {
        int mode = UnityEngine.Random.Range(0, 100);
        if (mode < Complexity)
        {
            mode = 1;
        }
        else
        {
            mode = 2;
        }
        return mode;
    }

    /// <summary>
    /// 判断是否可走
    /// </summary>
    /// <param name="X">坐标x</param>
    /// <param name="Y">坐标y</param>
    /// <returns>返回bool值</returns>
    private bool Check(int X, int Y)
    {
        if (X > 0 && X < MapWidth - 1 && Y > 0 && Y < MapHeight - 1 && Path[X, Y] != 1)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 输入一个点(x,y)，返回周围可走的区域
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int[] SerchAround(int x, int y, int mode)
    {
        int[] a = new int[5];
        int t = 0;
        for (int i = 0; i < 4; i++)
        {
            if (Check(x + dir[i, 0], y + dir[i, 1]))
            {
                if (mode == 2)
                {
                    if (Check(x + dir[i, 0] * mode, y + dir[i, 1] * mode))
                    {
                        a[t] = i;
                        t++;
                    }
                    else
                        continue;
                }
                else
                {
                    a[t] = i;
                    t++;
                }

            }
        }
        a[4] = t;
        // Debug.LogFormat("查:({0},{1}),有{2}个可走", x, y, t);
        /*for (int i = 0; i < t; i++)
        {
            switch (a[i])
            {
                case 0:
                    Debug.Log("上");
                    break;
                case 1:
                    Debug.Log("下");
                    break;
                case 2:
                    Debug.Log("右");
                    break;
                case 3:
                    Debug.Log("左");
                    break;
            }

        }*/
        return a;
    }

    /// <summary>
    /// 深度搜索
    /// 新的点（X,Y）
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <returns></returns>
    private bool DFS_Start(int X, int Y)
    {
        int Mode = RandomMode();
        //Debug.LogFormat("({0},{1})", x, y);
        Path[X, Y] = 1;

        /*if (Mathf.Abs(x2-x1+y2-y1) == 2)
        {
            //Debug.Log("中间："+new Vector2((x2 + x1) / 2, (y2 + y1) / 2));
            Path[(x2 + x1) / 2, (y2 + y1) / 2] = 1;
        }*/
        //Debug.Log("入" + new Vector2(X, Y));
        Road.Push(new Vector2(X, Y));

        CurrentLength += Mode;

        if (CurrentLength >= RoadLength)
        {
            // Debug.Log("找到了");
            return true;
        }
        int[] next = SerchAround(X, Y, Mode);

        for (int i = 0; i < next[4]; i++)
        {
            if (i != 0)
                next = SerchAround(X, Y, Mode);
            if (next[4] == 0)
            {
                break;
            }
            int index = UnityEngine.Random.Range(0, next[4]);
            Thread.Sleep(10);
            //Debug.Log("index=" + index);
            // Debug.Log("next[index]=" + next[index]);
            //Debug.LogFormat("({0},{1})", x2 + dir[next[index], 0], y2 + dir[next[index], 1]);
            int nextX = dir[next[index], 0] * Mode;
            int nextY = dir[next[index], 1] * Mode;
            if (Mode == 2)
            {
                Path[X + nextX / 2, Y + nextY / 2] = 1;
            }
            if (DFS_Start(X + nextX, Y + nextY))
            {
                return true;
            }
        }
        //Debug.Log("出" + new Vector2(x2, y2));
        Path[X, Y] = 0;
        Road.Pop();
        CurrentLength -= Mode;
        return false;
    }

    /// <summary>
    /// 创建抽象格子
    /// </summary>
    private void CreateTile()
    {
        for (int i = 0; i < MapWidth; i++)
            for (int j = 0; j < MapHeight; j++)
            {
                Vector3 TilePos = new Vector3(i, 0, j);
                MapTiles.Add(new Tile(TilePos));
            }
    }

    private void InitTile()
    {
        foreach (Tile item in MapTiles)
        {
            item.Data = null;
            item.IsPath = false;
        }
    }


    /*//暂时不用的方法
    Tile GetTileUnderMouse()
    {
        Vector2 wordPos = GetWorldPosition();
        return GetTile(wordPos);
    }

    //获取鼠标所在位置的世界坐标
    Vector3 GetWorldPosition()
    {
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        return worldPos;
    }*/
    #endregion

    #region 帮助方法
    /// <summary>
    /// 生成地面
    /// </summary>
    /// <returns></returns>
    private void SetGround()
    {
        foreach (Tile item in MapTiles)
        {
            item.Data = null;
            if (!item.IsPath)
            {
                /* prefab1.transform.position = item.Position;
                 Instantiate(prefab1);*/


                int index = UnityEngine.Random.Range(0, 100);
                Thread.Sleep(20);
                if (index < Complexity / 2)
                {
                    if (Box.Length > 0)
                    {
                        index %= Box.Length;
                        GameObject w = Game.Instance.ObjectPool.Spawn(Box[index].name);

                        Box box = w.GetComponent<Box>();
                        box.Load(index, item.Position);
                        box.Dead += Box_Dead;
                        item.Data = w;
                    }
                }
            }
        }
    }

    private void Box_Dead(Role obj)
    {
        //
        Debug.Log("清除盒子");
        Tile tile = GetTile(obj.Position);
        tile.Data = null;
    }

    /// <summary>
    /// 协程：生成箭头
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetArrow()
    {
        for (int i = 0; i < MapPath.Count - 1; i++)
        {
            Thread.Sleep(80);
            GameObject arrow = Game.Instance.ObjectPool.Spawn(Arrow.name);
            arrow.transform.position = MapPath[i] + new Vector3(0, 0.07f, 0);

            if (i > 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector3 Dir = new Vector3(MapPath[i].x + dir[j, 0], 0, MapPath[i].z + dir[j, 1]);
                    if (MapPath.Contains(Dir) && Dir != MapPath[i - 1] && Dir != MapPath[i + 1])
                    {
                        GameObject go = Game.Instance.ObjectPool.Spawn(Plane.name);
                        go.transform.position = (MapPath[i] + Dir) / 2;
                        if (MapPath[i].z == Dir.z)
                        {
                            go.transform.localEulerAngles = new Vector3(0, 90, 0);
                        }
                    }
                }
            }
            GameObject p= Game.Instance.ObjectPool.Spawn(path.name);
            p.transform.position= MapPath[i] + new Vector3(0, 0.07f, 0);
            Vector3 vector3 = MapPath[i + 1] - MapPath[i];
            if (vector3.x == 0)
            {
                if (vector3.z == 1)
                {
                    arrow.transform.localEulerAngles = new Vector3(90, -90, 0);
                }
                else
                {
                    arrow.transform.localEulerAngles = new Vector3(90, 90, 0);
                }
            }
            else
            {
                if (vector3.x == -1)
                {
                    arrow.transform.localEulerAngles = new Vector3(90, 180, 0);
                }
                else
                {
                    arrow.transform.localEulerAngles = new Vector3(90, 0, 0);
                }
            }
            yield return null;
        }
        path.transform.position = MapPath[MapPath.Count - 1] + new Vector3(0, 0.07f, 0);
        door = Game.Instance.ObjectPool.Spawn(Door.name);
        door.transform.position = MapPath[0];
        end = Game.Instance.ObjectPool.Spawn(End.name);


        end.transform.position = MapPath[MapPath.Count - 1];
        if (MapPath[1].x == MapPath[0].x)
        {
            door.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        Game.Instance.StartCoroutine(SetDoor());
    }

    /// <summary>
    /// 协程：生成门
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetDoor()
    {
        float t0 = -1.5f;
        float t2 = 0;
        float timer = 0;
        float time = 2f;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            float f = Mathf.Lerp(t0, t2, timer / time);
            door.transform.position = new Vector3(door.transform.position.x, f, door.transform.position.z);
            end.transform.position = new Vector3(end.transform.position.x, f, end.transform.position.z);
            yield return null;
        }
        Debug.Log("完成");
        SendEvent(Consts.E_CompleteInitMap);
    }

    /// <summary>
    /// 协程：生成玩家
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetPlayer()
    {
        //Player.gameObject.SetActive(true);
        yield return 0;
    }

    
    #endregion
}