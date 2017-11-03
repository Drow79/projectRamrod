using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class RandomController : MonoBehaviour {

	public float speed = 1;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	float timeSinceLastAction = 0;
	int rotationMagnitude = 1000;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{



		// Basic movement
		var x = Random.Range(-rotationMagnitude, rotationMagnitude) * Time.deltaTime;
		var z = Time.deltaTime * speed;
		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		// Commands
		if (Random.Range(1, 6) == 1)
		{
			CmdFire();
		}

		/* Timer
		timeSinceLastAction += Time.deltaTime;
		while (timeSinceLastAction > 2)
		{

			timeSinceLastAction -= 2;
		}
		*/
	}

	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);
		bullet.GetComponent<MeshRenderer>().material.color = Color.red;

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 5;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 3 seconds
		Destroy(bullet, 1.5f);
	}
}
