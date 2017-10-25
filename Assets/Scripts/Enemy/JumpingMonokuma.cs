using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMonokuma : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    Animator anim;

    Rigidbody2D rigid;

    public int direction;

    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Jumping();
    }

    void Jumping()
    {
        if(direction == 0)
        {
            anim.SetInteger("dir", 0);
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            //rigid.MovePosition(Vector2.left * speed);
            //rigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
        else if(direction == 1)
        {
            anim.SetInteger("dir", 1);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            if(direction == 0)
            {
                direction = 1;
            }
            else if(direction == 1)
            {
                direction = 0;
            }
        }

        if(collision.gameObject.tag == "Ground")
        {
            rigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
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
