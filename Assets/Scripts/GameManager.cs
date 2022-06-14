using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instanse;

    [SerializeField] Transform startPosition;
    [SerializeField] GameObject[] gems; 
    [SerializeField] Transform[] gemsPositions;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject help;

    private int score;
    private int gemsCount;    

    private void Awake() 
    {
        instanse=this;
        StartCoroutine(HelpRoutine());
    }

    private void Start() 
    {
        score=0;
        scoreText.text="Score: "+score;
        SpawnGems();    
    }

    private IEnumerator HelpRoutine()
    {
        help.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        help.SetActive(false);
    }

    private void SpawnGems()
    {
        gemsCount=0;
        foreach(Transform i in gemsPositions)
        {
            if(i.GetComponentInChildren<Gem>()!=null)
                Destroy(i.GetComponentInChildren<Gem>().gameObject);
            if(Random.Range(0,2)==0)
            {
                Instantiate(gems[Random.Range(0,gems.Length)],i.position,i.rotation,i);
                gemsCount++;
            }
        }
    }

    public void TakeGem()
    {
        gemsCount--;
        if(gemsCount<=0)
        {
            NewRound();
        }
    }

    public void NewRound()
    {
        score++;
        scoreText.text="Score: "+score;
        SpawnGems();
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {            
            other.transform.position=startPosition.position;
            Start();            
        }
    }
}
