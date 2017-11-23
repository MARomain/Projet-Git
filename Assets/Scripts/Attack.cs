using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public int Damage;
    private bool CanHit = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !CanHit)
        {
            CanHit = true;
            other.GetComponent<Human>().takeDamage(Damage);
            StartCoroutine(DoOnce(other.gameObject));
        }
    }

    IEnumerator DoOnce(GameObject other)
    {        
        yield return new WaitForSeconds(1);
        CanHit = false;
    }
}
