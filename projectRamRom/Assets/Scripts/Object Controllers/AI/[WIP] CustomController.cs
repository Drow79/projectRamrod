﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class CustomController : MonoBehaviour {

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
		/*
		GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("Enemy");
		Transform[] potentialTargetPositions;

		foreach (GameObject potentialTarget in potentialTargets)
		{
			potentialTargetPositions.add() 
		}

		// Basic movement
		Vector3 targetDir = GetClosestObjectPosition(potentialTargets) - transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
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

    Transform GetClosestObjectPosition(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
