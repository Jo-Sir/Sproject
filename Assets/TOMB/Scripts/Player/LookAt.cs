using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
	[SerializeField] private GameObject targetEnInfo;

    void Update()
	{
		//this.transform.LookAt(targetEnInfo.transform.position);

		LookTarget();
	}
	private void LookTarget()
	{
		if (targetEnInfo != null)
		{
			Vector3 dir = targetEnInfo.transform.position - this.transform.position;
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
		}
	}
}
