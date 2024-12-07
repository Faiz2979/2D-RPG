using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    [Header("Movement Mechanics")]
    [SerializeField] private float moveSpeed = 10f;
    private float sideMoveInput;
    [Header("")]
    [Header("Jump Mechanics")]
    public Transform groundCheck;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayCastLong;
    [SerializeField] private bool isGrounded;

    private bool player_Jump;
    private bool player_Run;
    private bool player_Down;
    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Update() {
        PlayerInput();
        Move();
    }

    public void PlayerInput() {
        sideMoveInput = playerControls.Movement.SideMove.ReadValue<float>();
        player_Run = playerControls.Movement.Run.triggered;
        player_Jump = playerControls.Movement.Jump.triggered;
        player_Down = playerControls.Movement.Down.triggered;
        if (player_Run) {
            moveSpeed = 14f;
            Debug.Log(moveSpeed);
        } else {
            moveSpeed = 10f;
        }
    }

    void Move(){
        CheckGroundStatus();
        rb.velocity = new Vector2(sideMoveInput * moveSpeed, rb.velocity.y);
        if (player_Jump && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (player_Down) {
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce/2);
        }
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayCastLong);
    }

    void CheckGroundStatus() {
        isGrounded= Physics2D.Raycast(transform.position, Vector2.down, rayCastLong, groundLayer);
    }
}
