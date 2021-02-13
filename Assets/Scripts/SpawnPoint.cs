using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector2 GetSpawnLocation()
    {
        var delta = gameObject.GetComponent<RectTransform>().sizeDelta;
        var pos = new Vector2(
            gameObject.transform.position.x + (Random.Range(-delta.x, delta.x)/200),
            gameObject.transform.position.y + (Random.Range(-delta.y, delta.y)/200)
            );

        return pos;
    }
}
