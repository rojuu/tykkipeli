using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameInfoUpdater : MonoBehaviour
{
    float wind;
    bool player1Turn;
    Text infoText;

    void Start()
    {
        wind = GameManager.wind;
        player1Turn = GameManager.Instance.Player1Turn;

        infoText = GetComponent<Text>();
        if (infoText == null) Destroy(gameObject);

        UpdateUIText();
    }

    void Update()
    {
        if(GameManager.Instance.GameEnded)
        {
            infoText.text = "GAME ENDED";
            return;
        }

        if(wind != GameManager.wind
        || player1Turn != GameManager.Instance.Player1Turn)
            UpdateUIText();
    }

    void UpdateUIText()
    {
        wind = GameManager.wind;
        player1Turn = GameManager.Instance.Player1Turn;
        string turnText = player1Turn ? "Player1 Turn" : "Player2 Turn";
        infoText.text = turnText + "\nWind: " + wind;
    }
}
