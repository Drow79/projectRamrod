using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {

	public float bulletSizeCollisionDecrement = 0.01F;

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	float GetBulletArea()
	{
		return transform.localScale.x * transform.localScale.y * transform.localScale.z;
	}

	void OnCollisionEnter(Collision collision)
	{
		var colliderHealth = collision.gameObject.GetComponent<Health>();

		if (colliderHealth != null)
		{
			colliderHealth.TakeDamage(10);
			transform.localScale = new Vector3(
				transform.localScale.x - bulletSizeCollisionDecrement,
				transform.localScale.y - bulletSizeCollisionDecrement,
				transform.localScale.z - bulletSizeCollisionDecrement
			);
		}
		if (GetBulletArea() <= 0)
		{
			Destroy(gameObject);
		}
	}
}