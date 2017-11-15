using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Human m_Human;
    public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
    [HideInInspector] public int m_PlayerNumber;            // This specifies which player this the manager for.
    [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the tank when it is created.



    public void Setup()
    {
        m_Human._playerNumber = m_PlayerNumber;
        

        m_Human = m_Instance.GetComponent<Human>();
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
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

}
