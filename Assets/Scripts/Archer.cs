using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Archer : Human
{


    public GameObject attack;
    bool isAtt = false;
    public GameObject ShootPos;
    public GameObject SkillPos01;
    public GameObject SkillPos02;
    public float bulletSpeed;
    bool SkillUse = false;
    public int TpRange;
    public float cooldown;

    public override void CmdAttack()
    {
        if (!isAtt)
        {
            isAtt = true;
            Debug.Log("Atk");
            CmdInstantiateAttack();
            StartCoroutine(TimeBetweenAttack());
        }
    }
    //
    void CmdInstantiateAttack()
    {
        GameObject go = Instantiate<GameObject>(attack, ShootPos.transform.position, ShootPos.transform.rotation);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
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
            GameObject go = Instantiate<GameObject>(attack, ShootPos.transform.position, ShootPos.transform.rotation);
            go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            GameObject to = Instantiate<GameObject>(attack, SkillPos01.transform.position, SkillPos01.transform.rotation);
            to.GetComponent<Rigidbody>().AddForce(to.transform.forward * bulletSpeed);
            GameObject bo = Instantiate<GameObject>(attack, SkillPos02.transform.position, SkillPos02.transform.rotation);
            bo.GetComponent<Rigidbody>().AddForce(bo.transform.forward * bulletSpeed);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        SkillUse = false;
    }
}

