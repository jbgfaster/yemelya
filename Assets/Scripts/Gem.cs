using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GemColor gemColor;

    void Start()
    {
        GetComponent<Animator>().Play(gemColor.ToString());
    }

    private void OnTriggerEnter2D(Collider2D other) {
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
