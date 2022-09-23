
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float damage;
    [SerializeField] private float rate;
    [SerializeField] private float curBullet;
    [SerializeField] private float totalBullet;
    [SerializeField] private float maxBullet;
    [SerializeField] private LayerMask layerMask;
    private ParticleSystem[] muzzleFlash;
    private bool rateOn = true;
    private RaycastHit hit;
    public float CurBullet { get { return curBullet; } }
    public float TotalBullet { get { return totalBullet; } }
    public float MaxBullet { get { return maxBullet; } }
    private void Awake()
    {
        curBullet = maxBullet;
    }
    private void OnEnable()
    { 
        muzzleFlash = GetComponentsInChildren<ParticleSystem>(true);
        rateOn = true;
    }
    public virtual void Shoot(int curGunNum)
    {
        if (curBullet == 0) return;
        if (rateOn)
        {
            curBullet--;
            ParticleOn();
            StartCoroutine(RateOfFire());
            int bulletCount = 1;
            int i = 0;
            if (curGunNum == 2)
            {
                bulletCount = 20;
            }
            IDamagable target = null;
            while (i < bulletCount)
            {
                Vector3 direction = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f)); ;
              
                if (Physics.Raycast(cam.transform.position, cam.transform.forward + direction, out hit, Mathf.Infinity, layerMask))
                {
                    GameObject hiteff;
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Monster"))
                    {
                        target = hit.transform.GetComponentInParent<IDamagable>();
                        hiteff = ObjectPoolManager.Instance.GetObject(KeyType.EffMonsterHit);
                    }
                    else
                    {
                        hiteff = ObjectPoolManager.Instance.GetObject(KeyType.EffObjectHit);
                    }
                    hiteff.transform.position = hit.point;
                    target?.TakeDamage(damage);
                }
                i++;
                Debug.DrawRay(cam.transform.position, (transform.forward + direction) * 15f, Color.red, 1f);
            }
            
        }
    }
    public void Reload(int curGunNum)
    {
        if (curBullet == maxBullet) return;
        float subAmmo = -(curBullet - maxBullet);
        if (totalBullet <= subAmmo) { subAmmo = totalBullet; }
        curBullet += subAmmo;
        totalBullet -= subAmmo;

    }
    public void ReloadShotGun()
    {
        curBullet += 1;
        totalBullet -= 1;
    }
    public void AddTotalAmmo(float addAmmo)
    {
        totalBullet += addAmmo;
    }
    private void ParticleOn()
    {
        for (int i = 0; i< muzzleFlash.Length; i++)
        {
            muzzleFlash[i].Play();
        }
    }
    private IEnumerator RateOfFire()
    {
        rateOn = false;
        yield return new WaitForSecondsRealtime(rate);
        rateOn = true;
    }
}
