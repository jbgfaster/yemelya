using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] string gemColor="Yellow";

    void Start()
    {
        GetComponent<Animator>().Play(gemColor);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            GameManager.instanse.TakeGem();
            Destroy(gameObject);
        }
    }
}
