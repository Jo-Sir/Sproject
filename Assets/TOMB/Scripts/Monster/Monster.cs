using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Monster<T> : MonoBehaviour where T : Monster<T>, IDamagable
{
    #region State
    protected class BaseState : State<T>
    {
        public override void Enter(T Onwer)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(T Onwer)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(T Onwer)
        {
            throw new System.NotImplementedException();
        }
    }
    protected class IdleState : BaseState
    {
        public override void Enter(T Onwer)
        {
        }

        public override void Update(T Onwer)
        {
            if (FindTarget(Onwer)) Onwer.ChangeState(Monster<T>.State.Trace);
        }

        public override void Exit(T Onwer)
        {

        }
        protected bool FindTarget(T Onwer)
        {
            Vector3 onwertrasform = Onwer.transform.position;
            onwertrasform.y = Onwer.transform.position.y + 1f;
            Collider[] target = Physics.OverlapSphere(onwertrasform, Onwer.TargetInRage, Onwer.TargetLayerMask);
            if (target.Length > 0)
            {
                Onwer.target = target[0].gameObject;
            }
            else
            {
                Onwer.target = null;
            }
            return Onwer.target != null;
        }
    }
    protected class TraceState : BaseState
    {
        public override void Enter(T Onwer)
        {
            Onwer.animator.SetBool("Walk", true);
        }

        public override void Update(T Onwer)
        {
            if (Onwer.target != null) Onwer.agent.destination = Onwer.target.transform.position;
            if (FindAttackTarget(Onwer))
            {
                Onwer.ChangeState(Monster<T>.State.Attack);
            }
        }
        public override void Exit(T Onwer)
        {
            Onwer.animator.SetBool("Walk", true);
            Onwer.agent.ResetPath();
        }
        protected bool FindAttackTarget(T Onwer)
        {
            Collider[] target = Physics.OverlapSphere(Onwer.transform.position, Onwer.AttackRange, Onwer.TargetLayerMask);
            bool attackTarget;
            if (target.Length > 0)
            {
                attackTarget = true;
            }
            else
            {
                attackTarget = false;
            }
            return attackTarget;
        }
    }
    protected class AttackState : BaseState
    {
        public override void Enter(T Onwer)
        {
            Onwer.animator.SetBool("Attack", true);
            Onwer.animator.SetInteger("RanAttack", Random.Range(0, 3));
            Onwer.StartCoroutine(AttackTime(Onwer));
        }

        public override void Update(T Onwer)
        {

        }

        public override void Exit(T Onwer)
        {
            Onwer.animator.SetBool("Attack", false);
        }

        IEnumerator AttackTime(T Onwer)
        {
            yield return new WaitForSeconds(1.0f);
            Onwer.ChangeState(Monster<T>.State.Trace);
        }
    }
    protected class HitState : BaseState
    {
        public override void Enter(T Onwer)
        {
            Onwer.animator.SetBool("Hit", true);
        }

        public override void Update(T Onwer)
        {
            Onwer.ChangeState(Monster<T>.State.Idle);
        }

        public override void Exit(T Onwer)
        {
            Onwer.animator.SetBool("Hit", false);
        }
    }
    protected class DieState : BaseState
    {
        public override void Enter(T Onwer)
        {
            Onwer.StartCoroutine(ToDie(Onwer));
        }
        public override void Update(T Onwer)
        {

        }
        public override void Exit(T Onwer)
        {
        }
        IEnumerator ToDie(T Onwer)
        {
            Onwer.animator.SetTrigger("Die");
            yield return new WaitForSeconds(1.5f);
            Onwer.gameObject.SetActive(false);
        }
    }
    #endregion
    public enum State { Idle, Trace, Attack, Hit, Die }
    protected StateMachine<State, T> stateMachine;
    [Header("Stats")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float hp;
    [SerializeField] protected float moveSpeed;
    [Header ("target & range")]
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField, Range(0f, 10f)] protected float targetInRage;
    [SerializeField, Range(0f, 10f)] protected float attackRange;
    protected GameObject target = null;
    protected NavMeshAgent agent;
    protected Animator animator;
    public LayerMask TargetLayerMask { get { return targetLayerMask; } }
    public float AttackRange { get { return attackRange; } }
    public float TargetInRage { get { return targetInRage; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public Animator Animator { get { return animator; } }
    public float MaxHp { get { return maxHp; } }
    public float Hp 
    { 
        get { return hp; } 
        set 
        { 
            hp = value;
            if (hp <= 0)
            {
                ChangeState(State.Die);
            }
            else
            { 
                ChangeState(State.Hit);
            }
        } 
    }
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine<State, T>((T)this);
        stateMachine.AddState(State.Idle, new IdleState());
        stateMachine.AddState(State.Trace, new TraceState());
        stateMachine.AddState(State.Attack, new AttackState());
        stateMachine.AddState(State.Hit, new HitState());
        stateMachine.AddState(State.Die, new DieState());
        Hp = MaxHp;
        stateMachine.ChangeState(State.Idle);
    }

    private void Update()
    {
        Debug.Log(stateMachine.curState.ToString());
        stateMachine.Update();
    }
    public void ChangeState(State nextState)
    {
        stateMachine.ChangeState(nextState);
    }
    public void TakeDamage(float damage)
    {
        Hp -= damage;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 onwertrasform =  transform.position;
        onwertrasform.y = transform.position.y + 1f;
        Gizmos.DrawWireSphere(transform.position, TargetInRage);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(onwertrasform, AttackRange);
    }
}
