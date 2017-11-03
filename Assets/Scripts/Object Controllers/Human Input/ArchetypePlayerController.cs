using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class ArchetypePlayerController : NetworkBehaviour {
	
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	// Update is called once per frame
	void Update()
	{
		if (!isLocalPlayer)
			return;

		// Basic movement
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 4.0f;
		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		// Commands
		if (Input.GetKey(KeyCode.Space))
		{
			CmdFire();
		}
		/*
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			
		}
		*/
	}

	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 3 seconds
		Destroy(bullet, 3.0f);
	}

	/*
	public override void OnStartLocalPlayer()
	{
		GetComponent<Health>().UIHealthBarForeground.GetComponent<Image>().color = Color.yellow;
		//GetComponent<MeshRenderer>().material.color = Color.yellow;
	}
	*/
}
