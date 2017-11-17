using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public PlayerManager[] Players;
    public GameObject[] playerPrefab;

    void Start()
    {
        Human warrior = new Warrior();

        Human mage = new Mage();

        PlayerManager p1 = new PlayerManager();
        p1.Setup();
        

        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].m_Instance =
                Instantiate(playerPrefab[i % playerPrefab.Length], Players[i].m_SpawnPoint.position, Players[i].m_SpawnPoint.rotation) as GameObject;
            Players[i].m_PlayerNumber = i + 1;
            Players[i].Setup();
        }
    }

}
