using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Effect_Cinemachine_Shake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera ref_vcam = null;

    [SerializeField] private float shake_amount = 0f;
    [SerializeField] private float shake_frequency = 0f;
    [SerializeField] private float shake_duration_seconds = 0f;

    private CinemachineBasicMultiChannelPerlin ref_vcam_noise;

    private float shake_time;

    private void Awake()
    {
        ref_vcam_noise = ref_vcam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float multiplier)
    {
        shake_time = Time.time;

        ref_vcam_noise.m_AmplitudeGain = shake_amount * multiplier;
        ref_vcam_noise.m_FrequencyGain = shake_frequency;
    }

    private void Update()
    {
        float e_time = Time.time - shake_time;

        if (e_time >= shake_duration_seconds)
        {
            ref_vcam_noise.m_AmplitudeGain = 0f;
            ref_vcam_noise.m_FrequencyGain = 0f;
        }
    }
}
