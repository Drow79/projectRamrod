using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class ArchetypeSpawner : NetworkBehaviour {

	public GameObject prefab;
	public int Number = 10;
	public float RandomPositionRange = 10.0f;

	public override void OnStartServer()
	{
		for (int i=0; i < Number; i++)
		{
			float spawnerX = transform.position.x;
			float spawnerZ = transform.position.z;
			
			var spawnPosition = new Vector3(
				Random.Range(
					spawnerX - RandomPositionRange / 2,
					spawnerX + RandomPositionRange / 2
				),
				0.0f,
				Random.Range(
					spawnerZ - RandomPositionRange / 2,
					spawnerZ + RandomPositionRange / 2
				)
			);
			
			var spawnRotation = Quaternion.Euler( 
				0.0f, 
				Random.Range(0,360), 
				0.0f
			);

			var newObject = (GameObject)Instantiate(prefab, spawnPosition, spawnRotation);
			NetworkServer.Spawn(newObject);
		}
	}
}
