using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Necromancer : Human
{

        public GameObject attack;
        bool isAtt = false;
        public GameObject ShootPos;
        public float bulletSpeed;
        bool SkillUse = false;

    [Command]
    public override void CmdAttack()
        {
            if (!isAtt)
            {
                isAtt = true;
                GameObject go = Instantiate<GameObject>(attack, ShootPos.transform.position, Quaternion.identity);
                NetworkServer.Spawn(go);
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

                StartCoroutine(Cooldown());
            }
        }

        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(5);
            SkillUse = false;
        }
    }