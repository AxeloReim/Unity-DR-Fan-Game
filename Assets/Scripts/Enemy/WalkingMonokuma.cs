using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonokuma : MonoBehaviour {

    public float speed;
    private int direction;
    Animator animator;

	// Use this for initialization
	void Start () {
        direction = 1;

        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        if (direction == 1)
        {
            animator.SetInteger("dir", 1);
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if(direction == 2)
        {
            animator.SetInteger("dir", 2);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Attack").GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            if(direction == 1)
            {
                direction = 2;
            }
            else if(direction == 2)
            {
                direction = 1;
            }
        }
    }
}
