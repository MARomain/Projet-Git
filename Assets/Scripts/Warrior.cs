using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Human
{
    public GameObject attack;
    bool isAtt = false;

    public override void Attack()
    {
        if (!isAtt)
        {
            isAtt = true;
            attack.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(TimeBetweenAttack());
        }
    } 

    IEnumerator TimeBetweenAttack()
    {
        yield return new WaitForSeconds(1);
        attack.GetComponent<BoxCollider>().enabled = false;
        isAtt = false;
    }

}
