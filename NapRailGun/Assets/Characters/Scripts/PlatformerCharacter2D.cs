using System;
using UnityEngine;

    public class PlatformerCharacter2D : MonoBehaviour
    {
		public GameObject statusControl;
		public StatusControl statusScript;

        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
       // private bool m_FacingRight = true;  // For determining which way the player is currently facing.

		private Transform m_RightCheck;
		private Transform m_LeftCheck;
		public bool m_SideCollisionRight;
		public bool m_SideCollisionLeft;

		private Transform m_Shield;

		public GameObject tombstonePrefab;
		public Texture2D tombstoneTexture;
		public RespawnScript respawnScript;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");

			m_RightCheck = transform.Find ("RightCheck");
			m_LeftCheck = transform.Find ("LeftCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

			m_Shield = transform.Find ("Shield");

			//Invoke ("die", 2f);
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            //Debug.Log(m_GroundCheck);
            //Debug.Log(m_GroundCheck.position);

            //Debug.Log(k_GroundedRadius);
            //Debug.Log(m_WhatIsGround);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject) 
                    m_Grounded = true;

            }
            //m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump, bool shield)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch)// && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            //m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                //m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
				if(move>0)
				{
					Collider2D[] colliders = Physics2D.OverlapCircleAll(m_RightCheck.position, k_GroundedRadius, m_WhatIsGround);
					
					for (int i = 0; i < colliders.Length; i++)
					{
						if (colliders[i].gameObject != gameObject) 
						{
							m_SideCollisionRight = true;
							//Debug.Log("Collision with " + colliders[i].gameObject.name);
						}
						
					}
					if (!m_SideCollisionRight)
	                	m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

					m_SideCollisionRight = false;
					colliders = null;
					
				}
				else if(move<0)
				{					
					Collider2D[] colliders = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_GroundedRadius, m_WhatIsGround);
					
					for (int i = 0; i < colliders.Length; i++)
					{
						if (colliders[i].gameObject != gameObject) 
						{
							m_SideCollisionLeft = true;
							//Debug.Log("Collision with " + colliders[i].gameObject.name);
						}
						
					}
					if (!m_SideCollisionLeft)
					{
						m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
						//Debug.Log ("NO Collision Left");
					}
					
					m_SideCollisionLeft = false;
					colliders = null;
				}
				
				
                // If the input is moving the player right and the player is facing left...
				/*
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
                */
            }


		if(m_Anim != null && m_Anim.isActiveAndEnabled) {
			if (move < 0)	{
				m_Anim.SetInteger ("IsMoving", 1);
			}else if (move > 0){
				m_Anim.SetInteger ("IsMoving", 2);
			}else{
				m_Anim.SetInteger("IsMoving",0);
			}
		}


            // If the player should jump...

            if (m_Grounded && jump)// && m_Anim.GetBool("Ground"))
            {
                //Debug.Log("JUMP!");
                // Add a vertical force to the player.
				
			gameObject.GetComponent<AudioSource>().Play();
                m_Grounded = false;
                //m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

			if(shield)
				m_Shield.GetComponent<SpriteRenderer> ().enabled = true;
			else
				m_Shield.GetComponent<SpriteRenderer> ().enabled = false;
        }

		public void setStatusControl(GameObject statusControl){
			
			Debug.Log ("StatusControl");
			this.statusControl = statusControl;
			this.statusScript = statusControl.GetComponent<StatusControl>();
		}


		public void die(){
			statusScript.die (respawnScript, tombstonePrefab);
		}

		public void playAnimVert(float v,float h){
				if (v > 0) {
				m_Anim.SetInteger ("Vertical", 1);
			} else if (v < 0) {
				m_Anim.SetInteger ("Vertical", 2);
			} else
				m_Anim.SetInteger ("Vertical", 0);
		
		}

		/*
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        */
    }
