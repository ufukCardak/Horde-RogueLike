using UnityEngine;

public class DamageTextScript : MonoBehaviour
{
    float randomY;
    private void Start()
    {
        Destroy(transform.parent.gameObject,0.75f);
        randomY = Random.Range(0.001f, 0.009f);
    }
    void Update()
    {
        transform.position += new Vector3(0, randomY);
    }
}
