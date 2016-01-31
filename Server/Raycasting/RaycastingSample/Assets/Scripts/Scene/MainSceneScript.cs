using UnityEngine;
using System.Collections;

public class MainSceneScript : MonoBehaviour {

    public Camera MainCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0)) {
            // Ray 按眉 积己
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            // rayCasting 角青
            rayCasting(ray);
        }
	}

    void rayCasting(Ray ray) {
        RaycastHit hitObj;
        if (Physics.Raycast(ray, out hitObj, Mathf.Infinity)) {
            if (hitObj.transform.tag.Equals("Cube")) {
                CubeScript cubeScript = hitObj.transform.GetComponent<CubeScript>();
                if (null != cubeScript) {
                    cubeScript.Hit();
                }
            }
        }
    }
}
