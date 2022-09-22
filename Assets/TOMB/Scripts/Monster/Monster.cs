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
    [SerializeField, Range(0f, 50f)] protected float targetInRage;
    [SerializeField, Range(0f, 50f)] protected float attackRange;
    protected GameObject traceTarget = null;
    protected GameObject attackTarget = null;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected MeshCollider meshCollider;
    protected UnityAction onAttack;
    public LayerMask TargetLayerMask { get { return targetLayerMask; } }
    public float AttackRange { get { return attackRange; } }
    public float AttackDamage { get { return attackDamage; } }
    public float TargetInRage { get { return targetInRage; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public Animator Animator { get { return animator; } }
    public float MaxHp { get { return maxHp; } }
    public UnityAction OnAttack { get { return onAttack; } }
    public virtual float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                meshCollider.enabled = false;
                ChangeState(State.Die);
            }else if (Hp > 0 && Hp < MaxHp)
            {
                ChangeState(State.Hit);
                if (traceTarget is null)
                {
                    traceTarget = GameManager.Instance.player.gameObject;
                }
            }
        }
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        Hp = MaxHp;
        curState = State.Idle;
    }
    private void OnEnable()
    {
        meshCollider.enabled = true;
        ChangeState(curState);
    }
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
    public void TakeDamage(float damage)
    {
        if (Hp > 0)
        {
            Hp -= damage;
        }
    }
    public void AttackFunc()
    {
        OnAttack?.Invoke();
    }
    private void OnDrawGizmosSelected()
    {
        //추적 범위
        Gizmos.color = Color.blue;
        Vector3 onwertrasform = transform.position;
        onwertrasform.y = transform.position.y + 1f;
        Gizmos.DrawWireSphere(transform.position, TargetInRage);
        //공격 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(onwertrasform, AttackRange);
    }
}
