using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossMonster : Monster
{
    [SerializeField] private GameObject bossHpbar;
    [SerializeField] private Image hpbar;
    [SerializeField] private Image damageBar;
    [SerializeField] private AudioController audioController;

    //event UnityAction onPlayerTrace;
    
    public override float Hp
    {
        get => base.Hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                ChangeState(State.Die);
            }
            else if (Hp < MaxHp)
            {
                ChangeState(State.Hit);
                if (Hp <= MaxHp * 0.5f)
                {
                    moveSpeed = 10f;
                }
            }
        }
    }
    private new void Awake()
    {
        base.Awake();
        audioController = GameObject.Find("BGM").GetComponent<AudioController>();
        //onPlayerTrace += ObjectPoolManager.Instance.traceAll; �־ȵ���
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
        if (produc) { produc = false; ShowHpBar(); ChangeBGM(); PlayAggravationSound(); ObjectPoolManager.Instance.traceAll?.Invoke(); }
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
        if ((!animator.GetBool("Attack") && !animator.GetBool("Walk") && !animator.GetBool("Hit")))
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
        PlayDieSound();
        PlayerManager.Instance.playerUI.GameClear();
        animator.SetBool("Hit", false);
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(3f);
        GameManager.Instance.ReturnMain();
    }
    #endregion State
    #region Func
    public override void TakeDamage(float damage)
    {
        if (Hp > 0)
        {
            if (Hp < damage)
            {
                Hp = 0;
            }
            else
            {
                Hp -= damage;
            }
            UpdateHp(Hp);
        }
    }
    private void ShowHpBar()
    {
        bossHpbar.SetActive(true);
        UpdateHp(Hp);
    }
    private void UpdateHp(float curHp)
    {
        float cent = curHp / MaxHp;
        if (hpbar.fillAmount == cent)
        { // ����
            StartCoroutine(UpdateGreenHp(cent));
        }
        else
        { // �¾�����
            hpbar.fillAmount = cent;
            StartCoroutine(UpdateRedHp(cent));
        }
    }
    private void ChangeBGM()
    {
        audioController.BossTrace();
    }
    #endregion Func

    #region UI IEnumerator
    private IEnumerator UpdateRedHp(float cent)
    {
        for (float i = damageBar.fillAmount; i > cent; i -= 0.002f)
        {
            damageBar.fillAmount = i;
            yield return null;
        }
        damageBar.fillAmount = hpbar.fillAmount - 0.005f;
    }
    private IEnumerator UpdateGreenHp(float cent)
    {
        damageBar.fillAmount = 0;
        for (float i = 0; i < cent; i += 0.007f)
        {
            hpbar.fillAmount = i;
            yield return null;
        }
        hpbar.fillAmount = 1f;
        damageBar.fillAmount = hpbar.fillAmount - 0.005f;
    }
    #endregion UI IEnumerator
}
