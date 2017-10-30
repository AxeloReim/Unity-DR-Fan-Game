using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLV1 : MonoBehaviour {

    public Transform Target;
    public GameObject Projectile;
    public float ProjectilePower;
    public float speed;
    public bool canAttack;
    public bool invincibleFrame;
    public int HP;

    private GameObject newProjectile;
    private float rng;
    private float attackTimer = 5f;
    private float specialAttackTimer = 3f;
    private Vector3 MovingDirection = Vector3.left;
    private float invincibleTimer = 1f;

    Animator anim;
    SpriteRenderer spriteRenderer;
    //Animation animate;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        invincibleFrame = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (Target)
        {
            float dist = Vector3.Distance(Target.position, transform.position);
            //Debug.Log(dist);
            if(dist < 13f)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().speed = 0;
                anim.SetBool("ViewPlayer", true);
                bool isPlaying = this.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk Left");

                if (isPlaying)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().speed = 1;
                    Movement();
                    Attack();
                    if (HP < 3)
                    {
                        specialAttackTimer -= Time.deltaTime;
                        if (specialAttackTimer <= 0)
                        {
                            rng = Random.Range(1f, 6f);
                            specialAttackTimer = rng;
                            canAttack = true;
                        }
                        SpecialAttack();
                    }
                }
                else
                {
                    //Debug.Log(isPlaying);
                }
                
            }
        }

        InvicibleFrame();

        

        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0)
        {
            attackTimer = 5f;
            canAttack = true;
        }
    }

    void Movement()
    {
        //transform.Translate(-speed * Time.deltaTime, 0, 0);
        //rng = Random.Range(107, 115);
        //float x1 = 107f;
        //float x2 = 115f;
        //Debug.Log(rng);
        if (transform.position.x > 104f)
        {
            MovingDirection = Vector3.left;
        }
        else if (transform.position.x < 101f)
        {
            MovingDirection = Vector3.right;
        }
        transform.Translate(MovingDirection * Time.smoothDeltaTime);
    }

    void Attack()
    {
        rng = Random.Range(0, -3.8f);
        if (canAttack == false)
        {
            return;
        }

        Vector3 pos = new Vector3(transform.position.x, rng, 0);
        newProjectile = Instantiate(Projectile, pos, transform.rotation);
        newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * ProjectilePower);

        canAttack = false;
    }

    void SpecialAttack()
    {
        rng = Random.Range(0, -3.8f);
        if (canAttack == false)
        {
            return;
        }

        Vector3 pos = new Vector3(transform.position.x, rng, 0);
        newProjectile = Instantiate(Projectile, pos, transform.rotation);
        newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * ProjectilePower);
    }

    void InvicibleFrame()
    {
        if (invincibleFrame == true)
        {
            invincibleTimer -= Time.deltaTime;
            rng = Random.Range(0, 100);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            if (invincibleFrame == false)
            {
                invincibleFrame = true;
                HP--;
            }

            if (HP == 0)
            {
                Destroy(gameObject);
            }
            GameObject.FindGameObjectWithTag("Attack").GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }
}
