using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public int _life;
    public int _speed;
    public int _damage;
    public int _fireRate;

    public Human(int life, int speed, int damage, int fireRate)
    {
        _life = life;
        _speed = speed;
        _damage = damage;
        _fireRate = fireRate;
    }


}
