using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Human
{
    public int warriorLife;
    public GameObject attack;


    public override void Attack()
    {
        attack.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(TimeBetweenAttack());

    } 

    IEnumerator TimeBetweenAttack()
    {
        yield return new WaitForSeconds(1);
        attack.GetComponent<BoxCollider>().enabled = false;
    }
}
