using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Necromancer : Human
{

        public GameObject attack;
    public GameObject skill;
    public Animator anim;
    bool isAtt = false;
        public GameObject ShootPos;
        public float bulletSpeed;
        bool SkillUse = false;
    public float cooldown;
    public int HpMax;

    public override void CmdAttack()
        {
            if (!isAtt)
            {
                isAtt = true;
            anim.SetBool("IsAtt", true);
            GameObject go = Instantiate<GameObject>(attack, ShootPos.transform.position, ShootPos.transform.rotation);
            go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
                StartCoroutine(TimeBetweenAttack());
            }
        }

        IEnumerator TimeBetweenAttack()
        {
            yield return new WaitForSeconds(1);
        anim.SetBool("IsAtt", false);
        isAtt = false;
        }

        public override void Skill()
        {
            if (!SkillUse)
            {
                SkillUse = true;
            anim.SetBool("IsLS", true);
            skill.gameObject.SetActive(true);
            //skill.GetComponent<Collider>().enabled = true;
            StartCoroutine(SkillTimeUse());
            }
        }
    IEnumerator SkillTimeUse()
    {
        yield return new WaitForSeconds(2.15f);
        anim.SetBool("IsLS", false);
        skill.gameObject.SetActive(false);
        //skill.GetComponent<Collider>().enabled = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(cooldown);
            SkillUse = false;
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