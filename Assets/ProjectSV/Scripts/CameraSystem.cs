using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : SingletonBase<CameraSystem>
{
    public CinemachineConfiner2D Confiner => cameraConfiner;
    [SerializeField] private CinemachineConfiner2D cameraConfiner;

    public void SetCameraConfiner(Collider2D collider)
    {
        Debug.Log("SetCameraConfiner »£√‚");
        cameraConfiner.m_BoundingShape2D = collider;
        cameraConfiner.InvalidateCache(); 
    }
}
