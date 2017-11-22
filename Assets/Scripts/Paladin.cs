using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Paladin : Human {

    public GameObject attack;
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
