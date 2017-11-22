using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackN : Attack {

    public Human human;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LifeSteal(other.gameObject));
    }

    IEnumerator LifeSteal(GameObject oter)
    {
        while (true)
        {
            human._life += Damage;
            oter.gameObject.GetComponent<Human>().takeDamage(Damage);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(LifeSteal(other.gameObject));
    }

}
