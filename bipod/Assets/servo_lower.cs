using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class servo_lower : MonoBehaviour {

    public float targetAngle = 0;
	float torque = 200;

    void Start () {
	}
	
	void Update () {
		
		//rotate towards target angle
		float targetAngleDeg = targetAngle*52.5f+47.5f;
		float currentAngle = transform.localEulerAngles.z;
		if (currentAngle > 180) 
			currentAngle -= 360;
		float rotAngle = Mathf.Clamp(targetAngleDeg - currentAngle, -1, 1) * torque * Time.deltaTime;
		rotAngle = Mathf.Clamp(rotAngle, -Mathf.Abs(targetAngleDeg - currentAngle), Mathf.Abs(targetAngleDeg - currentAngle));
		transform.Rotate(Vector3.forward * rotAngle);
		
	}

	public void SetAngle(float offset)
	{
		targetAngle = Mathf.Clamp(offset, -1, 1);
	}
}
