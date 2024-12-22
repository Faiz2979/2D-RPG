using System;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Movement Mechanics")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCD;
    [SerializeField] private float dashTimer;
    private float dashTime;
    private float sideMoveInput;
    [Header("")]
    [Header("Jump Mechanics")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayCastLong;
    [SerializeField] private bool isGrounded;

    [Header("Combat Mechanics")]
    public bool isAttacking;
    private int comboCounter = 1;
    private float attackTimer;
    [SerializeField]private float attackCD = 0.5f;
    [SerializeField]private float comboCD = 0.5f;
    private float comboTimer;

    private bool player_Jump;
    private bool player_Dash;
    private bool player_Down;
    private bool player_Attack;
    private bool isDashing;
    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        comboTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        PlayerInput();
        HandleAnimation();
        Move();
        FlipSprite();
    }

    public void PlayerInput()
    {
        sideMoveInput = playerControls.Movement.SideMove.ReadValue<float>();
        player_Dash = playerControls.Movement.Dash.triggered;
        player_Attack = playerControls.Combat.BasicAttack.triggered;

        if (player_Attack && attackTimer < 0){
            
        if (comboTimer > 0) {
            // Trigger animasi serangan berikutnya dalam combo
            animator.SetTrigger("isAttacking");
        } else {
            // Combo telah selesai, reset ke serangan awal
            comboCounter = 1; 
            animator.SetTrigger("isAttacking");
        }

            comboTimer = comboCD;
            attackTimer = attackCD;

        }

        player_Jump = playerControls.Movement.Jump.triggered;
        player_Down = playerControls.Movement.Down.triggered;
    }

void Move()
{
    CheckGroundStatus();
    rb.linearVelocity = new Vector2(sideMoveInput * moveSpeed, rb.linearVelocity.y);

    if (player_Jump && isGrounded)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    if (player_Down)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -jumpForce / 2);
    }

    Dash(); // Panggil metode Dash
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayCastLong);

    }

    void CheckGroundStatus()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayCastLong, groundLayer);
    }

    void HandleAnimation()
    {
        bool isMoving = sideMoveInput != 0;
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isDashing", isDashing);
        animator.SetInteger("Combo", comboCounter);

    }

    void FlipSprite()
    {
        if (sideMoveInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (sideMoveInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        }
    
    
    
void Dash(){
    if (isDashing  && dashTimer > 0){
        dashTimer -= Time.deltaTime;
        rb.linearVelocity = new Vector2(sideMoveInput * dashSpeed, rb.linearVelocity.y);
        if (dashTimer <= 0){
            isDashing = false;
            dashTimer = dashCD;
        }
        }
        else if (dashTimer > 0){
            dashTimer -= Time.deltaTime;
        }
        else if (player_Dash){
            isDashing = true;
            dashTimer = dashCD;
        }
    }




public void AttackOver(){
    comboCounter++;
    if (comboCounter > 3){
        comboCounter = 1;
    }

}

    private void OnDisable()
    {
        playerControls.Disable();
}
}
