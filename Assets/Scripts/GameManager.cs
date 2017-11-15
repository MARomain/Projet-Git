using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    void Start()
    {
        Human warrior = new Warrior();

        Human mage = new Mage();
    }
}
