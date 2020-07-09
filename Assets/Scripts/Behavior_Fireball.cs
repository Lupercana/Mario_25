using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Fireball : MonoBehaviour
{
    [SerializeField] private float rotate_speed = 0;
    [SerializeField] private float kill_vel_threshold = 0;

    private Rigidbody2D ref_rbody;

    private bool vel_check = false;

    private void Awake()
    {
        ref_rbody = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (ref_rbody.velocity.magnitude > kill_vel_threshold)
        {
            vel_check = true;
        }

        if (vel_check && ref_rbody.velocity.magnitude <= kill_vel_threshold)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.back * rotate_speed * Time.fixedDeltaTime);
    }
}
