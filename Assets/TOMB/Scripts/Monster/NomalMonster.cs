using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalMonster : Monster
{
    private new void Awake()
    {
        base.Awake();
        ObjectPoolManager.Instance.returnObjectAll += objectReturn;
        ObjectPoolManager.Instance.traceAll += AllTrace;
    }
    #region State
    private IEnumerator Idle()
    {
        while (true)
        {
            if (!animator.GetBool("Attack"))
            {
                if (FindTarget())
                {
                    ChangeState(State.Trace);
                }
                else if (FindAttackTarget())
                {
                    ChangeState(State.Attack);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator Trace()
    {
        if (produc) { produc = false; PlayAggravationSound(); }
        while (true)
        {
            animator.SetBool("Walk", (traceTarget != null));
            if (traceTarget != null) 
            {
                agent.speed = MoveSpeed;
                agent.destination = traceTarget.transform.position; 
            }
            if (!FindTarget())
            {
                traceTarget = null;
                animator.SetBool("Walk", (traceTarget != null));
                agent.ResetPath();
                ChangeState(State.Idle);
            }
            if (FindAttackTarget() && !animator.GetBool("Attack"))
            {
                agent.ResetPath();
                animator.SetBool("Walk", false);
                ChangeState(State.Attack);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator Attack()
    {
        animator.SetBool("Attack", FindAttackTarget());
        animator.SetInteger("RanAttack", Random.Range(0, attackPattern));
        yield return new WaitForSeconds(1f);
        ChangeState(State.Idle);
    }
    private IEnumerator Hit()
    {
        if ((!animator.GetBool("Attack") && !animator.GetBool("Walk")&& !animator.GetBool("Hit"))) 
        {
            animator.Play("Take Damage");
            // animator.SetBool("Hit", true);
            yield return null;
        }
        ChangeState(State.Idle);
    }
    private IEnumerator Die()
    {
        agent.ResetPath();
        animator.SetBool("Hit", false);
        PlayDieSound();
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1.5f);
        DropItem();
        ObjectPoolManager.Instance.ReturnObject(this.gameObject, keyType);
    }
    #endregion State
}
