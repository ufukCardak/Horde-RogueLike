using System.Collections;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField] int exp;
    Transform player;
    bool toPlayer;
    float timer;
    [SerializeField] BoxCollider2D boxCollider;
    public void SetExp(int exp,int expIndex)
    {
        this.exp = exp;
        transform.GetChild(expIndex).gameObject.SetActive(true);
    }
    public int GetExp()
    {
        return exp;
    }
    
    public void SetColliderSize(Vector2 size)
    {
        boxCollider.size = size;
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
        toPlayer = true;
        Destroy(gameObject,5);
    }
    public void SetCollectionArea(float area)
    {
        boxCollider.size += new Vector2(boxCollider.size.x * area * 8, boxCollider.size.y * area * 3);
    }

    private void FixedUpdate()
    {
        if (toPlayer)
        {
            timer += Time.deltaTime/10;
            transform.position = Vector3.Lerp(transform.position,new Vector2(player.position.x,player.position.y), timer);
        }
    }
    IEnumerator GoToPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        toPlayer = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.transform;
            StartCoroutine(GoToPlayer());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (toPlayer && collision.tag == "Player")
        {
            Destroy(gameObject,0.5f);
        }
    }
    private void OnDestroy()
    {
        if (player != null)
            player.GetComponent<PlayerExp>().AddExp(exp);
    }
}
