using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    public Component[] boxCollider2DChild;

    public float speed;
    public bool canMove;
    public bool invincibleFrame;
    public Image[] hp;
    public int maxHp;
    public int currHp;
    public float jumpHeight;
    
    private bool isAttacking;
    private bool onGround;
    private bool isJump;
    private float timer = .25f;
    private float invincibleTimer = 1f;
    

	// Use this for initialization
	void Start () {
        canMove = true;
        isAttacking = false;
        currHp = maxHp;
        onGround = true;
        isJump = false;
        invincibleFrame = false;
        
        GetHealth();

        boxCollider2DChild = GetComponentsInChildren<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim.SetInteger("dir", 0);
    }
	
	// Update is called once per frame
	void Update () {
        GetHealth();
        Movement();
        InvicibleFrame();
        Attack();

	}

    void Movement()
    {
        //if (!canMove)
        //return;

        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            transform.Translate(speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", 6);
            if (isAttacking == false)
            {
                speed = 5;
                timer = .25f;
            }
            else if (isAttacking == true)
            {
                speed = 0;
                timer -= Time.deltaTime;
                if (timer <= 0)
                    isAttacking = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
            {
                anim.Play("Jump");
                isJump = true;
                rigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                onGround = false;
            }

        }
        else if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", 4);
            if (isAttacking == false)
            {
                speed = 5;
                timer = .25f;
            }
            else if (isAttacking == true)
            {
                speed = 0;
                timer -= Time.deltaTime;
                if (timer <= 0)
                    isAttacking = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
            {
                anim.Play("Jump");
                isJump = true;
                rigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                onGround = false;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {
            anim.Play("Jump");
            isJump = true;
            rigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            onGround = false;

        }

        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.Play("Idle");
            anim.SetInteger("dir", 0);
        }

        anim.SetBool("IsJump", isJump);
        anim.SetBool("OnGround", onGround);
        anim.SetBool("IsAttacking", isAttacking);

    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnableChildComponent();
            isAttacking = true;
            anim.Play("Attack");

        }
        else
        {
            GameObject.FindGameObjectWithTag("Attack").GetComponentInChildren<BoxCollider2D>().enabled = false;
        }

        anim.SetBool("IsAttacking", isAttacking);
    }

    void GetHealth()
    {
        for(int i = 0; i<=hp.Length -1; i++)
        {
            hp[i].gameObject.SetActive(false);
        }
        for(int i = 0; i<=currHp -1; i++)
        {
            hp[i].gameObject.SetActive(true);
        }
    }
    
    void InvicibleFrame()
    {
        if (invincibleFrame == true)
        {
            invincibleTimer -= Time.deltaTime;
            int rng = Random.Range(0, 100);
            if (rng < 50)
                spriteRenderer.enabled = false;
            if (rng > 50)
                spriteRenderer.enabled = true;
            if (invincibleTimer <= 0)
            {
                invincibleTimer = 1f;
                invincibleFrame = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    void EnableChildComponent()
    {
        foreach (BoxCollider2D collider in boxCollider2DChild)
        {
            collider.enabled = true;
        }
    }

    void DisableChildComponent()
    {
        foreach (BoxCollider2D collider in boxCollider2DChild)
        {
            collider.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Flying Enemy")
        {
            if(invincibleFrame == false)
            {
                invincibleFrame = true;
                currHp--;
            }

            if(currHp == 0)
            {
                hp[0].gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle")
        {
            isJump = false;
            onGround = true;
        }

        anim.SetBool("OnGround", onGround);
    }

}
