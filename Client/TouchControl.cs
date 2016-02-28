using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {

	public Transform PointIcon;
	public Transform Target;
	public Transform Player;
	public float targetDistance;

	void Start ()
	{
		Target = PointIcon;
		PointIcon.position = Player.position;
		PointIcon.gameObject.GetComponent<Renderer>().enabled = false;
	}
	void Update () 
	{
		if( Input.GetMouseButtonDown(0) )
		{
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if( Physics.Raycast( r, out hit, 1000.0f ) )
			{
				if( hit.collider.gameObject.tag == "Ground" )
				{
					PointIcon.position = hit.point;
					Target = PointIcon;
					PointIcon.gameObject.GetComponent<Renderer>().enabled = true;
				}
				else
				{
					Target = hit.collider.gameObject.transform;
					PointIcon.gameObject.GetComponent<Renderer>().enabled = false;
				}
			}
		}
		targetDistance = Vector3.Distance( Player.position, Target.position );
		if( targetDistance < 1.0f ) PointIcon.gameObject.GetComponent<Renderer>().enabled = false;
	}
	public void TargetReset ()
	{
		Target = PointIcon;
		PointIcon.gameObject.GetComponent<Renderer>().enabled = false;
		PointIcon.position = Player.position;
	}
}
