using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffOnDisable : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    [SerializeField] private float lifeTime;
    private void OnEnable()
    {
        StartCoroutine(ReturnObject());
    }

    private IEnumerator ReturnObject()
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPoolManager.Instance.ReturnObject(this.gameObject, keyType);
    }
}
