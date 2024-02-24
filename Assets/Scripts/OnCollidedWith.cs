using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnCollidedWith : MonoBehaviour
{

    float timer;
    bool startTimer;
    public float timeToReset;
    public Collider nonTriggerCollider;
    public GameObject cooldownTimerText;
    GameObject spawnedTxt;
    bool collided;

    private void Start()
    {
        spawnedTxt = Instantiate(cooldownTimerText, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), Quaternion.Euler(90,0,0));
        spawnedTxt.GetComponent<FollowPlayer>().target = transform;
        spawnedTxt.SetActive(false);
    }

    public void Collided(float stunTime)
    {
        if (!collided)
        {
            collided = true;
            GetComponent<PlayerController>().enabled = false;
            //nonTriggerCollider.enabled = false;
            startTimer = true;
            timer = stunTime;
            Debug.Log("collided");
            spawnedTxt.SetActive(true);
        }

    }

    private void Update()
    {
        if (startTimer)
        {
            GetComponent<PlayerController>().playerNumberText.gameObject.SetActive(false);
            timer -= Time.deltaTime;
            float timerText = Mathf.Round((timer) * 10f) * 0.1f;
            spawnedTxt.GetComponent<TextMeshPro>().text = (timerText).ToString();

            //Debug.Log(timer);

            if (timer <= 0)
            {

                GetComponent<PlayerController>().playerNumberText.gameObject.SetActive(true);
                timer = 0;
                GetComponent<PlayerController>().enabled = true;
                GetComponent<Rigidbody>().isKinematic = false;
                nonTriggerCollider.enabled = true;
                spawnedTxt.SetActive(false);
                collided = false;
                startTimer = false;
            }
        }
    }

}
