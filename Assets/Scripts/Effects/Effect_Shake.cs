using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Shake : MonoBehaviour
{
    [SerializeField] private float shake_amount = 0.1f;
    [SerializeField] private float shake_duration_seconds = 0.1f;

    private Vector3 original_pos;
    private float shake_time = 0;
    private float shake_multiplier = 1;
    private bool shake = false;

    public void Shake()
    {
        shake = true;
        shake_time = Time.time;
    }
    public void Shake(float new_shake_multiplier)
    {
        shake_multiplier = new_shake_multiplier;
        shake = true;
        shake_time = Time.time;
    }

    private void Start()
    {
        shake_time = 0;
        shake = false;
        original_pos = this.transform.localPosition;
    }

    private void Update()
    {
        if (shake && (Time.time - shake_time) < shake_duration_seconds)
        {
            float offset_x = Random.Range(-shake_amount * shake_multiplier, shake_amount * shake_multiplier);
            float offset_y = Random.Range(-shake_amount * shake_multiplier, shake_amount * shake_multiplier);
            //float offset_z = Random.Range(-shake_amount, shake_amount);

            this.transform.localPosition = new Vector3(
                original_pos.x + offset_x,
                original_pos.y + offset_y,
                original_pos.z);
        }
        else
        {
            shake = false;
            this.transform.localPosition = original_pos;
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        shake = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        shake = false;
    }
    */
}
