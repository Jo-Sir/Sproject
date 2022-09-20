
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float damage;
    [SerializeField] private float rate;
    [SerializeField] private LayerMask layerMask;
    private ParticleSystem[] muzzleFlash;
    private Animator gunAnimator;
    private Animator playerAnimator;
    private bool rateOn = true;
    private RaycastHit hit;
    private void OnEnable()
    { 
        gunAnimator = GetComponent<Animator>();
        muzzleFlash = GetComponentsInChildren<ParticleSystem>(true);
        rateOn = true;
    }
    public virtual void Shoot(int curGunNum)
    {
        if (rateOn)
        {
            ParticleOn();
            StartCoroutine(RateOfFire());
            int bulletCount = 1;
            int i = 0;
            if (curGunNum == 2)
            {
                bulletCount = 20;
            }
            IDamagable target = null;
            float accumDamage = 0;
            while (i < bulletCount)
            {
                Vector3 direction = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f)); ;
              
                if (Physics.Raycast(cam.transform.position, cam.transform.forward + direction, out hit, Mathf.Infinity, layerMask))
                {
                    GameObject hiteff;
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Monster"))
                    {
                        target = hit.transform.GetComponentInParent<IDamagable>();
                        accumDamage += damage;
                        hiteff = ObjectPoolManager.Instance.GetObject(KeyType.EffMonsterHit);
                    }
                    else
                        hiteff = ObjectPoolManager.Instance.GetObject(KeyType.EffObjectHit);
           
                    hiteff.transform.position = hit.point;
                }
                i++;
                Debug.DrawRay(cam.transform.position, (transform.forward + direction) * 15f, Color.red, 1f);
            }
            target?.TakeDamage(accumDamage);
        }
    }

    public void Reload(int curGunNum)
    {
        
    }
    private void ParticleOn()
    {
        for (int i = 0; i< muzzleFlash.Length; i++)
        {
            muzzleFlash[i].Play();
        }
    }
    protected IEnumerator RateOfFire()
    {
        rateOn = false;
        yield return new WaitForSecondsRealtime(rate);
        rateOn = true;
    }
}
