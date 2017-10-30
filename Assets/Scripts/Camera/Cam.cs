using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

    Transform player;
    public bool moveCam;

    //private float x1;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveCam = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(moveCam)
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

        if(player.transform.position.x >= 90.50f)
        {
            moveCam = false;
            transform.Translate(6 * Time.smoothDeltaTime, 0, 0);
            Vector3 pos = gameObject.transform.position;
            pos.x = 97.5f;
            if(gameObject.transform.position.x >= 97.4f)
            {
                gameObject.transform.position = pos;
            }
            
        }
        /*if(player.transform.position.x >= 101f)
        {
            Vector3 pos = gameObject.transform.position;
            pos.x = 101f;
            gameObject.transform.position = pos;
            moveCam = false;
            //transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        }*/
    }
}
