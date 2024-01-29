using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 
 
public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera fpsCamera;
    [SerializeField] float normalFOV = 40f;
    [SerializeField] float zoomedFOV = 20f;
 
    void Start()
    {
        fpsCamera = GetComponent<CinemachineVirtualCamera>();
    }
 
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ZoomView();
        }
        else
        {
            fpsCamera.m_Lens.FieldOfView = normalFOV;
        }
    }
 
    void ZoomView()
    {
        fpsCamera.m_Lens.FieldOfView = zoomedFOV;
    }
}