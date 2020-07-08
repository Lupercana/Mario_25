using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Tube : MonoBehaviour
{
    [SerializeField] private GameObject ref_next_tube = null;
    [SerializeField] private Collider2D ref_center_collider = null;
    [SerializeField] private Transform ref_warp_location = null;
    
    public void DisableCollider()
    {
        ref_center_collider.enabled = false;
    }

    public Vector3 GetWarpLocation()
    {
        return ref_warp_location.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Player hit end of tube, warp to next location
            collision.gameObject.transform.position = ref_next_tube.GetComponent<Behavior_Tube>().GetWarpLocation();
            collision.gameObject.layer = ref_next_tube.layer; // Set the layer for updated collisions
            if (ref_next_tube.layer == LayerMask.NameToLayer("Background_Platform")) // Set to background mode
            {
                collision.gameObject.layer = LayerMask.NameToLayer("Background");
                collision.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Background");
            }
            else // Set to foreground mode
            {
                collision.gameObject.layer = LayerMask.NameToLayer("Foreground");
                collision.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Default");
            }

            // Re-enable top collider
            ref_center_collider.enabled = true;
            collision.gameObject.GetComponent<Animator>().SetBool("spinning", false);
        }
    }
}
