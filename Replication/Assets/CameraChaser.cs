using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaser : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    Vector3 offset, targetPrevPos, targetMoveDir;
    float distance;
    private void Start()
    {
        offset = transform.position - target.transform.position;
        distance = offset.magnitude;
        targetPrevPos = target.transform.position;
    }
    void LateUpdate()
    {
        targetMoveDir = target.transform.position - targetPrevPos;
        if (targetMoveDir != Vector3.zero)
        {
            transform.position = target.transform.position - targetMoveDir.normalized * distance;
            transform.LookAt(target.transform);
            targetPrevPos = target.transform.position;
        }
    }
    public void UpdateTarget(GameObject virus)
    {
        virus.GetComponent<CellCollision>().pMovement.enabled = true;
        target = virus;
        targetPrevPos = target.transform.position;
    }
}