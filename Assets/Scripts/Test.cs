using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private static Dictionary<string, string> saveData = new Dictionary<string, string>();
    // 将变量名和值添加到存档数据中
    public static void SetSaveData(string varName, string value)
    {
        saveData[varName] = value;
    }
    // 从存档数据中获取变量值，如果不存在则返回默认值
    public static string GetSaveData(string varName, string defaultValue)
    {
        if (saveData.ContainsKey(varName))
        {
            return saveData[varName];
        }
        else
        {
            return defaultValue;
        }
    }
    // 将存档数据保存到PlayerPrefs中
    public static void SaveGame()
    {
        foreach (var kvp in saveData)
        {
            PlayerPrefs.SetString(kvp.Key, kvp.Value);
        }
    }
    // 从PlayerPrefs中加载存档数据
    public static void LoadGame()
    {
        saveData.Clear();

        foreach (string key in PlayerPrefs.GetString("saveDataKeys").Split(','))
        {
            if (key != "")
            {
                string value = PlayerPrefs.GetString(key);
                saveData[key] = value;
            }
        }
    }
    // 清除存档数据
    public static void ClearSaveData()
    {
        saveData.Clear();
    }
    // 将存储在PlayerPrefs中的所有数据清除
    public static void DeleteAllSavedData()
    {
        PlayerPrefs.DeleteAll();
        saveData.Clear();
    }
    // 保存存档数据的信息
    private static void SaveSaveDataKeys()
    {
        string keys = "";
        foreach (var kvp in saveData)
        {
            keys += kvp.Key + ",";
        }
        PlayerPrefs.SetString("saveDataKeys", keys);
    }
}
