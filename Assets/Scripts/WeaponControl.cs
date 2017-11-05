using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour {

	public bool CanUseGuns;
	public bool CanUseMeleeWeapons;
	
	public GameObject Weapon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag == "Gun" && CanUseGuns)
		{
			Weapon = col.gameObject;
		}
    }
}
