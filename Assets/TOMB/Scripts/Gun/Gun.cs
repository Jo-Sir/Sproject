
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected Camera cam;
    [SerializeField] protected float damage;
    [SerializeField] protected float rate;
    [SerializeField] protected LayerMask layerMask;
    protected ParticleSystem muzzleFlash;
    protected Animator animator;
    protected bool rateOn = true;
    protected RaycastHit hit;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        rateOn = true;
    }
    public virtual void Shoot(int curGunNum)
    {

        if (rateOn)
        {
            //GameManager.Instance.shootRate++;
            GameManager.Instance.player.Animator.SetBool("Shoot", true);
            
            // muzzleFlash.Play();
            StartCoroutine(RateOfFire());
            // (시작점, 방향, 맞는객체의 인포, 사거리)
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
                    target = hit.transform.GetComponentInParent<IDamagable>();
                    accumDamage += damage;  
                }
                i++;
                Debug.DrawRay(cam.transform.position, transform.forward * 15f, Color.red, 1f);
            }
            target?.TakeDamage(accumDamage);
            //GameManager.Instance.player.Animator.SetBool("Shoot", false);
            // animator.SetTrigger("Idle");
            // GameManager.Instance.Accuracy();
        }
    }
    public void ReLord()
    {
        animator.SetTrigger("ReLord");
        animator.SetTrigger("Idle");
    }

    protected IEnumerator RateOfFire()
    {
        rateOn = false;
        yield return new WaitForSeconds(rate);
        rateOn = true;
    }
}
