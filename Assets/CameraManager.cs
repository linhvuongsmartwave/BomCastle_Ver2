using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // Import DOTween namespace

public class CameraManager : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        // Set camera size to 4 at the start
        cam.orthographicSize = 2;

        // Tween the camera size to 7 over 0.5 seconds
        cam.DOOrthoSize(7, 1f);
    }
}
