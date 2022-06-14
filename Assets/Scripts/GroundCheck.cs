using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private int checkCoolDown=10;

    private int timer=0;

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(timer<0)
            isGrounded=other!=null&&(((1<<other.gameObject.layer)&platformLayerMask)!=0); 
        timer--;   
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        isGrounded=false;
        timer=checkCoolDown;
    }
}
