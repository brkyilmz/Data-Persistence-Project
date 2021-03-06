using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        if (DataSaver.Instance != null && DataSaver.Instance.bScoreNum != 0)
        {
            DataSaver.Instance.LoadBestPlayer();
            BestScoreText.text = DataSaver.Instance.bScoreText;
        }
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            //When the game starts, update the score section with the name user selected in menu, and their current score, which is 0
            ScoreText.text = $"{DataSaver.Instance.playerName} Score : {m_Points}";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            // The best score is loaded when this script starts
            // When the game ends, if the score of the current player exceeds the loaded best
            // Update best score variable with current best score
            // Update best score section with current playername and current points
            // Then, save this player as the new best one
            if (m_Points > DataSaver.Instance.bScoreNum)
            {
                DataSaver.Instance.bScoreNum = m_Points;
                BestScoreText.text = "Best Score: " + DataSaver.Instance.playerName + ": " + m_Points;
                Debug.Log("No errs this far.");
                DataSaver.Instance.bScoreText = BestScoreText.text;
                DataSaver.Instance.SaveBestPlayer(m_Points);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{DataSaver.Instance.playerName} Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

   
}
