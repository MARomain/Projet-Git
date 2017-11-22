using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Warrior : Human
{
    public GameObject attack;
    public GameObject skill;
    public Animator anim;
    bool isAtt = false;
    bool SkillUse = false;
    public float cooldown;

    public override void CmdAttack()
    {
        if (!isAtt)
        {
            isAtt = true;
            anim.SetBool("IsAtt", true);
            attack.GetComponent<Collider>().enabled = true;
            StartCoroutine(TimeBetweenAttack());
        }
    } 

    IEnumerator TimeBetweenAttack()
    {
        yield return new WaitForSeconds(1);
        attack.GetComponent<Collider>().enabled = false;
        isAtt = false;
        anim.SetBool("IsAtt", false);
    }
    IEnumerator SkillTimeUse()
    {
        yield return new WaitForSeconds(2);
        //skill.gameObject.SetActive(false);
        ////skill.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        SkillUse = false;
    }

    public override void Skill()
    {
        if (!SkillUse)
        {
            SkillUse = true;
            skill.gameObject.SetActive(true);
            //skill.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(SkillTimeUse());
        }
    }

}
