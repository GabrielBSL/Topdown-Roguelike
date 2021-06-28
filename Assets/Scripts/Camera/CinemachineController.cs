using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    public static CinemachineController Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVC;
    private CinemachineBasicMultiChannelPerlin cinemachineBMCP;

    private float shakeTime = 0;
    private float shakeTimer = 0;
    private float shakeIntensity = 0;

    private void Awake()
    {
        Instance = this;
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
        cinemachineBMCP = cinemachineVC.GetCinemachineComponent
            <CinemachineBasicMultiChannelPerlin>();
    }

    public void StartShake(float intensity, float time)
    {
        cinemachineBMCP.m_AmplitudeGain = intensity;

        shakeIntensity = intensity;
        shakeTime = time;
        shakeTimer = time;
    }

    public void FollowObject(Transform obj)
    {
        cinemachineVC.Follow = obj;
    }

    void Update()
    {
        if (shakeTimer <= 0) return;

        shakeTimer -= Time.deltaTime;
        cinemachineBMCP.m_AmplitudeGain = Mathf.Lerp
            (shakeIntensity, 0f, 1 - (shakeTimer / shakeTime));
    }
}
