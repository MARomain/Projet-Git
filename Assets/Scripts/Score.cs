using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : Singleton<Score>
{
    [Header("Score")]
    public int player1Score;
    public int player2Score;
    public int scorePerHit = 10;
    public Text player1;
    public Text player2;

    public void AddScore(int value, int playerNumber)
    {
        if (playerNumber == 1)
        player2Score += scorePerHit;

        if (playerNumber == 2)
        player1Score += scorePerHit;
    }

    private void FixedUpdate()
    {
        player1.text = "Player1 : " + player1Score.ToString();
        player2.text = "Player2 : " + player2Score.ToString();
    }

}
