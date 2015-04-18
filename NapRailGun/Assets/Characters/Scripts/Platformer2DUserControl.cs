using System;
using UnityEngine;

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		private bool m_Shield;

		public String jump = "Jump1";
		public String fire = "Fire1";
		public String shield = "Shield1";
	 	public String axisHorizontal = "Horizontal1";
		public String axisVertical = "Vertical1";

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
			m_Jump = Input.GetButtonDown(jump);
            }
		if (Input.GetButtonDown (shield)) {
				m_Shield = true;
		} else if (Input.GetButtonUp (shield)) {
				m_Shield = false;
			}
        }


        private void FixedUpdate()
        {
            // Read the inputs.
			bool crouch = false; //Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis(axisHorizontal);
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump, m_Shield);
            m_Jump = false;
        }
    }
