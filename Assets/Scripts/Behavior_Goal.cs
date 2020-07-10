using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behavior_Goal : MonoBehaviour
{
    [SerializeField] private Object ref_next_level = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (ref_next_level == null)
            {
                int next_build_index = 0;

                next_build_index = SceneManager.GetActiveScene().buildIndex + 1;

                // Check for valid 
                if (next_build_index < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(next_build_index);
                }
            }
            else
            {
                SceneManager.LoadScene(ref_next_level.name);
            }   
        }
    }
}
