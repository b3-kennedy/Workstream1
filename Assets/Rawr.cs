using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rawr : MonoBehaviour
{

    public int TOTALp1;
    public int TOTALp2;

    public bool parked = false;
    public GameObject prefabP1;
    public GameObject prefabP2;

    public GameObject target;

    UI uii;
    
    public GameObject Player1;
    public GameObject Player2;

    public GameObject spawnpointP1;
    public GameObject spawnpointP2; 
    // Start is called before the first frame update
    void Start()
    {
        uii = GameObject.FindObjectOfType<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (parked)
        {

        }
        else if (other.transform.gameObject.tag == "Player")
        {
            TOTALp1++;
            //  Debug.Log("Total: " + TOTALp1);
            uii.p1Score++;
            Instantiate(prefabP1, target.transform);
            Player1.transform.position = spawnpointP1.transform.position;

            parked = true;
        }
        else if (other.transform.gameObject.tag == "Player2")
        {
            uii.p2Score++;
            Instantiate(prefabP2, target.transform);
            Player2.transform.position = spawnpointP2.transform.position;

            parked = true;
        }
    }



}
