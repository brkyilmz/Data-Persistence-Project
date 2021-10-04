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

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
