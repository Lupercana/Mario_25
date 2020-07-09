using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Mario : MonoBehaviour
{
    // References
    //[SerializeField] private LayerMask mask_ground = 0;
    [SerializeField] private GameObject ref_shot_object = null;
    [SerializeField] private Transform ref_shot_spawn = null;
    [SerializeField] private Transform ref_ground_check = null;
    [SerializeField] private Rigidbody2D ref_rbody = null;
    [SerializeField] private Animator ref_animator = null;

    // Scripts
    [SerializeField] private Effect_Cinemachine_Shake ref_script_shake = null;

    // Parameters
    [SerializeField] private float initial_speed = 0;
    [SerializeField] private float jump_velocity = 0;
    [SerializeField] private float jump_root_scaling = 1;
    [SerializeField] private float jump_reset_lock = 0;
    [SerializeField] private float shot_velocity = 0;
    [SerializeField] private float shot_cd_seconds = 0;

    /*******************************/

    // Inputs
    private float input_horizontal = 0;
    private float input_vertical = 0;

    private float ground_check_rad = 0;
    private float last_shot_time = 0;
    private int jump_frame_counter = 0;
    private bool vertical_lock = false;
    private bool facing_right = true;
    private bool jumping = false;

    private void FlipSprite()
    {
        transform.Rotate(0f, 180f, 0f);
        facing_right = !facing_right;
    }

    private void Start()
    {
        CircleCollider2D ref_gcheck_collider = ref_ground_check.GetComponent<CircleCollider2D>();
        ground_check_rad = ref_gcheck_collider.radius;
    }

    private void Update()
    {
        // Variable updates
        ++jump_frame_counter;

        // Check inputs
        input_horizontal = Input.GetAxis("Horizontal");
        float input_vertical_raw = Input.GetAxisRaw("Vertical");
        input_vertical = vertical_lock ? 0 : input_vertical_raw;
        if (vertical_lock && input_vertical_raw == 0)
        {
            vertical_lock = false;
        }
        else if (!vertical_lock && input_vertical_raw != 0)
        {
            vertical_lock = true;
        }

        if ((input_horizontal < 0 && facing_right) || (input_horizontal > 0 && !facing_right))
        {
            FlipSprite();
        }

        // Check ground collision
        int mask_ground_unshifted = (this.gameObject.layer == LayerMask.NameToLayer("Background")) ? LayerMask.NameToLayer("Background_Platform") : LayerMask.NameToLayer("Foreground_Platform");
        if (jumping && jump_frame_counter >= jump_reset_lock && Physics2D.OverlapCircle((Vector2)ref_ground_check.position, ground_check_rad, 1 << mask_ground_unshifted))
        {
            ref_script_shake.Shake(transform.localScale.z); // Scale shake amount with size of Mario
            ref_animator.SetBool("jumping", false);
            jumping = false;
        }

        // Jumping, can't handle jumping in FixedUpdate, need to catch all frames due to acting on key down
        if (!jumping && input_vertical > 0)
        {
            ref_rbody.AddForce(new Vector2(0, jump_velocity * Mathf.Pow(transform.localScale.z, jump_root_scaling)), ForceMode2D.Force);
            ref_animator.SetBool("jumping", true);
            jumping = true;
            jump_frame_counter = 0;
        }

        // Shoot fireball
        float e_time = Time.time - last_shot_time;
        if (Input.GetButton("Shoot") && e_time >= shot_cd_seconds)
        {
            ref_animator.Play("mario_shoot");
            var inst = Instantiate(ref_shot_object, ref_shot_spawn.position, Quaternion.identity);
            inst.layer = gameObject.layer;
            inst.transform.localScale *= this.transform.localScale.z;
            inst.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shot_velocity * (facing_right ? 1 : -1));
            last_shot_time = Time.time;
        }
    }

    private void FixedUpdate()
    {
        // Horizontal
        float move_hori = initial_speed * Time.fixedDeltaTime * input_horizontal;

        ref_rbody.velocity = new Vector2(move_hori, ref_rbody.velocity.y);
        ref_animator.SetFloat("speed", Mathf.Abs(move_hori));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Tube")
        {
            if (Input.GetAxis("Vertical") < 0 && transform.localScale.z <= 1) // Down is pressed
            {
                Behavior_Tube script_bt = collision.collider.GetComponent<Behavior_Tube>();
                if (script_bt)
                {
                    script_bt.DisableCollider();
                    ref_animator.SetBool("spinning", true);
                }
            }
        }
    }
}