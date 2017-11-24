using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Warrior : Human
{
    public GameObject attack;
    public GameObject skill;
    public GameObject player;
    public Animator anim;
    bool isAtt = false;
    bool SkillUse = false;
    public float cooldown;

    protected override void OnEnable()
    {
        base.OnEnable();
        anim.SetBool("IsAtt", false);
        anim.SetBool("IsBlk", false);
    }

    protected void OnDisable()
    {
        anim.SetBool("IsAtt", false);
        anim.SetBool("IsBlk", false);
    }

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
        skill.GetComponent<Collider>().enabled = false;
        player.GetComponent<Collider>().enabled = true;
        anim.SetBool("IsBlk", false);
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
            //skill.gameObject.SetActive(true);
            skill.GetComponent<Collider>().enabled = true;
            player.GetComponent<Collider>().enabled = false;
            anim.SetBool("IsBlk", true);
            StartCoroutine(SkillTimeUse());
        }
    }

}
