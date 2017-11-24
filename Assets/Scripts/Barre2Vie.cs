using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barre2Vie : MonoBehaviour
{
    public Slider player1Barre2Vie;
    public Slider player2Barre2Vie;



    public void UpdateBarre2Vie(int playerNumber, int life)
    {
        if (playerNumber == 1)
            player1Barre2Vie.value = life;

        if (playerNumber == 2)
            player2Barre2Vie.value = life;
    }

}

