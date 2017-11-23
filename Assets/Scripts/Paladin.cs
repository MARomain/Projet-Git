using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Paladin : Human {

    public GameObject attack;
    public Animator anim;
    bool isAtt = false;
    bool SkillUse = false;
    public int heal;
    public float cooldown;
    public int HpMax;

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
        anim.SetBool("IsAtt", false);
        attack.GetComponent<Collider>().enabled = false;
        isAtt = false;
    }


    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        SkillUse = false;
    }

    IEnumerator resetAnim()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("IsHeal", false);
    }

    public override void Skill()
    {
        if (!SkillUse)
        {
            SkillUse = true;
            anim.SetBool("IsHeal", true);
            StartCoroutine(resetAnim());
            _life += heal;
            StartCoroutine(Cooldown());
        }
    }

    protected override void Update()
    {
        base.Update();
        if (_life > HpMax)
        {
            _life = HpMax;
        }
    }

}
