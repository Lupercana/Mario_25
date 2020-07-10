using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Behavior_UI_Level : MonoBehaviour
{
    private Text ref_text;

    private void Awake()
    {
        ref_text = this.GetComponent<Text>();
    }

    private void Start()
    {
        ref_text.text = SceneManager.GetActiveScene().name;
    }
}
