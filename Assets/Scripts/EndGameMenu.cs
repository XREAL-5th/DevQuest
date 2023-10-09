using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI endGameText;
    [SerializeField]
    private Button RestartButton;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.main.clear)
            endGameText.text = "Game Clear";
        scoreText.text = "Score: " + GameManager.main.score;
        GameManager.main.score = 0;
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Assigment");
    }
 
}
