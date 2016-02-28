using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	public string State;
	public TouchControl core;
	public float MoveSpeed = 5.0f;
	public float RotationSpeed = 10.0f;
	public float Accelerator;
	public Transform axis;
	public bool keyMove = false;
	
	Animation ani;

	void Start ()
	{
		ani = transform.FindChild("Render").gameObject.GetComponent<Animation>();
	}

	void Update () 
	{
		ManualGravity ();
		WASDmove ();
		if( !keyMove ) PointTracking();

		switch( State )
		{
		case "STAND":
			ani.CrossFade("Idle");
			break;
		case "RUN":
			ani.CrossFade("Run2");
			break;
		}
	}

	void LookRot()
	{
		Vector3 rot = core.Target.transform.position - transform.position;
		Quaternion q = Quaternion.LookRotation( rot, Vector3.up );
		Quaternion sl = Quaternion.Slerp( transform.rotation, q, RotationSpeed * Time.deltaTime );
		Vector3 yAxis = sl.eulerAngles;
		yAxis.x = 0f;
		yAxis.z = 0f;
		transform.rotation = Quaternion.Euler(yAxis);
	}

	void ManualGravity ()
	{
		RaycastHit hit;
		Vector3 pos = transform.position;
		pos.y += 10f;
		GetComponent<Collider>().enabled = false;

		Debug.DrawRay(pos, Vector3.down*100.0f, Color.red);
		if( Physics.Raycast(pos , Vector3.down * 100.0f, out hit ) )
		{
			if( hit.collider.tag == "Ground" )
			{
				if( hit.point.y >= transform.position.y )
				{
					pos.y = hit.point.y;
					transform.position = pos;
					Accelerator = 0.0f;
				}
				else
				{
					Accelerator += 1;
					pos = transform.position;
					pos.y -= Accelerator * Time.deltaTime;
					transform.position = pos;
				}
			}

		}
		GetComponent<Collider>().enabled = true;
	}
	void WASDmove()
	{
		int h = 0;
		int v = 0;
		State = "STAND";
		keyMove = false;
		if( Input.GetKey( KeyCode.W ) ) v =  1;
		if( Input.GetKey( KeyCode.A ) ) h = -1;
		if( Input.GetKey( KeyCode.S ) ) v = -1;
		if( Input.GetKey( KeyCode.D ) ) h =  1;
		if( v!=0 || h!=0 ) 
		{
			transform.eulerAngles = new Vector3( 0, axis.rotation.eulerAngles.y, 0 );
			State = "RUN";
			keyMove = true;
			core.TargetReset();
		}
		transform.Translate( new Vector3(h,0,v) * MoveSpeed * Time.deltaTime );
	}

	void PointTracking()
	{
		ManualGravity ();
		if( core.targetDistance > 1.0f )
		{
			LookRot();
			transform.Translate( new Vector3( 0, 0, 1 ) * MoveSpeed * Time.deltaTime );
			State = "RUN";
		}
		else
			State = "STAND";
	}
}











