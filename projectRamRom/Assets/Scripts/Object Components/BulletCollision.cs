using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {

	public float bulletSizeCollisionDecrement = 0.01F;

	float bulletSize = 0;

	// Use this for initialization
	void Start()
	{
		bulletSize = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
	}

	void OnCollisionEnter(Collision collision)
	{
		var colliderHealth = collision.gameObject.GetComponent<Health>();

		bulletSize -= bulletSizeCollisionDecrement;
		if (colliderHealth != null)
		{
			colliderHealth.TakeDamage(10);
			transform.localScale = new Vector3(
				bulletSize,
				bulletSize,
				bulletSize
			);
		}
		if (bulletSize <= 0)
		{
			Destroy(gameObject);
		}
	}
}