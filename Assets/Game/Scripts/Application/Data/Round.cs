using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Round
{
    public List<MonsterGroup> TheRound = new List<MonsterGroup>();
    public int TotalMonster=0;
    /// <summary>
    /// 建立一个关卡
    /// </summary>
    /// <param name="group">一个怪物群的集合</param>
    public Round(List< MonsterGroup> groups)
    {
        TheRound = groups;
        foreach (MonsterGroup item in groups)
        {
            TotalMonster += item.count;
        }
    }
}