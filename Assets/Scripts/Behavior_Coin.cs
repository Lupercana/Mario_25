using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Coin : MonoBehaviour
{
    [SerializeField] private float max_scale = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.gameObject.transform.localScale *= 2;

        float z = collision.collider.gameObject.transform.localScale.z;
        if (z > max_scale)
        {
            collision.collider.gameObject.transform.localScale *= (max_scale / z);
        }

        Destroy(this.gameObject);
    }
}
