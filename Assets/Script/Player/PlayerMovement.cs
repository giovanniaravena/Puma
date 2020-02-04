using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	
	public CharacterController2D controller;
	public Animator animator;
	public ManaBar ManaBar;	

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	int lastDirection = 0;
	bool jump = false;
	bool crouch = false;
	//bool attack1 = false;
	

	// Update is called once per frame
	void Update () {

		Move();

	}

	void Move(){
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		if(horizontalMove>0){
			lastDirection=1;
		}
		if(horizontalMove<0){
			lastDirection=-1;
		}

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsAttack1_Jump",false);
			animator.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if (Input.GetButtonDown("Fire1"))
		{
			ManaBar.mana.TrySpendMana(50);
			//attack1 = true; /*para evitar conflicto en el evento OnBorder*/
			if(animator.GetBool("IsJumping") || animator.GetBool("IsAttack1_Jump") ){
				animator.SetBool("IsJumping",false);
				animator.SetBool("IsAttack1_Jump",true);
				//Debug.Log(animator.GetBool("IsJumping"));
			}else if(animator.GetFloat("Speed") < 0.01 && !animator.GetBool("IsCrouching")){
				animator.SetTrigger("Attack1_Idle");
			}else if(animator.GetFloat("Speed") > 0.01 && !animator.GetBool("IsCrouching")){
				animator.SetTrigger("Attack1_Run");
			}
			
		}

	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}


	public void OnLanding () {
		animator.SetBool("IsJumping", false);
		animator.SetBool("IsAttack1_Jump",false);
	}

	public void OnCrouching (bool isCrouching) {
		animator.SetBool("IsCrouching", isCrouching);
	}

	public void OnBorder () {
		
		animator.SetBool("IsAttack1_Jump",false);
		animator.SetBool("IsJumping", true);
		horizontalMove = lastDirection * runSpeed;
		//attack1 = false; 
	}



	
}
