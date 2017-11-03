using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Camera camera = Camera.allCameras [Camera.allCamerasCount - 1];
		transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
			camera.transform.rotation * Vector3.up);
	}
}
