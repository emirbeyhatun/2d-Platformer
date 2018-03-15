using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotor : MonoBehaviour
{
    [SerializeField]
    private  WheelJoint2D backWheel;
    [SerializeField]
    private WheelJoint2D frontWheel;
    [SerializeField]
    float speed;
    float movement = 0;
    private bool stop=true;
    [SerializeField]
    private GameObject[] destroyToStartEngine;

    [SerializeField]
    private bool useChains;
   
    private void Update()
    {
        if (useChains)
        {
            foreach (GameObject item in destroyToStartEngine)
            {
                if (item.activeSelf == false)
                {
                    for (int i = 0; i < destroyToStartEngine.Length; i++)
                        destroyToStartEngine[i].GetComponent<Rigidbody2D>().mass = 0f;


                    stop = false;
                }
            } 
        }

        if (stop == true)
        {
            speed = 0.5f;
            movement += -speed;
        }
        if (stop == false)
        {
            speed = 50;
            movement += -speed;
        }
            
        
      

    }
    private void FixedUpdate()
    {
      
            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = 10000 };
        backWheel.motor = motor;
        frontWheel.motor = motor;
        
    }
}
