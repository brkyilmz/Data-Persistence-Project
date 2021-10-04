using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSaver : MonoBehaviour
{
    public static DataSaver Instance;
    public string playerName;
    public string bScoreText;
    public int bScoreNum;
    public string bPlayerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestPlayer();
    }

    public void SaveBestPlayer(int points)
    {
        Debug.Log("Inside Save");
        SaveData data = new SaveData();
        data.bPlayerName = DataSaver.Instance.playerName;
        data.bPlayerScore = points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/save.json";

        if (File.Exists(path))
        {
            Debug.Log("Path exists.");
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            DataSaver.Instance.bScoreNum = data.bPlayerScore;
            DataSaver.Instance.bPlayerName = data.bPlayerName;
            DataSaver.Instance.bScoreText = "Best Score: " + DataSaver.Instance.bPlayerName + ": " + DataSaver.Instance.bScoreNum;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string bPlayerName;
        public int bPlayerScore;
    }
}
