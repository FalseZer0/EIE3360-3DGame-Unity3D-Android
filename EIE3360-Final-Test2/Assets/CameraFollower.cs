using System;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    private void Awake()
    {
        transform.position = Target.position;
    }
    private void Update()
    {
        //simply follows the target
        transform.position = Target.position;
    }
}
