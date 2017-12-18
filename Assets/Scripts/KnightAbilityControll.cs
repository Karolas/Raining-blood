using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAbilityControll : MonoBehaviour {

	public bool IsDefending = false;
	public bool IsOnCooldown = false;
	public float DefendTime;
	public float AbilityReuseTime;
	
	public string Action1;
	
	private Animator animator;
	private WeaponControl weaponControl;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		weaponControl = GetComponent<WeaponControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton(Action1) && weaponControl.Weapon != null)
		{
			Defend();
		}
	}
	
	void Defend() {
		if(!IsDefending && !IsOnCooldown)
		{
			IsDefending = true;
			IsOnCooldown = true;
			animator.SetBool("IsDefending", true);
			StartCoroutine(StopDefending());
		}
	}
	
	IEnumerator StopDefending()
	{
		yield return new WaitForSeconds(DefendTime);
		IsDefending = false;
		animator.SetBool("IsDefending", false);
		yield return new WaitForSeconds(AbilityReuseTime);
		IsOnCooldown = false;
	}
}
