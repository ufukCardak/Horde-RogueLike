using UnityEngine;

public class CastleChunkTrigger : MonoBehaviour
{
    CastleController cc;
    GameObject targetMap;

    private void Awake()
    {
        targetMap = transform.parent.gameObject;

        cc = FindObjectOfType<CastleController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cc.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cc.currentChunk == targetMap)
            {
                cc.currentChunk = null;
            }
        }
    }
}
