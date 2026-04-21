using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerA : MonoBehaviour
{
   private Rigidbody playerRb;
	private Animator playerAnim;
	public float jumpForce;
	public float gravityModifier;
	public bool isOnGround = true;
	public bool gameOver;
	public ParticleSystem explosionParticle;
	public ParticleSystem dirtParticle;
	public AudioClip jumpSound;
	public AudioClip crashSound;
	private AudioSource playerAudio;

	public bool doubleJumpUsed = false;
	public float doubleJumpForce;

    public bool doubleSpeed = false;

	private string animationParameter;

	// Start is called before the first frame update
	void Start()
	{
		playerRb = GetComponent<Rigidbody>();
		playerAnim = GetComponent<Animator>();
		Physics.gravity *= gravityModifier;
		playerAudio = GetComponent<AudioSource>();
		animationParameter = "Blow";
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
		{
			playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isOnGround = false;
			playerAnim.SetTrigger("Jump_trig");
			dirtParticle.Stop();
			playerAudio.PlayOneShot(jumpSound, 0.4f);
			doubleJumpUsed = false;
		}
		else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
		{
			doubleJumpUsed = true;
			playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
			playerAnim.Play("Standing_Jump", 3);
			playerAudio.PlayOneShot(jumpSound, 0.4f);
		}

		if (Input.GetKey(KeyCode.F))
		{
			doubleSpeed = true;
			playerAnim.SetFloat("Speed_M", 5.0f);
		}
		else if (doubleSpeed)
		{
			doubleSpeed = false;
			playerAnim.SetFloat("Speed_M", 1.0f);
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			playerAnim.SetTrigger("Crouch Trigger");
		}
		if(Input.GetKeyDown(KeyCode.H))
		{
			playerAnim.SetTrigger(animationParameter);
		}
		
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isOnGround = true;
			dirtParticle.Play();
		} else if (collision.gameObject.CompareTag("Obstacle"))
		{
			gameOver = true;
			Debug.Log("Game Over!");
			playerAnim.SetBool("Death_b", true);
			playerAnim.SetInteger("DeathType_int", 1);
			explosionParticle.Play();
			playerAudio.PlayOneShot(crashSound, 0.8f);
		}
	}
}
