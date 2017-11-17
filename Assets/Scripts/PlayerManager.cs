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


    private Human m_Human;


    public void Setup()
    {
        // Get reference to the components


        //MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
        //// Go through all the renderers...
        //for (int i = 0; i < renderers.Length; i++)
        //{
        //    // ... set their material color to the color specific to this tank.
        //        renderers[i].material.color = m_PlayerColor;
        //}
    }





    public void DisableControl()
    {

    }

    public void EnableControl()
    {

    }

    public void Reset()
    {

    }
}
