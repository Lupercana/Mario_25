using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Block : MonoBehaviour
{
    [SerializeField] private Collider2D ref_disable_collider = null;

    [SerializeField] private float revert_timer_seconds = 0;

    private Animator ref_animator = null;

    private float hit_time = 0;

    private void Awake()
    {
        ref_animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        float e_time = Time.time - hit_time;
        if (e_time >= revert_timer_seconds)
        {
            ref_disable_collider.enabled = true;
            ref_animator.SetBool("spinning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool activate = false;
        // Activate if player hits from below only
        if (collision.collider.tag == "Player")
        {
            activate = collision.collider.transform.position.y <= this.transform.position.y;
        }

        if (collision.collider.tag == "Fireball" || activate)
        {
            ref_disable_collider.enabled = false;
            ref_animator.SetBool("spinning", true);
            hit_time = Time.time;
        }
    }
}
