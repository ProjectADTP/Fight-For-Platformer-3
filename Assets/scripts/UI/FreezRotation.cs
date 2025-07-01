using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezRotation : MonoBehaviour
{
    private Quaternion _fixedRotation;

    private void Awake()
    {
        _fixedRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = _fixedRotation;
    }
}
