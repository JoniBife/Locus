using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;

    private float _currentRotation;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, _currentRotation +=_rotationSpeed * Time.deltaTime, 0f);
    }
}
