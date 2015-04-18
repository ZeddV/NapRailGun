using System;
using UnityEngine;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Player2 : MonoBehaviour
{
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;
	private bool m_Shield;
	
	
	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2D>();

	}
	
	
	private void Update()
	{
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = Input.GetButtonDown("Jump2");
		}
		if (Input.GetKeyDown ("u")) {
			m_Shield = true;
		} else if (Input.GetKeyUp ("u")) {
			m_Shield = false;
		}
	}

	
	
	private void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		float h = Input.GetAxis("Horizontal2");
		// Pass all parameters to the character control script.
		m_Character.Move(h, crouch, m_Jump, m_Shield);
		m_Jump = false;
	}
}