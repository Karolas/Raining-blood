using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	
	public bool IsPistol;
	public bool IsSword;
	
	public string Name;
	
	public int Durability;
	
	public float RespawnTime;
	
	public GameObject Bullet;
	
	public bool available = true;
	
	public GameObject TakeGun()
	{
		if(available)
		{
			available = false;
			GetComponent<SpriteRenderer>().enabled = false;
			StartCoroutine(EnableGun());
			return gameObject;
		}
		else
		{
			return null;
		}
	}
	
	IEnumerator EnableGun()
	{
		Debug.Log("Called");
		yield return new WaitForSeconds(RespawnTime);
		available = true;
		GetComponent<SpriteRenderer>().enabled = true;
	}
}
