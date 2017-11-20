using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScore : MonoBehaviour {

    [Header("PickupScore")]
    public int scoreValue = 10;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Score.Instance.AddScore(scoreValue, other.GetComponent<Human>().playerNumber);
            Destroy(gameObject);
        }
    }




}
