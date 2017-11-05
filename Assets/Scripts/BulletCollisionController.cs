﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
		if(col.gameObject.tag == "Floor")
		{
			Destroy(gameObject);
		}
	}
}
