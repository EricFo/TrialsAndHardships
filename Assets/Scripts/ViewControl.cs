using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour
{
    public Camera mianCamera;
    public Camera insideCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (mianCamera.enabled == true)
            {
                mianCamera.enabled = false;
                insideCamera.enabled = true;
            }
            else
            {
                mianCamera.enabled = true;
                insideCamera.enabled = false;
            }
        }
    }
}
