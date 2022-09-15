using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour
{
    public enum State { Idle, Trace, Attack, Hit, Die }
    protected State curState;
    [Header("Stats")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float hp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected int attackPattern;
    [Header("target & range")]
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField, Range(0f, 10f)] protected float targetInRage;
    [SerializeField, Range(0f, 10f)] protected float attackRange;
    protected GameObject traceTarget = null;
    protected GameObject attackTarget = null;
    protected NavMeshAgent agent;
    protected Animator animator;
    public LayerMask TargetLayerMask { get { return targetLayerMask; } }
    public float AttackRange { get { return attackRange; } }
    public float AttackDamage { get { return attackDamage; } }
    public float TargetInRage { get { return targetInRage; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public Animator Animator { get { return animator; } }
    public float MaxHp { get { return maxHp; } }
    public virtual float Hp
    {
        get { return hp; }
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
                }
            
        }
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Hp = MaxHp;
        curState = State.Idle;
        ChangeState(curState);
    }
    private void Update()
    {
        Debug.Log("상태 : " + curState.ToString());
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
        Collider[] target = Physics.OverlapSphere(onwertransform, TargetInRage, TargetLayerMask);
        if (target.Length > 0)
        {
            traceTarget = target[0].gameObject;
        }
        else
        {
            traceTarget = null;
        }
        return traceTarget != null;
    }
    protected bool FindAttackTarget()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, AttackRange, TargetLayerMask);
        if (target.Length > 0)
        {
            attackTarget = target[0].gameObject;
        }
        else
        {
            attackTarget = null;
        }
        return attackTarget != null;
    }
    public void TakeDamage(float damage)
    {

            if (Hp > 0)
            {
                Hp -= damage;
            }

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
