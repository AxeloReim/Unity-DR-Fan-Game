using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonokuma : MonoBehaviour {

    public float speed;

    private float timer = 30f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
	}

    void Movement()
    {
        if (gameObject.transform.localPosition.y > -0.96f)
        {
            transform.Translate(-speed * Time.deltaTime, (-speed +2) * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Attack").GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }
}
