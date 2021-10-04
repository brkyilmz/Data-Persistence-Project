using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public string chosenName;
    public InputField nameInput;
    public Text highScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        if (DataSaver.Instance != null)
        {
            highScoreText.text = DataSaver.Instance.bScoreText.text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void NameSetter()
    {
        chosenName = nameInput.text;
        Debug.Log(chosenName);
        NewNameCreated(chosenName);
    }

    public void NewNameCreated(string newName)
    {
        DataSaver.Instance.playerName = newName;
    }
}
