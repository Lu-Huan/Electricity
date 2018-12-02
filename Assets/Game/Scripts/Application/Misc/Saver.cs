using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using LitJson;
using System.IO;


public static class Saver
{
    /// <summary>
    /// 写入Json数据
    /// </summary>
    /// <param name="data">Json字符串</param>
    public static void WriteJsonString(string data)
    {
        FileInfo file = new FileInfo(Application.persistentDataPath + "Data.json");
        StreamWriter writer =file.CreateText();
        writer.Write(data);
        writer.Close();
        writer.Dispose();
    }


    /// <summary>
    /// 读取Json数据
    /// </summary>
    /// <returns>Json字符串</returns>
    public static string ReadJsonString()
    {
        StreamReader reader = new StreamReader(Application.persistentDataPath + "Data.Json");
        string JsonData = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();
        return JsonData;
    }

}