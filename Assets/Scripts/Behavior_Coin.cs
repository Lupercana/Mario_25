using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Coin : MonoBehaviour
{
    [SerializeField] private float min_scale = 1f;
    [SerializeField] private float max_scale = 1f;
    [SerializeField] private float multiplier = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.gameObject.transform.localScale *= multiplier;

        float z = collision.collider.gameObject.transform.localScale.z;
        Debug.Log(z);
        if (z > max_scale)
        {
            collision.collider.gameObject.transform.localScale *= (max_scale / z);
        }
        if (z < min_scale)
        {
            collision.collider.gameObject.transform.localScale *= (z / min_scale);
        }

        Destroy(this.gameObject);
    }
}
