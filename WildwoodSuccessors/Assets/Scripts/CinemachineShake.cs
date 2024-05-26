using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    float durationShake;
    // public float shakePower;
    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin =
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        durationShake = time;
        StartCoroutine(CountShake());
    }
    public void StartShakeCamera(float intensity)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }
    public void StopShakeCamera()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
    IEnumerator CountShake()
    {
        yield return new WaitForSeconds(durationShake);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }




}
