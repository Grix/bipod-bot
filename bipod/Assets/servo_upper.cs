using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class servo_upper : MonoBehaviour {

    public float targetAngle = 5;
	float torque = 200;

    void Start () {
	}
	
	void Update () {
		
		//rotate towards target angle
		float currentAngle = transform.localEulerAngles.z;
		if (currentAngle > 180) 
			currentAngle -= 360;
		float rotAngle = Mathf.Clamp(targetAngle - currentAngle, -1, 1) * torque * Time.deltaTime;
		rotAngle = Mathf.Clamp(rotAngle, -Mathf.Abs(targetAngle - currentAngle), Mathf.Abs(targetAngle - currentAngle));
		transform.Rotate(Vector3.forward * rotAngle);
		
	}

	public void SetAngle(float offset)
	{
		targetAngle = Mathf.Clamp(offset, -30, 30);
	}
}
