using UnityEngine;
using UnityEditor;

public class MonsterGroup
{
    public int monsterID;
    public int count;
    /// <summary>
    /// 添加一群怪到某个回合中
    /// </summary>
    /// <param name="MonsterID">怪的类型</param>
    /// <param name="Count">怪的数目</param>
    public MonsterGroup(int MonsterID,int Count)
    {
        monsterID = MonsterID;
        count = Count;
    }
}