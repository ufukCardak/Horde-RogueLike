using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

    [SerializeField] List<Vector3> spawnPositions;
    [SerializeField] LayerMask propMask;

    Vector3 _propPosition;
    // Start is called before the first frame update
    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int propChance = Random.Range(0, 101);

            if (propChance >= 25 )
            {
                int random = Random.Range(0, propPrefabs.Count);

                GameObject prop = Instantiate(propPrefabs[random], PropPosition(), Quaternion.identity);
                prop.transform.parent = sp.transform;
            }
        }
    }

    Vector3 PropPosition()
    {
        _propPosition = new Vector3(transform.position.x + Random.Range(-8f, 8f), transform.position.y + Random.Range(-8f, 8f), 0);

        if (!Physics2D.OverlapBox(new Vector2(_propPosition.x,_propPosition.y), Vector2.one * 3, 0f, propMask)) 
            return _propPosition; 
        else
            return PropPosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_propPosition, Vector3.one * 3);
    }
}
