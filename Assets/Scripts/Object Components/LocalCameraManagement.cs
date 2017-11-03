using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.Networking;

public class LocalCameraManagement : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (GetComponentInParent<NetworkIdentity>().isLocalPlayer)
		{
			GetComponent<Camera>().enabled = true;
			GetComponent<AudioListener>().enabled = true;
		}
	}
}
