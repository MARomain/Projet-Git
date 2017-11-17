using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScore : Score {
    public int scoreValue = 10;


    private void OnTriggerEnter(Collider other)
    {
        Score.Instance.AddScore(scoreValue);
    }




}
