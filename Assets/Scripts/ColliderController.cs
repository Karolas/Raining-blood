using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(9, 10);
		Physics2D.IgnoreLayerCollision(9, 11);
		Physics2D.IgnoreLayerCollision(9, 12);
		Physics2D.IgnoreLayerCollision(10, 11);
		Physics2D.IgnoreLayerCollision(10, 12);
		Physics2D.IgnoreLayerCollision(11, 12);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
