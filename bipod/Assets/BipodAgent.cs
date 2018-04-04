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

    public override void InitializeAgent()
    {

    }

    public override void CollectObservations()
    {
		AddVectorObs(RightAxis.GetComponent<servo_axis>().targetAngle);
		AddVectorObs(RightUpper.GetComponent<servo_upper>().targetAngle);
		AddVectorObs(RightLower.GetComponent<servo_upper>().targetAngle);
		AddVectorObs(LeftAxis.GetComponent<servo_axis>().targetAngle);
		AddVectorObs(LeftUpper.GetComponent<servo_upper>().targetAngle);
		AddVectorObs(LeftLower.GetComponent<servo_upper>().targetAngle);
		AddVectorObs(IMU.GetComponent<imu>().linAccel);
		AddVectorObs(IMU.GetComponent<imu>().angAccel);

        // AddVectorObs(gameObject.transform.rotation.z);
        // AddVectorObs(gameObject.transform.rotation.x);
        // AddVectorObs((ball.transform.position - gameObject.transform.position));
        // AddVectorObs(ball.transform.GetComponent<Rigidbody>().velocity);
        // SetTextObs("Testing " + gameObject.GetInstanceID());
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
		RightAxis.GetComponent<servo_axis>().SetAngle(vectorAction[0]);
		RightUpper.GetComponent<servo_upper>().SetAngle(vectorAction[1]);
		RightLower.GetComponent<servo_upper>().SetAngle(vectorAction[2]);
		LeftAxis.GetComponent<servo_axis>().SetAngle(vectorAction[3]);
		LeftUpper.GetComponent<servo_upper>().SetAngle(vectorAction[4]);
		LeftLower.GetComponent<servo_upper>().SetAngle(vectorAction[5]);

		SetReward(0.1f);
		SetReward(-(IMU.GetComponent<imu>().linAccel + Vector3.up*9.81f).magnitude); //loss if bipod not upright
		SetReward(-(IMU.GetComponent<imu>().angAccel).magnitude); //loss if bipod rotating
        // if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        // {
        //     float action_z = 2f * Mathf.Clamp(vectorAction[0], -1f, 1f);
        //     if ((gameObject.transform.rotation.z < 0.25f && action_z > 0f) ||
        //         (gameObject.transform.rotation.z > -0.25f && action_z < 0f))
        //     {
        //         gameObject.transform.Rotate(new Vector3(0, 0, 1), action_z);
        //     }
        //     float action_x = 2f * Mathf.Clamp(vectorAction[1], -1f, 1f);
        //     if ((gameObject.transform.rotation.x < 0.25f && action_x > 0f) ||
        //         (gameObject.transform.rotation.x > -0.25f && action_x < 0f))
        //     {
        //         gameObject.transform.Rotate(new Vector3(1, 0, 0), action_x);
        //     }

        //     SetReward(0.1f);

        // }
        // if ((ball.transform.position.y - gameObject.transform.position.y) < -2f ||
        //     Mathf.Abs(ball.transform.position.x - gameObject.transform.position.x) > 3f ||
        //     Mathf.Abs(ball.transform.position.z - gameObject.transform.position.z) > 3f)
        // {
        //     Done();
        //     SetReward(-1f);
        // }


    }

    public override void AgentReset()
    {
        //todo reset
		RightAxis.GetComponent<servo_axis>().SetAngle(0);
		RightUpper.GetComponent<servo_upper>().SetAngle(0);
		RightLower.GetComponent<servo_upper>().SetAngle(0);
		LeftAxis.GetComponent<servo_axis>().SetAngle(0);
		LeftUpper.GetComponent<servo_upper>().SetAngle(0);
		LeftLower.GetComponent<servo_upper>().SetAngle(0);
    }

}
