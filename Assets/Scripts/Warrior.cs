using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Human
{
    public GameObject attack;
    public GameObject skill;
    bool isAtt = false;
    bool SkillUse = false;

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
    IEnumerator SkillTimeUse()
    {
        yield return new WaitForSeconds(2);
        skill.gameObject.SetActive(false);
        //skill.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5);
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
