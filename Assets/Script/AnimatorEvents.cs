using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController=GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackOverTrigger()
    {
        playerController.AttackOver();
    }
    private void AttackStartTrigger()
    {
        playerController.AttackStart();
    }
}
