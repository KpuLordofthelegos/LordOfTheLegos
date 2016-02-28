using UnityEngine;
using System.Collections;

public class ForceMoveCannon : MonoBehaviour {

	public AudioClip destroySound;
		void Start ()
	{
		GetComponent<Rigidbody>().AddForce (transform.forward * 1000.0f);
		GetComponent<Rigidbody>().AddTorque(new Vector3 (0, 0, 1) * 1000.0f);

	}

	void OnCollisionEnter( Collision _col )
	{
		if( _col.gameObject.tag == "Enemy" )
		{
			Destroy(gameObject);
			Destroy(_col.gameObject);
			AudioSource.PlayClipAtPoint(destroySound, new Vector3(0, 0, 0));
		}
	}
}
//rigidbody