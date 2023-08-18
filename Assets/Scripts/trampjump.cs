using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampjump : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject[] player;
    private Rigidbody2D rb;

    private float trampForce = 29f;
    int i = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        rb = player[i].GetComponent<Rigidbody2D>();

        collision.GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, trampForce);
        anim.SetBool("tramptouch", true);
    }
    private void BackToIdle()
    {
        anim.SetBool("tramptouch", false);
    }
}
