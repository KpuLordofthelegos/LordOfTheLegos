using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour {

	public Transform target;
	void Update () 
	{
		transform.position = target.position;
	}
}
