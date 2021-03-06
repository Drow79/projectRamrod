﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.UI;

public enum ObjectAction {None, PlayerRespawn, Destroy};

public class Health : NetworkBehaviour {

	public float maxHealth = 1000;
	[SyncVar(hook = "OnChangeHealth")]
	public float health = 1000;
	public ObjectAction deathAction;
	public GameObject UIHealthBarForeground;

	bool isAlive;

	// Use this for initialization
	void Start()
	{
		UpdateUI(); // Ensures the UI Health Bar is correct upon instantiation
	}
		
	public void TakeDamage(int amount)
	{
		// Function call works only on server
		if (!isServer)
			return;

		health -= amount;
		if (health <= 0)
		{
			isAlive = false;
			Debug.Log(this.gameObject.name+": deathAction = "+deathAction.ToString());
			if (deathAction == ObjectAction.PlayerRespawn)
			{
				RpcRespawn();
				health = maxHealth;
			    UpdateUI();
            }
			else if (deathAction == ObjectAction.Destroy)
			{
				Destroy(gameObject);
			}
		}
		else if (health > 0)
		{
			isAlive = true;
		}
	}

	// Called when health variable changes via network SyncVar
	void OnChangeHealth(float health)
	{
		UpdateUI();
	}

	void UpdateUI()
	{
		RectTransform healthBarSize = UIHealthBarForeground.GetComponent<RectTransform>();
		healthBarSize.sizeDelta = new Vector2(GetHealthPercentage(), healthBarSize.sizeDelta.y);
	}

	// Called on the Server, but invoked on the Clients
	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// move back to zero location
			transform.position = Vector3.zero;
		}
	}

	float GetHealthPercentage()
	{
		return (health / maxHealth) * 100;
	}

	public override void OnStartLocalPlayer()
	{
		UIHealthBarForeground.GetComponent<Image>().color = Color.yellow;
	}
}
