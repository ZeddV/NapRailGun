using System;
using UnityEngine;

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

		private bool m_Shield;

		//These get overriden
		public String jump = "";
		public String fire = "";
		public String shield = "";
	 	public String axisHorizontal = "";
		public String axisVertical = "";

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

		if (Input.GetKeyDown("x")) {	
			m_Character.die ();
		}

   }

	//private float last = 0;
        private void FixedUpdate()
        {
			// Read the inputs.
			bool crouch = false; //Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis(axisHorizontal);

			//if(!(last != 0 && h == 0)) {
			    // Pass all parameters to the character control script.
				//Debug.Log (h); 
	            m_Character.Move(h, crouch, m_Jump, m_Shield);
	            m_Jump = false;
			//}
		   //last = h;

			float v = Input.GetAxis (axisVertical);
			m_Character.playAnimVert (v,h);
        }
    }
