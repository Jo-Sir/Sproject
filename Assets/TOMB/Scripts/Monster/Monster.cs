using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour, IDamagable
{
    public enum State { Idle, Trace, Attack, Hit, Die }
    protected State curState;
    [Header("Key")]
    [SerializeField] protected KeyType keyType;
    [Header("Stats")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float hp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected int attackPattern;
    [Header("target & range")]
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField, Range(0f, 100f)] protected float targetInRage;
    [SerializeField, Range(0f, 50f)] protected float attackRange;
    [Header("Audio")]
    [SerializeField] protected AudioClip[] audioClips;

    protected AudioSource audioSource;
    protected bool produc = true;
    protected GameObject traceTarget = null;
    protected GameObject attackTarget = null;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Collider _collider;
    protected UnityAction onAttack;
    public LayerMask TargetLayerMask { get { return targetLayerMask; } }
    public float AttackRange { get { return attackRange; } }
    public float AttackDamage { get { return attackDamage; } }
    public float TargetInRage { get { return targetInRage; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public Animator Animator { get { return animator; } }
    public float MaxHp { get { return maxHp; } }
    public UnityAction OnAttack { get { return onAttack; } }

    #region Unity
    public virtual float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                _collider.enabled = false;
                ChangeState(State.Die);
            }
            else if (hp < MaxHp)
            {
                if (!animator.GetBool("Hit")) { ChangeState(State.Hit); } 
                if (traceTarget == null)
                {
                    traceTarget = PlayerManager.Instance.player.gameObject;
                }
            }
        }
    }
    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _collider = GetComponentInChildren<Collider>();
        audioSource = GetComponent<AudioSource>();
        Hp = MaxHp;
        curState = State.Idle;
        ObjectPoolManager.Instance.returnObjectAll += objectReturn;
    }
    private void OnEnable()
    {
        _collider.enabled = true;
        ChangeState(curState);
        traceTarget = null;
    }
    #endregion Unity

    #region Func
    protected void ChangeState(State nextState)
    {
        StopCoroutine(curState.ToString());
        curState = nextState;
        StartCoroutine(curState.ToString());
    }
    protected bool FindTarget()
    {
        Vector3 onwertransform = transform.position;
        onwertransform.y = transform.position.y + 1f;

        if (traceTarget == null)
        {
            Collider[] target = Physics.OverlapSphere(onwertransform, TargetInRage, TargetLayerMask);
            if (target.Length > 0)
            {
                traceTarget = target[0].gameObject;
            }
            else
            {
                traceTarget = null;
            }
        }
        return traceTarget != null;
    }
    protected bool FindAttackTarget()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, AttackRange, TargetLayerMask);
        if (target.Length > 0)
        {
            attackTarget = target[0].gameObject;
            onAttack = () => target[0].GetComponent<IDamagable>().TakeDamage(attackDamage);
        }
        else
        {
            attackTarget = null;
            onAttack = null;
        }
        return attackTarget != null;
    }
    protected void DropItem()
    {
        GameObject obj = null;
        if (Random.Range(0, 100) < 30)
        { 
            obj = ObjectPoolManager.Instance.GetObject((KeyType)Random.Range(0, 2));
            Vector3 itemposition = this.transform.position;
            itemposition.y = 1f;
            obj.transform.position = itemposition;
        }
    }
    public virtual void TakeDamage(float damage)
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
        }
    }
    public void AttackFunc()
    {
        PlayAttackSound();
        OnAttack?.Invoke();
    }
    private void objectReturn()
    {
        ObjectPoolManager.Instance.ReturnObject(this.gameObject, keyType);
    }
    #endregion Func

    #region AudioFunc
    protected void PlayAggravationSound()
    {
        audioSource.clip = audioClips[0];
        PlayAudioSource();
    }
    public void PlayAttackSound()
    {
        audioSource.clip = audioClips[1];
        PlayAudioSource();
    }

    protected void PlayDieSound()
    {
        audioSource.clip = audioClips[2];
        PlayAudioSource();
    }

    private void PlayAudioSource()
    {
        audioSource.Play();
    }
    #endregion AudioFunc
    /*private void OnDrawGizmos()
    {
        //추적 범위
        Gizmos.color = Color.blue;
        Vector3 onwertrasform = transform.position;
        onwertrasform.y = transform.position.y + 1f;
        Gizmos.DrawWireSphere(transform.position, TargetInRage);
        //공격 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(onwertrasform, AttackRange);
    }*/
}
