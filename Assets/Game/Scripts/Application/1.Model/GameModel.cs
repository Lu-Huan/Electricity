﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class GameModel : Model
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public GameObject MainCharater;
    //所有的关卡
    List<Level> m_Levels = new List<Level>();
    //关卡

    //最大通关关卡索引
    int m_GameProgress = 3;

    //当前游戏的关卡索引
    int m_PlayLevelIndex = 0;

    //游戏金币
    int m_gold = 0;
    //控制的角色索引
    int m_role = 0;

    //是否游戏中
    bool m_IsPlaying = false;
    #endregion

    #region 属性//全为返回值

    public override string Name
    {
        get { return Consts.M_GameModel; }
    }

    public int Current_role
    {
        set
        {
            if (value == -1)
            {
                Debug.Log("异常");
            }
            else
            {
                m_role = value;
                PlayerPrefs.SetInt("CurrentChar", value);
                SendEvent(Consts.E_ChangeMainCharater);
            }
        }
        get
        {
            return m_role;
        }
    }
    public int Gold
    {
        get { return m_gold; }
        set { m_gold = value; }
    }

    public int LevelCount
    {
        get { return m_Levels.Count; }
    }

    public int GameProgress
    {
        get { return m_GameProgress; }
    }

    public int PlayLevelIndex
    {
        get { return m_PlayLevelIndex; }
    }

    public bool IsPlaying
    {
        get { return m_IsPlaying; }
        private set { m_IsPlaying = value; }
    }

    public bool IsGamePassed
    {
        get { return m_GameProgress >= LevelCount - 1; }
    }

    //返回所有关卡
    public List<Level> AllLevels
    {
        get { return m_Levels; }
    }

    public Level PlayLevel
    {
        get
        {
            if (m_PlayLevelIndex < 0 || m_PlayLevelIndex > m_Levels.Count - 1)
                throw new IndexOutOfRangeException("关卡不存在");

            return m_Levels[m_PlayLevelIndex];
        }
    }
    #endregion

    #region 方法

    //初始化
    public void Initialize()
    {
        m_GameProgress = 0;
        //构建Level集合
        //List<FileInfo> files = Tools.GetLevelFiles();
        List<Level> levels = new List<Level>();
        for (int i = 0; i < m_GameProgress; i++)
        {
            Level level = new Level();
            //Tools.FillLevel(files[i].FullName, ref level);
            levels.Add(level);
        }
        m_Levels = levels;

        //读取游戏进度
        //m_GameProgress = Saver.GetProgress();
    }

    //游戏开始
    public void StartLevel(int levelIndex)
    {
        m_PlayLevelIndex = levelIndex;
        m_IsPlaying = true;
    }

    //游戏结束,是否成功
    public void PushProgress()
    {
        m_GameProgress++;
    }

    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}
