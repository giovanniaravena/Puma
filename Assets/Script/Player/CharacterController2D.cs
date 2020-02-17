using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_GroundCheck2;							// Una posición que marca donde verificar si el player está conectado a tierra con patas traseras.*/
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
																				// Una posición que marca donde verificar los techos
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Si el jugador está conectado a tierra o no con las patas delanteras*/
	private bool hind_legs;  			// Si el jugador está conectado a tierra o no con las patas traseras*/
	private bool m_Border;				//si el jugador estuvo al borde
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	public UnityEvent OnBorderEvent;



	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		
		if (OnBorderEvent == null)
			OnBorderEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
		
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		bool wasBorder = m_Border;
		m_Grounded = false;
		m_Border = false;
		hind_legs = false;		//patas traseras (true: en contacto con tierra)
		//bool front_legs= false;		//patas delanteras (true: en contacto con tierra)
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		/*colliders = patas delanteras*/
		for (int i = 0; i < colliders.Length; i++){
			if (colliders[i].gameObject != gameObject){ 	//si colisiona con otro objeto diferente al del jugador
				if (!wasGrounded)								//si no estuvo conectado a tierra invoca OnLanding{dejar de saltar}
					OnLandEvent.Invoke();
				m_Grounded = true;							//entonces está conectado a tierra
				//front_legs = true;
				
			}
		}
		Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_GroundCheck2.position, k_GroundedRadius, m_WhatIsGround); //colisión de patas traseras
		for (int i = 0; i < colliders2.Length; i++){
			if (colliders2[i].gameObject != gameObject){ 	//si patas traseras están conectadas a tierra
				if(!m_Grounded){								//si la patas delanteras no están conectadas a tierras
					OnBorderEvent.Invoke();
					//OnLandEvent.Invoke();					
					m_Border = true;							//puma se desestabiliza y cae
				}
				if(wasBorder){
					//OnBorderEvent.Invoke();					//puma se desestabiliza y cae
				}
				
				hind_legs = true;							//entonces está conectado a tierra
			}
		}

		/*
		colliders2 = patas traseras
		
		if collider2 deja de collisionar con otro objeto diferente al del jugador && collider está conectado && no hay salto de por medio
		entonces avanzar al jugador hasta que caiga
		 */

	}


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
                Debug.Log("Hay un objeto que está tocando el techo del puma");
			}
		}

		//solo controla al player si está conectado a tierra o airControl está activado
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump || hind_legs && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			hind_legs = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
		/*if (attack1){

			if(jump){
				Attack1_Jump.Invoke();
			}else if(crouch){
				Attack1_Crouch.Invoke();
			}else{
				Attack1_Idle.Invoke();
			}

		}*/
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
