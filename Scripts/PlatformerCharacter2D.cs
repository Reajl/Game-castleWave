using System;
using UnityEngine;
namespace UnityStandardAssets._2D
{
	public class PlatformerCharacter2D : MonoBehaviour
	{
		                 
		[SerializeField] private float m_JumpForce = 400f;                 
		[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  
		[SerializeField] private bool m_AirControl = false;                 
		[SerializeField] private LayerMask m_WhatIsGround;               

		private Transform m_GroundCheck;    
		const float k_GroundedRadius = .2f; 
		private bool m_Grounded;            
		private Transform m_CeilingCheck;  
		const float k_CeilingRadius = .01f; 
		private Animator m_Anim;            
		private Rigidbody2D m_Rigidbody2D;
		private bool m_FacingRight = true;  
     
		Transform playerGraphics;  

		void Awake()
        {
  
			
			m_GroundCheck = transform.Find("GroundCheck");
			m_CeilingCheck = transform.Find("CeilingCheck");
			m_Anim = GetComponent<Animator>();
			m_Rigidbody2D = GetComponent<Rigidbody2D>();
			playerGraphics = transform.Find ("Graphics");
			if (playerGraphics == null)
			{
				Debug.LogError ("Let's freak out! There is no 'Graphics' object as a child of player");
			}
		}

		void FixedUpdate()
		{
			m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			m_Anim.SetBool("Ground", m_Grounded);

		
			m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
		}


		public void Move(float move, bool crouch, bool jump)
		{
			
			if (!crouch && m_Anim.GetBool("Crouch"))
			{
				
				if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
				{
					crouch = true;
				}
			}
          
			
			m_Anim.SetBool("Crouch", crouch);

		
			if (m_Grounded || m_AirControl)
			{
				
				move = (crouch ? move*m_CrouchSpeed : move);

				
				m_Anim.SetFloat("Speed", Mathf.Abs(move));

			
                m_Rigidbody2D.velocity = new Vector2(move * PlayerStats.instance.movementSpeed, m_Rigidbody2D.velocity.y);

				
				if (move > 0 && !m_FacingRight)
				{
				
					Flip();
				}
				
				else if (move < 0 && m_FacingRight)
				{
				
					Flip();
				}
			}
			
			if (m_Grounded && jump && m_Anim.GetBool("Ground"))
			{
			
				m_Grounded = false;
				m_Anim.SetBool("Ground", false);
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			}
		}


		void Flip()
		{
			
			m_FacingRight = !m_FacingRight;

		
			Vector3 theScale = playerGraphics.localScale;
			theScale.x *= -1;
			playerGraphics.localScale = theScale;
		}
	}
}﻿ 