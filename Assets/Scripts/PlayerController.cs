using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

	public int JumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
		playerRb = GetComponent<Rigidbody>();
		playerAnim = GetComponent<Animator>();
		Physics.gravity *= gravityModifier;
		playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2 && !gameOver)
		{
			playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isOnGround = false;
			playerAnim.SetTrigger("Jump_trig");
			dirtParticle.Stop();
			playerAudio.PlayOneShot(jumpSound, 0.4f);
			JumpCount += 1;
			Debug.Log(JumpCount);
		}
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isOnGround = true;
			dirtParticle.Play();
			JumpCount = 0;
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
