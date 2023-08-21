using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;

    [SerializeField]
    private TextMeshProUGUI player1Score;

    [SerializeField]
    private TextMeshProUGUI player2Score;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject playMenu;

    [SerializeField]
    private TextMeshProUGUI gameOverMessage;

    private void Start()
    {
        manager.onScoreChange += OnScoreChange;
        manager.onMainGameEnd += OnGameOver;
        manager.onMainGameStart += OnGameStart;

        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        gameOverMessage.gameObject.SetActive(false);
    }

    private void OnGameStart()
    {
        ToggleMenu(false);
        gameOverMessage.gameObject.SetActive(false);
        player1Score.text = "0";
        player2Score.text = "0";
    }

    private void OnGameOver(Player player)
    {
        ToggleMenu(true);
        gameOverMessage.gameObject.SetActive(true);
        switch (player)
        {
            case Player.Player1:
                gameOverMessage.text = "YOU WON!";
                break;
            case Player.Player2:
                gameOverMessage.text = "THE COMPUTER WON!";
                break;
        }
    }

    private void ToggleMenu(bool show)
    {
        mainMenu.SetActive(show);
        playMenu.SetActive(!show);
    }

    private void OnScoreChange(Player player, int score)
    {
        switch (player)
        {
            case Player.Player1:
                player1Score.text = score.ToString();
                break;
            case Player.Player2:
                player2Score.text = score.ToString();
                break;
        }
    }
}
