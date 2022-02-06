using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController main;
    public float CamMoveTime = 1f;
    private void Awake()
    {
        main = this;
    }
    public void MoveToPosition(Transform position)
    {
        MoveToPosition(transform.position, transform.rotation);
    }
    public void MoveToPosition(Vector3 Position, Quaternion Rotation)
    {

        LeanTween.move(Camera.main.gameObject, Position, CamMoveTime);
        LeanTween.rotate(Camera.main.gameObject, Rotation.eulerAngles, CamMoveTime);
    }
}
