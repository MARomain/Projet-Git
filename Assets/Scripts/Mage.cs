using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mage : Human
{


    public GameObject attack;
    public Animator anim;
    bool isAtt = false;
    public GameObject ShootPos;
    public float bulletSpeed;
    bool SkillUse = false;
    public int TpRange;
    public float cooldown;

    public override void CmdAttack()
    {
        if (!isAtt)
        {
            isAtt = true;
            anim.SetBool("IsAtt", true);
            InstantiateAttack();
            StartCoroutine(TimeBetweenAttack());
        }        
    }

    void InstantiateAttack() {
        GameObject go = Instantiate<GameObject>(attack, ShootPos.transform.position, ShootPos.transform.rotation);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        
    }

    IEnumerator TimeBetweenAttack()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("IsAtt", false);
        isAtt = false;
    }

    IEnumerator resetAnim()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("Tp", false);
    }

    public override void Skill()
    {
        if (!SkillUse)
        {
            SkillUse = true;
            anim.SetBool("Tp", false);
            StartCoroutine(resetAnim());
            transform.Translate(Vector3.forward.normalized * TpRange);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        SkillUse = false;
    }
}
