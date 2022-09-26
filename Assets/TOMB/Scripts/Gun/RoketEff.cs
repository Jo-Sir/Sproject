using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoketEff : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    private void OnEnable()
    {
        Debug.Log("온에이블");
        //Attack();
    }
    private void Attack()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, range, layerMask);
        if (target.Length > 0)
        {
            for (int i = 0; i < target.Length; i++)
            {
                target[i]?.GetComponent<IDamagable>().TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
}
