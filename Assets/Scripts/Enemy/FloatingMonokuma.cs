using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMonokuma : MonoBehaviour {

    public float speed;
    public float amplitude;

    private float y0,x0;
    private Vector3 tempPos;

    // Use this for initialization
    void Start()
    {
        y0 = transform.position.y;
        x0 = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        tempPos.y = y0 + amplitude * Mathf.Sin(speed * Time.time);
        tempPos.x = x0;
        transform.position = tempPos;
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
