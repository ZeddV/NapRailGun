using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float jumpX;
    public float jumpY;
    public float torqueMult;

    public AudioClip jumpSound;

    private bool canJump = false;

    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rigidBody.AddForce(new Vector2(jumpX,jumpY));
            GetComponent<Animator>().SetTrigger("Jump");
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
            canJump = false;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        rigidBody.AddTorque(-horizontalInput * torqueMult);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true; 
    }
}
