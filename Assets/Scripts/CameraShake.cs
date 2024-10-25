using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noiseComponent;
    private float shakeTimer;

    void Awake()
    {
        // Singleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Get the virtual camera component
        virtualCamera = GetComponent<CinemachineCamera>();

        if (virtualCamera != null)
        {
            // Try to get the noise component from the virtual camera
            noiseComponent = virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera not found on the GameObject.");
        }
    }

    public void Shake(float intensity, float duration)
    {
        if (noiseComponent != null)
        {
            noiseComponent.AmplitudeGain = intensity; // Use new property name
            shakeTimer = duration;
        }
        else
        {
            Debug.LogWarning("CinemachineBasicMultiChannelPerlin component not found.");
        }
    }

    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f && noiseComponent != null)
            {
                noiseComponent.AmplitudeGain = 0f; // Reset shake
            }
        }
    }
}
