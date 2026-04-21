using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftA : MonoBehaviour
{
    private float speed = 30;
	private PlayerControllerA playerControllerScript;
	private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
		playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerA>();
    }

    // Update is called once per frame
    void Update()
    {
		if (playerControllerScript.gameOver == false)
		{
			if (playerControllerScript.doubleSpeed)
			{
				transform.Translate(Vector3.left * Time.deltaTime * (speed * 8));
			}
			else 
			{
				transform.Translate(Vector3.left * Time.deltaTime * speed);
			}
			
		}

		if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
		{
			Destroy(gameObject);
		}
    }
}
