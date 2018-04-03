

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imu : MonoBehaviour {

	public Vector3 angVel = Vector3.zero;
	public Vector3 angAccel = Vector3.zero;
	public Vector3 linVel = Vector3.zero;
	public Vector3 linAccel = Vector3.zero;
	public float updateFreq = 100;

	private Vector3 linVelLast = Vector3.zero;
	private Vector3 angVelLast = Vector3.zero;
	private Vector3 posLast = Vector3.zero;
	private Vector3 angLast = Vector3.zero;
	private float timer;

	// Use this for initialization
	void Start () {
		posLast = transform.position;
		angLast = transform.rotation.eulerAngles;
		timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		timer += Time.deltaTime;

		if (timer > 1/updateFreq )
		{
			linVelLast = linVel;
			angVelLast = angVel;

			var posLastInv = transform.InverseTransformPoint(posLast);	
			linVel = (Vector3.zero - posLastInv)/timer;
			// print("------");
			// print(posLast);
			// print(linVel);
			posLast = transform.position;

			float deltaX = Mathf.Abs((transform.rotation.eulerAngles).x)-angLast.x;
			if (Mathf.Abs(deltaX)<180 && deltaX>-180) 
				angVel.x = deltaX/timer;
			else
			{
				if (deltaX>180) 
					angVel.x = (360-deltaX)/timer;
				else 
					angVel.x = (360+deltaX)/timer;
			}
	
			float deltaY = Mathf.Abs((transform.rotation.eulerAngles).y)-angLast.y;
			if (Mathf.Abs(deltaY)<180 && deltaY>-180) 
				angVel.y = deltaY/timer;
			else
			{
				if (deltaY>180) 
					angVel.y = (360-deltaY)/timer;
				else 
					angVel.y = (360-deltaY)/timer;
			}
	
			float deltaZ = Mathf.Abs((transform.rotation.eulerAngles).z)-angLast.z;
			if (Mathf.Abs(deltaZ)<180 && deltaZ>-180) 
				angVel.z = deltaZ/timer;
			else
			{
				if (deltaZ>180) 
					angVel.z = (360-deltaZ)/timer;
				else 
					angVel.z = (360+deltaZ)/timer;
			}

			linAccel = ((linVel - linVelLast)/timer);
			linAccel += transform.InverseTransformDirection(Vector3.up)*9.81f;
			// print(linAccel);
			
			angLast = new Vector3(	Mathf.Abs(transform.eulerAngles.x),
									Mathf.Abs(transform.eulerAngles.y),
									Mathf.Abs(transform.eulerAngles.z) );
			angAccel = ((angVel - angVelLast)/timer);

			timer = 0;
		}
	}
}
