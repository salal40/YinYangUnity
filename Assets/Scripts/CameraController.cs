using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform followingPoint;
	public Rotate rotateObject;

	bool follow = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(follow)
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, followingPoint.position.y+4, gameObject.transform.position.z);

		if(rotateObject != null)
			//if(rotateObject.startGame)
		if (Input.touchCount > 0 || Input.GetMouseButtonDown (0) || rotateObject.startGame)
					follow = true;
	}
}
