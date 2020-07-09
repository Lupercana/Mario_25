using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behavior_Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Move to next scene if it exists
            int next_build_index = SceneManager.GetActiveScene().buildIndex + 1;
            if (next_build_index < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(next_build_index);
            }
        }
    }
}
