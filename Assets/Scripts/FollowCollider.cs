using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCollider : MonoBehaviour
{
    public WheelCollider collider;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(collider.transform.position, Vector3.down, out hit, collider.radius + collider.suspensionDistance))
        {
            transform.position = hit.point + Vector3.up * collider.radius;
        }
        else
        {
            transform.position = collider.transform.position - collider.transform.up * collider.suspensionDistance;
        }
    }
}
