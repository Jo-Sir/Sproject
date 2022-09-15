using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private DropItem item;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range;
    private void Update()
    {
        PlayerCheck();
    }
    private void PlayerCheck()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, range, layerMask);
        if (target.Length > 0)
        {
            float distanceToTarget = (target[0].transform.position - transform.position).sqrMagnitude;
            
            if (distanceToTarget > Mathf.Pow(0.1f, 2))
            {
                // 등속이동
                transform.position = Vector3.MoveTowards(transform.position, target[0].transform.position, 0.05f);
                if (distanceToTarget < Mathf.Pow(1f, 2))
                {
                    item.Use();
                    // 오브젝트 풀링의 반환
                }
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
