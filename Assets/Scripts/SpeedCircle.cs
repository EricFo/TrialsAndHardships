using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedCircle : MonoBehaviour
{
    public RectTransform speed;
    public Image speed1;
    public Image speed2;
    public Image speed3;

    public WheelCollider wheelFLCollider;
    public float currentSpeed;

    public Text speedText;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpeedUpdate", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        SpeedCircleDisplay();
    }

    public void SpeedUpdate()//刷新速度值
    {
        speedText.text = Mathf.Abs(Mathf.Round(currentSpeed)).ToString();
    }

    public void SpeedCircleDisplay()//转动速度条填充
    {
        if (speed.eulerAngles.z < 200f)
        {
            speed2.enabled = true;
        }
        else
        {
            speed2.enabled = false;
        }
        if (speed.eulerAngles.z < 100f)
        {
            speed1.enabled = true;
        }
        else
        {
            speed1.enabled = false;
        }
        //Debug.Log(speed.eulerAngles.z);
        currentSpeed = wheelFLCollider.rpm * (wheelFLCollider.radius * 2 * Mathf.PI) * 60 / 1000;
        if (currentSpeed >= -20 && currentSpeed <= 145)
        {
            speed.eulerAngles = new Vector3(0, 0, 234f - Mathf.Abs(currentSpeed) * 234 / 140);
        }
    }
}
