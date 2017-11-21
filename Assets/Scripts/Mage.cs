using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Human
{


    public GameObject attack;
    bool isAtt = false;
    public GameObject ShootPos;
    public float bulletSpeed;
    bool SkillUse = false;
    public int TpRange;

    public override void Attack()
    {
        if (!isAtt)
        {
            isAtt = true;
            GameObject go = Instantiate<GameObject>(attack, ShootPos.transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            StartCoroutine(TimeBetweenAttack());
        }        
    }

    IEnumerator TimeBetweenAttack()
    {
        yield return new WaitForSeconds(1);
        isAtt = false;
    }

    public override void Skill()
    {
        if (!SkillUse)
        {
            SkillUse = true;
            transform.Translate(Vector3.forward * 8);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5);
        SkillUse = false;
    }
}
