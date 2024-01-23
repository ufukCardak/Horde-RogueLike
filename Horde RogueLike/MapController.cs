using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    Vector2 moveDir;
    Vector3 playerLastPosition;

    [Header("Optimization")]
    [SerializeField] List<GameObject> spawnedChunks;
    GameObject latestChunk;
    [SerializeField] float maxOpDist;
    float opdist;

    float optimizerCooldown = 1;

    [SerializeField] int gameMap;

    // Start is called before the first frame update

    private void Awake()
    {
        Instantiate(terrainChunks[Random.Range(0,terrainChunks.Count)]);
    }
    void Start()
    {
        playerLastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChunckChecker();
        ChunkOptimizer();
    }

    string GetDirecitonName(Vector3 direction)
    {
        direction = direction.normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) 
        {
            if (direction.y > 0.5f)
            {
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if (direction.y < -0.5f)
            {
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void CheckAndSpawnChunk(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position,checkerRadius,terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    void ChunckChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirecitonName(moveDir);

        CheckAndSpawnChunk (directionName);

        if (directionName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
        }
    }
    void SpawnChunk(Vector3 spawnPosition)
    {
        int random = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[random], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = 1;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opdist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opdist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
