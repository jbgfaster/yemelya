using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GemColor gemColor;
    [SerializeField] private Animator animator;

    void Start()
    {
        animator.Play(gemColor.ToString());
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.TakeGem();
            Destroy(gameObject);
        }
    }

    private enum GemColor
    {
        Yellow,
        Green,
        Blue,
        Pink
    }
}
