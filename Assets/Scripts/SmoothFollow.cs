using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;

    public float height = 2f;
    public float distance = -5f;
    public float smoothSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()//Ïà»úÆ½»¬¸úËæ
    {
        Vector3 targetForward = target.forward;
        targetForward.y = 0;
        Vector3 currentForward = transform.forward;
        currentForward.y = 0;

        Vector3 forward = Vector3.Lerp(currentForward.normalized, targetForward.normalized, smoothSpeed * Time.deltaTime);

        this.transform.position = target.position + Vector3.up * height + forward * distance;
        transform.LookAt(target.position + Vector3.up * 2f);
    }
}
