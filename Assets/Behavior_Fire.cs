using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behavior_Fire : MonoBehaviour
{
    [SerializeField] private Collider2D ref_collider_disable = null;

    private Rigidbody2D ref_rbody = null;

    private void Awake()
    {
        ref_rbody = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.collider.tag == "Fireball")
        {
            Physics2D.IgnoreCollision(collision.collider, ref_collider_disable, true);
            ref_rbody.velocity = new Vector2(0, 0);
            Destroy(collision.gameObject);
        }
    }
}
