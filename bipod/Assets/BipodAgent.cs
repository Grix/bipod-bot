using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BipodAgent : Agent
{
    [Header("Specific to Bipod")]
    public GameObject RightAxis;
	public GameObject RightUpper;
	public GameObject RightLower;
    public GameObject LeftAxis;
	public GameObject LeftUpper;
	public GameObject LeftLower;
	public GameObject IMU;

    Vector3 startPos;

    public override void InitializeAgent()
    {
        startPos = transform.position;
    }

    public override void CollectObservations()
    {
		AddVectorObs(RightAxis.GetComponent<servo_axis>().targetAngle);
		AddVectorObs(RightUpper.GetComponent<servo_upper>().targetAngle);
		AddVectorObs(RightLower.GetComponent<servo_lower>().targetAngle);
		AddVectorObs(LeftAxis.GetComponent<servo_axis>().targetAngle);
		AddVectorObs(LeftUpper.GetComponent<servo_upper>().targetAngle);
		AddVectorObs(LeftLower.GetComponent<servo_lower>().targetAngle);
		AddVectorObs(Mathf.Clamp(IMU.GetComponent<imu>().linAccel.x / 20f, -1f, 1f));        
        AddVectorObs(Mathf.Clamp(IMU.GetComponent<imu>().linAccel.y / 20f, -1f, 1f));
        AddVectorObs(Mathf.Clamp(IMU.GetComponent<imu>().linAccel.z / 20f, -1f, 1f));
		AddVectorObs(Mathf.Clamp(IMU.GetComponent<imu>().angAccel.x / 1000f, -1f, 1f));        
        AddVectorObs(Mathf.Clamp(IMU.GetComponent<imu>().angAccel.y / 1000f, -1f, 1f));
        AddVectorObs(Mathf.Clamp(IMU.GetComponent<imu>().angAccel.z / 1000f, -1f, 1f));

        SetTextObs("Testing " + gameObject.GetInstanceID());
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
		RightAxis.GetComponent<servo_axis>().SetAngle(Mathf.Clamp(vectorAction[0], -1f, 1f));
		RightUpper.GetComponent<servo_upper>().SetAngle(Mathf.Clamp(vectorAction[1], -1f, 1f));
		RightLower.GetComponent<servo_lower>().SetAngle(Mathf.Clamp(vectorAction[2], -1f, 1f));
		LeftAxis.GetComponent<servo_axis>().SetAngle(Mathf.Clamp(vectorAction[3], -1f, 1f));
		LeftUpper.GetComponent<servo_upper>().SetAngle(Mathf.Clamp(vectorAction[4], -1f, 1f));
		LeftLower.GetComponent<servo_lower>().SetAngle(Mathf.Clamp(vectorAction[5], -1f, 1f));

        float reward = 0.1f;
        //reward -= (IMU.GetComponent<imu>().linAccel + Vector3.up*9.81f).magnitude/100; //loss if bipod not upright
        //reward -= IMU.GetComponent<imu>().angAccel.magnitude/2000; //loss if bipod rotating
        //reward += (Mathf.Min(0, IMU.transform.position.y*2)); //loss if hub too low
        // print(reward);

        //print("ang:"+IMU.GetComponent<imu>().angAccel);
        //print("lin:"+IMU.GetComponent<imu>().linAccel);

        if (!IsDone())
            SetReward(Mathf.Clamp(reward, -1f, 1f));

        if (IMU.transform.position.y < -0.18f)
        {
            SetReward(-1);
            Done();
        }

    }

    public override void AgentReset()
    {
        RightAxis.transform.localEulerAngles =  new Vector3(180, 0, 0);
		RightUpper.transform.localEulerAngles =  Vector3.zero;
		RightLower.transform.localEulerAngles =  Vector3.zero;
		LeftAxis.transform.localEulerAngles =  new Vector3(180, 0, 0);
		LeftUpper.transform.localEulerAngles =  Vector3.zero;
        LeftLower.transform.localEulerAngles =  Vector3.zero;
        transform.position = startPos;
        transform.eulerAngles = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        IMU.GetComponent<imu>().Reset();
    }

}
