using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {
	
	void OnCollisionEnter(Collision collision)
	{
		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();
		if (health  != null)
		{
			health.TakeDamage(10);
			transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
		}
	}
}