using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class joy : MonoBehaviour {

	public Text t;
	void Update () 
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		transform.rotation = Quaternion.Euler( new Vector3( 0, 0, h*-20 ) );

		t.text = "Axis : " + h;
	}
}
