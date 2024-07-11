using System;
using System.IO;
using UnityEngine;

public static class SaveLoadUtility
{
    public static readonly string inventoryFilePath = Application.persistentDataPath + "/inventory.json";
    public static readonly string currencyFilePath = Application.persistentDataPath + "/currency.json";

    public static void SaveData<T>(T data, string fileName)
    {
        try
        {
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), jsonData);
            Debugger.PrintLog($"{fileName} saved successfully.");
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
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                T data = JsonUtility.FromJson<T>(jsonData);
                Debugger.PrintLog($"{fileName} loaded successfully.");
                return data;
            }
            else
            {
                T data = new T();
                SaveData(data, filePath);
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
