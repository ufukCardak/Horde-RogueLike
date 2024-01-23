using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mc;
    GameObject targetMap;

    private void Awake()
    {
        targetMap = transform.parent.gameObject;
        mc = FindObjectOfType<MapController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mc.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (mc.currentChunk == targetMap)
            {
                mc.currentChunk = null;
            }
        }
    }
}
