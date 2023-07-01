using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour
{
    //四个轮子的车轮碰撞器
    public WheelCollider wheelFLCollider;
    public WheelCollider wheelFRCollider;
    public WheelCollider wheelRLCollider;
    public WheelCollider wheelRRCollider;

    //四个轮子的模型
    public Transform wheelFLModel;
    public Transform wheelFRModel;
    public Transform wheelRLModel;
    public Transform wheelRRModel;

    //两个可刹车轮子模型
    public Transform discBrakeFL;
    public Transform discBrakeFR;

    public float motorTorque = 1000;
    public float steerAngle = 20;
    public Transform centerOfMass;

    public float maxSpeed = 140;
    public float minSpeed = 15;
    public float currentSpeed;

    public float brakeTorque = 1000;//刹车力量
    private bool isBrakeing = false;

    public AudioSource carEngineSound;
    public AudioSource skidAudio;
    public AudioSource crashSound;

    public Light leftLight;
    public Light rightLight;

    public GameObject skidMark;
    public GameObject fetalSmoke;
    private Vector3 lastRLSkidPos = Vector3.zero;
    private Vector3 lastRRSkidPos = Vector3.zero;

    public int[] speedArray;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        RotateWheel();
        SteerWheel();
        EngineSound();
        ControlLight();
        //没有输入时引擎不发声
        if (Input.GetAxis("Vertical") == 0)
        {
            carEngineSound.volume = 0.15f;
        }
        else
        {
            carEngineSound.volume = 0.3f;
        }
    }
    private void FixedUpdate()
    {
        Skid();
        //限制速度
        currentSpeed = wheelFLCollider.rpm * (wheelFLCollider.radius * 2 * Mathf.PI) * 60 / 1000;

        if ((currentSpeed > 0 && Input.GetAxis("Vertical") < 0) || (currentSpeed < 0 && Input.GetAxis("Vertical") > 0)||Input.GetKey(KeyCode.Space))
        {
            isBrakeing = true;
            //Debug.LogWarning("1111");
        }
        else
        {
            isBrakeing = false;
        }

        //限制速度，获取加速
        if ((currentSpeed > maxSpeed && Input.GetAxis("Vertical") > 0) || (currentSpeed < -minSpeed && Input.GetAxis("Vertical") < 0))
        {
            wheelFLCollider.motorTorque = 0;
            wheelFRCollider.motorTorque = 0;
            wheelRLCollider.motorTorque = 0;
            wheelRRCollider.motorTorque = 0;
        }
        else
        {
            wheelFLCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelFRCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelRLCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelRRCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        }

        if (isBrakeing)//刹车
        {
            //wheelFLCollider.motorTorque = 0;
            //wheelFRCollider.motorTorque = 0;
            //wheelRLCollider.motorTorque = 0;
            //wheelRRCollider.motorTorque = 0;

            wheelFLCollider.brakeTorque = brakeTorque;
            wheelFRCollider.brakeTorque = brakeTorque;
            wheelRLCollider.brakeTorque = brakeTorque;
            wheelRRCollider.brakeTorque = brakeTorque;
        }
        else
        {
            wheelFLCollider.brakeTorque = 0;
            wheelFRCollider.brakeTorque = 0;
            wheelRLCollider.brakeTorque = 0;
            wheelRRCollider.brakeTorque = 0;
        }

        wheelFLCollider.steerAngle = Input.GetAxis("Horizontal") * steerAngle*2;
        wheelFRCollider.steerAngle = Input.GetAxis("Horizontal") * steerAngle*2;
    }

    void RotateWheel()//控制车轮转动
    {
        discBrakeFL.Rotate(wheelFLCollider.rpm * 6 * Time.deltaTime * Vector3.right);
        discBrakeFR.Rotate(wheelFRCollider.rpm * 6 * Time.deltaTime * Vector3.right);
        wheelRLModel.Rotate(wheelRLCollider.rpm * 6 * Time.deltaTime * Vector3.right);
        wheelRRModel.Rotate(wheelRRCollider.rpm * 6 * Time.deltaTime * Vector3.right);
    }

    void SteerWheel()//控制轮子转向
    {
        Vector3 localEulerAngles = wheelFLModel.localEulerAngles;
        localEulerAngles.y = wheelFLCollider.steerAngle;

        wheelFLModel.localEulerAngles = localEulerAngles;
        wheelFRModel.localEulerAngles = localEulerAngles;
    }

    void EngineSound()//根据车子速度调整引擎声音播放速度
    {
        int index = 0;
        for (int i = 0; i < speedArray.Length - 1; i++)
        {
            if (currentSpeed >= speedArray[i])
            {
                index = i;
            }
        }
        int minSpeed = speedArray[index];
        int maxSpeed = speedArray[index + 1];
        carEngineSound.pitch = 0.5f + (currentSpeed - minSpeed) / (maxSpeed - minSpeed) * 0.7f;//模拟档位
    }

    void Skid()//漂移
    {
        if (currentSpeed > 40 && Mathf.Abs(wheelFLCollider.steerAngle) > 5)
        {
            //判断轮胎是否着地
            bool isGround = false;
            WheelHit hit;
            if (wheelFLCollider.GetGroundHit(out hit) || wheelFRCollider.GetGroundHit(out hit) || wheelRLCollider.GetGroundHit(out hit) || wheelRRCollider.GetGroundHit(out hit))
            {
                isGround = true;
            }
            else
            {
                isGround = false;
            }
            if (wheelRLCollider.GetGroundHit(out hit))
            {
                if (lastRLSkidPos.x != 0 && lastRLSkidPos.y != 0 && lastRLSkidPos.z != 0)
                {
                    Vector3 pos = hit.point;
                    pos.y += 0.05f;
                    pos += this.transform.forward;
                    Quaternion rotation = Quaternion.LookRotation(hit.point - lastRLSkidPos);
                    GameObject.Instantiate(skidMark, pos, rotation);
                    GameObject.Instantiate(fetalSmoke, pos, rotation);
                }
                lastRLSkidPos = hit.point;
            }
            else
            {
                lastRLSkidPos = Vector3.zero;
            }
            if (wheelRRCollider.GetGroundHit(out hit))
            {
                if (lastRRSkidPos.x != 0 && lastRRSkidPos.y != 0 && lastRRSkidPos.z != 0)
                {
                    Vector3 pos = hit.point;
                    pos.y += 0.05f;
                    pos += this.transform.forward;
                    Quaternion rotation = Quaternion.LookRotation(hit.point - lastRRSkidPos);
                    GameObject.Instantiate(skidMark, pos, rotation);
                    GameObject.Instantiate(fetalSmoke, pos, rotation);
                }
                lastRRSkidPos = hit.point;
            }
            else
            {
                lastRRSkidPos = Vector3.zero;
            }
            if (skidAudio.isPlaying == false && isGround)
            {
                skidAudio.Play();
            }
            else if (skidAudio.isPlaying && !isGround)
            {
                skidAudio.Stop();
            }
        }
        else
        {
            if (skidAudio.isPlaying)
            {
                skidAudio.Stop();
            }
        }
    }

    void ControlLight()//控制倒车灯
    {
        if (Input.GetAxis("Vertical") < 0||Input.GetKey(KeyCode.Space))
        {
            leftLight.enabled = true;
            rightLight.enabled = true;
        }
        else
        {
            leftLight.enabled = false;
            rightLight.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            crashSound.Play();
        }
    }
}
