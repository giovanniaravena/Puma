using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public char direction = 'R';
    public bool slash = false;
	
	public CharacterController2D controller;
	public Animator animator;
	public ManaBar ManaBar;	
    public HealthBar HealthBar;
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
            direction = 'R';
		}
		if(horizontalMove<0){
			lastDirection=-1;
            direction = 'L';
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
            if (slash) return;
            if (crouch) return;
            if (!ManaBar.mana.TrySpendMana(20)) return;
            slash = true;
            Invoke("Attack",0.2f);
			if(animator.GetBool("IsJumping") || animator.GetBool("IsAttack1_Jump") ){
				animator.SetBool("IsJumping",false);
				animator.SetBool("IsAttack1_Jump",true);
				
			}else if(animator.GetFloat("Speed") < 0.01 && !animator.GetBool("IsCrouching")){
				animator.SetTrigger("Attack1_Idle");
			}else if(animator.GetFloat("Speed") > 0.01 && !animator.GetBool("IsCrouching")){
				animator.SetTrigger("Attack1_Run");
			}
			
		}

	}

    
    void Attack()
    {
        Vector3 position = Vector3.zero;
        if (direction == 'R')
        {
            position.Set(transform.position.x + 2, transform.position.y - 0.25f, 1);
        }
        else
        {
            position.Set(transform.position.x - 2, transform.position.y - 0.25f, 1);
        }
        GameObject obj = Instantiate(Resources.Load("Slash") as GameObject, position, Quaternion.identity);
        obj.GetComponent<SlashController>().setPlayer(this);

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

    public void GetDamage(int amount)
    {
        HealthBar.SetHealth(amount);
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        for (int n = 0; n < 6; n++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                
            yield return new WaitForSeconds(0.01f);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.01f);
        }
    }


	
}
