using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public Player player;

    private CinemachineVirtualCamera mCam;
    public float fireShakeIntensity = 1f;
    public float boomShakeIntensity = 3f;
    public float shakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    private void Awake()
    {
        mCam = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        StopShake(); 
    }
    private void FireShake()
    {
        _cbmcp=mCam.GetCinemachineComponent <CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = fireShakeIntensity;

        timer = shakeTime;
    }

    public void BoomShake()
    {
        _cbmcp = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = boomShakeIntensity;

        timer = shakeTime;
    }

    private void StopShake()
    {
        _cbmcp = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;

        timer = 0f;
    }

    private void Update()
    {
        if(player.fireShake)
        {
            FireShake();
            player.fireShake = false;
        }

        if(timer > 0f)
        {
            timer -= Time.deltaTime;

            if(timer <= 0f)
            {
                StopShake();
            }
        }
    }
}
