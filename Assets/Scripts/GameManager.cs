using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Paused,
    Playing
}

public enum Player
{
    Player1,
    Player2
}

public class GameManager : MonoBehaviour
{
    public delegate void OnMainGameStart();
    public delegate void OnMainGameEnd(Player player);
    public delegate void OnGameStart();
    public delegate void OnResetFunction();
    public delegate void OnScoreChange(Player player, int score);

    public GameState state = GameState.Menu;
    public int maxScore = 10;

    private int player1Score = 0;
    private int player2Score = 0;

    public OnResetFunction onReset;
    public OnGameStart onStart;
    public OnScoreChange onScoreChange;
    public OnMainGameStart onMainGameStart;
    public OnMainGameEnd onMainGameEnd;

    public void OnLost(Player player)
    {
        state = GameState.Paused;
        onReset?.Invoke();

        switch (player)
        {
            case Player.Player1:
                player2Score += 1;
                onScoreChange?.Invoke(Player.Player2, player2Score);
                break;
            case Player.Player2:
                player1Score += 1;
                onScoreChange?.Invoke(Player.Player1, player1Score);
                break;
        }

        if (player1Score >= maxScore || player2Score >= maxScore)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        state = GameState.Menu;
        onMainGameEnd?.Invoke(player1Score > player2Score ? Player.Player1 : Player.Player2);
    }

    public void MainGameStart()
    {
        state = GameState.Paused;
        onMainGameStart?.Invoke();
        player1Score = 0;
        player2Score = 0;
    }

    private void GameStart()
    {
        state = GameState.Playing;
        onStart?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state == GameState.Paused)
        {
            GameStart();
        }
    }
}
