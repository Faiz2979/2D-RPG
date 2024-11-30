using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed=7;
    public float runSpeed=10;
    public float dash=15;
    public float xInput;
    public float yInput;
    public Rigidbody2D rb;
    public float dashCD=0.5f;
    [Header("Player Animation")]
    public Animator anim;
    public bool isRunning;
    public bool isDashing;
    public bool isAttacking;

    [Header("Player Facing")]
    public int facingDir = 1;
    public bool facingRight = true;

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleAnimation();
        handleDash();
        flipPlayer();
    }

    void handleMovement(){
        xInput=Input.GetAxis("Horizontal");
        yInput=Input.GetAxis("Vertical");
        rb.velocity=new Vector2(xInput*moveSpeed,yInput*moveSpeed);
        isRunning=Input.GetKey(KeyCode.LeftShift);
        if(isRunning){
            rb.velocity=new Vector2(xInput*runSpeed,yInput*runSpeed);
        }else{
            rb.velocity=new Vector2(xInput*moveSpeed,yInput*moveSpeed);
        }
    }

    void handleDash(){
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.velocity=rb.velocity.normalized*dash;
        }
    }

    void flipPlayer(){
    if (xInput > 0 && !facingRight)  // Jika bergerak ke kanan dan menghadap ke kiri, lakukan flip
    {
            flip();
    }
    else if (xInput < 0 && facingRight)  // Jika bergerak ke kiri dan menghadap ke kanan, lakukan flip
    {
        flip();
    }
}

    // Fungsi untuk melakukan flip player
    void flip()
    {
        facingDir *= -1;  // Ubah arah menghadap
        facingRight = !facingRight;  // Toggle arah
        transform.Rotate(0f, 180f, 0f);  // Putar player 180 derajat di sumbu Y
    }

    void handleAnimation(){
        anim.SetFloat("xInput",xInput);
        anim.SetFloat("yInput",yInput);
    }
}
