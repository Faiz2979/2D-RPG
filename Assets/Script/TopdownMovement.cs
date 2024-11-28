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
    public bool isMoving;
    public bool isRunning;
    public bool isDashing;
    public bool isAttacking;

    [Header("Player Facing")]
    private int facingDir = 1;
    private bool facingRight = true;

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleDash();
        flipPlayer();
    }

    void handleMovement(){
        xInput=Input.GetAxis("Horizontal");
        yInput=Input.GetAxis("Vertical");
        Vector2 moveDir=new Vector2(xInput,yInput);
        if(Input.GetKey(KeyCode.LeftShift)){
            rb.velocity=moveDir.normalized*runSpeed;
        }else if(Input.GetKey(KeyCode.LeftControl)){
            rb.velocity=moveDir.normalized*dash;
        }else{
            rb.velocity=moveDir.normalized*moveSpeed;
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

    }
}
