using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
public static class SaveLoadUtility
{
    public static readonly string inventoryFilePath = "inventory.json";

    private static string GetFullPath(string fileName)
    {
#if UNITY_EDITOR
        return Path.Combine(Application.dataPath, fileName);
#elif UNITY_ANDROID
        return Path.Combine(Application.persistentDataPath, fileName);
#else
        return Path.Combine(Application.persistentDataPath, fileName);
#endif
    }

    public static void SaveData<T>(T data, string fileName)
    {
        try
        {
            string fullPath = GetFullPath(fileName);
            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(fullPath, jsonData);
            Debugger.PrintLog($"{fullPath} saved successfully. data : {jsonData}");
        }
        catch (Exception ex)
        {
            Debugger.PrintLog($"Error saving {fileName}: " + ex.Message , LogType.Error);
        }
    }

    public static T LoadData<T>(string fileName) where T : new()
    {
        try
        {
            string fullPath = GetFullPath(fileName);
            if (File.Exists(fullPath))
            {
                string jsonData = File.ReadAllText(fullPath);
                T data = JsonConvert.DeserializeObject<T>(jsonData);
                Debugger.PrintLog($"{fullPath} loaded successfully.");
                return data;
            }
            else
            {
                T data = new T();
                SaveData(data, fullPath);
                return data;
            }
        }
        catch (Exception ex)
        {
            Debugger.PrintLog($"Error loading {fileName}: {ex.Message}", LogType.Error);
            return new T();
        }
    }
    public static void ResetData<T>(string filePath) where T : new()
    {
        try
        {
            T data = new T();
            SaveData(data, filePath);
            Debugger.PrintLog($"{filePath} reset successfully.");
        }
        catch (Exception ex)
        {
            Debugger.PrintLog($"Error resetting {filePath}: {ex.Message}", LogType.Error);
        }
    }
}
