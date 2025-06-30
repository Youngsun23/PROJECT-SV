using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : SingletonBase<CameraSystem>
{
    [SerializeField] private CinemachineConfiner2D cameraConfiner;

    public void SetCameraConfiner(Collider2D collider)
    {
        cameraConfiner.m_BoundingShape2D = collider;
    }
}
