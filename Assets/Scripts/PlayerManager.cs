using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class PlayerManager
{

    public Color m_PlayerColor;                             // This is the color this tank will be tinted.
    public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
    [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
    [HideInInspector] public int m_PlayerNumber;            // This specifies which player this the manager for.
    [HideInInspector] public string m_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
    [HideInInspector] public int m_Wins;                    // The number of wins this player has so far.


    private Human m_Human;
    private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.

    public void Setup()
    {
        // Get reference to the components
        m_Human = m_Instance.GetComponent<Human>();
        m_CanvasGameObject = GameObject.Find("Canvas/GameManager");

        //MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
        //// Go through all the renderers...
        //for (int i = 0; i < renderers.Length; i++)
        //{
        //    // ... set their material color to the color specific to this tank.
        //        renderers[i].material.color = m_PlayerColor;
        //}


        // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
    }




    public void DisableControl()
    {
        //script à disable
        m_Human.enabled = false;
        m_CanvasGameObject.SetActive(false);
    }

    public void EnableControl()
    {
        //script à enable
        m_Human.enabled = true;
        m_CanvasGameObject.SetActive(true);
    }

    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }


}
