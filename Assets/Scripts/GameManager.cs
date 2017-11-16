using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Transform m_SpawnPointPlayer1;                          // The position and direction the tank will have when it spawns.
    public Transform m_SpawnPointPlayer2;                          // The position and direction the tank will have when it spawns.
    private Human m_Human;


    void Start()
    {
        Human warrior = new Warrior();

        Human mage = new Mage();


        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        Instantiate(player1Prefab,m_SpawnPointPlayer1);
        Instantiate(player2Prefab,m_SpawnPointPlayer2);
    }



    public void DisableControl()
    {
        m_Human.enabled = false;
    }


    // Used during the phases of the game where the player should be able to control their tank.
    public void EnableControl()
    {
        m_Human.enabled = true;
    }


    // Used at the start of each round to put the tank into it's default state.
    public void Reset()
    {
        player1Prefab.transform.position = m_SpawnPointPlayer1.position;
        player1Prefab.transform.rotation = m_SpawnPointPlayer1.rotation;

        player2Prefab.transform.position = m_SpawnPointPlayer2.position;
        player2Prefab.transform.rotation = m_SpawnPointPlayer2.rotation;

        player1Prefab.SetActive(false);
        player1Prefab.SetActive(true);

        player2Prefab.SetActive(false);
        player2Prefab.SetActive(true);
    }


}
