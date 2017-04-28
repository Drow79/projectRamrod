using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public bool isAlive = true;
	public bool isRespawnable = true;

	public const float maxHealth = 1000;
	[SyncVar(hook = "OnChangeHealth")]
	public float currentHealth = maxHealth;
	public RectTransform healthBar;

	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			isAlive = false;
			Debug.Log("Health on an object has been depleted");

			if (isRespawnable)
			{
				// called on the Server, but invoked on the Clients
				RpcRespawn();
			}
		}
	}

	void OnChangeHealth (float health)
	{
		healthBar.sizeDelta = new Vector2(GetHealthPercentage(), healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// move back to zero location
			transform.position = Vector3.zero;
		}
	}

	public float GetHealthPercentage()
	{
		return (currentHealth / maxHealth) * 100;
	}
}
