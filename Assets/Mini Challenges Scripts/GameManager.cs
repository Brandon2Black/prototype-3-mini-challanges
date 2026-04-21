using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float score;
    private PlayerControllerA playerControllerScript;
    float timer;
    float delayAmount = .75f;

    // Player Start Animation Variables
    public Transform startingPoint;
    public float lerpAmount;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerA>();
        score = 0;
        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerControllerScript.gameOver)
        {
            timer += Time.deltaTime;
            if(timer >=  delayAmount)
            {
                timer = 0;
                
                if(playerControllerScript.doubleSpeed)
                {
                    score += 2;
                }else
                {
                    score += 1;
                }
                Debug.Log("Score:" + score);
            }
            
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpAmount;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_M", 0.5f);

        while(fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpAmount;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);

            yield return null;
        }

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_M", 1f);
        playerControllerScript.gameOver = false;

    }
}
