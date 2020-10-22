using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int playerScore;
    public int playerHealth;
    
    public TextMeshProUGUI tmpUGUI_PlayerScore;

    void Awake()
    {
        playerScore = 0;
        playerHealth = 100;

        tmpUGUI_PlayerScore.text = playerScore.ToString();
    }

    void Update()
    {
        tmpUGUI_PlayerScore.text = playerScore.ToString();
    }
}
