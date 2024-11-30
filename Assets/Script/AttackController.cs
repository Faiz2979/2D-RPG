
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float lineLong;
    public Animator animator;
    public Vector3 MousePosition;
    TopdownMovement topdownMovement;
    // Start is called before the first frame update
    void Start()
    {
        topdownMovement = GetComponent<TopdownMovement>();
        animator =topdownMovement.anim;
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Attack();
    }
    
    void Attack(){
        Physics2D.Raycast(transform.position, MousePosition, lineLong);
        MousePosition.z = 0f;
        Vector3 directionToMouse = (MousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        if(Input.GetMouseButtonDown(0)){
            if (angle > -45 && angle <= 45)
            {
                animator.SetTrigger("AttackSide");
                if(!topdownMovement.facingRight){
                    transform.Rotate(0, 180, 0);
                    topdownMovement.facingRight = true;
                }
            }
            else if (angle > 45 && angle <= 135)
            {
                animator.SetTrigger("AttackUp");
            }
            else if (angle > -135 && angle <= -45)
            {
                animator.SetTrigger("AttackDown");
            }
            else
            {
                animator.SetTrigger("AttackSide");
                if (topdownMovement.facingRight)
                {
                    transform.Rotate(0, 180, 0);
                    topdownMovement.facingRight = false;
                }
                else
                {
                    transform.Rotate(0, 180, 0);
                    topdownMovement.facingRight = true;
                }
            }
        }
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, directionToMouse, lineLong);
        if (hitInfo.collider != null)
        {
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(1);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3[] directions = {
            Quaternion.Euler(0, 0, 45) * Vector3.up,
            Quaternion.Euler(0, 0, 135) * Vector3.up,
            Quaternion.Euler(0, 0, 225) * Vector3.up,
            Quaternion.Euler(0, 0, 315) * Vector3.up
        };

        foreach (var direction in directions)
        {
            Gizmos.DrawLine(transform.position, transform.position + direction * lineLong);
        }
        Gizmos.DrawLine(transform.position, MousePosition);
    }
}

