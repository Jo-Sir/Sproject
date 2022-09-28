using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private DropItem item;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range;
    [SerializeField] private KeyType keyType;
    private void Awake()
    {
        ObjectPoolManager.Instance.returnObjectAll += objectReturn;
    }
    private void Update()
    {
        PlayerCheck();
    }
    #region Func
    private void PlayerCheck()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, range, layerMask);
        if (target.Length > 0)
        {
            Vector3 targetVec = new Vector3(target[0].transform.position.x, target[0].transform.position.y + 3f, target[0].transform.position.z);
            float distanceToTarget = (targetVec - transform.position).sqrMagnitude;
            if (distanceToTarget > Mathf.Pow(0.1f, 2))
            {
                // 등속이동
                transform.position = Vector3.MoveTowards(transform.position, targetVec, 1f);
                if (distanceToTarget < Mathf.Pow(1f, 2))
                {
                    item.Use();
                    // 오브젝트 풀링의 반환
                    ObjectPoolManager.Instance.ReturnObject(this.gameObject, keyType);
                }
            }
        }
    }
    private void objectReturn()
    {
        ObjectPoolManager.Instance.ReturnObject(this.gameObject, keyType);
    }
    #endregion Func
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
