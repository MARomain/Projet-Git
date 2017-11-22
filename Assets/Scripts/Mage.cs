using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mage : Human
{


    public GameObject attack;
    bool isAtt = false;
    public GameObject ShootPos;
    public float bulletSpeed;
    bool SkillUse = false;
    public int TpRange;

    public override void CmdAttack()
    {
        if (!isAtt)
        {
            isAtt = true;
            Debug.Log("Atk");
            InstantiateAttack();
            StartCoroutine(TimeBetweenAttack());
        }        
    }

    void InstantiateAttack() {
        GameObject go = Instantiate<GameObject>(attack, ShootPos.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        //NetworkServer.Spawn(go);
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
            transform.Translate(Vector3.forward.normalized * TpRange);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5);
        SkillUse = false;
    }
}
