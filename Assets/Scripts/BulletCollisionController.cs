using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour {
	
	public GameObject owner;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			PlayerStateController playerState = col.gameObject.GetComponent<PlayerStateController>();
			if(playerState.IsAlive) Destroy(gameObject);
			playerState.KillPlayer(owner.GetComponent<PlayerStateController>());
		}
		if(col.gameObject.tag == "Floor")
		{
			Destroy(gameObject);
		}
	}
}
