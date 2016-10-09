using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public Transform yinPosition; //the one on the right side
	public Transform yangPosition; //the one on the left side

	public Rotate rotateObject;

	bool positionInward = true;
	bool coroutineRunning = false;

	public float moveSpeed = 1f;

	public WaterPhysicsYAxis water;
	public float rippleIntensity;

	// Use this for initialization
	void Start () {
		StartCoroutine("createRipples");
	}

	// Update is called once per frame
	/*
	void Update () {
		
		if (TouchRelease() || Input.GetMouseButtonDown(0)) {
			if (rotateObject.gameStarted) {

				if(!coroutineRunning)
					StartCoroutine ("moveDot");
				if (positionInward) {
					yangPosition.transform.position = new Vector3 (yangPosition.transform.position.x - 1.5f,
						yangPosition.transform.position.y,
						yangPosition.transform.position.z);

					yinPosition.transform.position = new Vector3 (yinPosition.transform.position.x + 1.5f,
						yinPosition.transform.position.y,
						yinPosition.transform.position.z);
				} else {
					yangPosition.transform.position = new Vector3 (yangPosition.transform.position.x + 1.5f,
						yangPosition.transform.position.y,
						yangPosition.transform.position.z);

					yinPosition.transform.position = new Vector3 (yinPosition.transform.position.x - 1.5f,
						yinPosition.transform.position.y,
						yinPosition.transform.position.z);
				}

				//positionInward = !positionInward;

			}
		}	
	}
	*/

	void Update()
	{
		if (Input.touchCount > 0 || Input.GetMouseButtonDown (0)) {
			
			bool touchCheck = Camera.main.ScreenToViewportPoint (Input.GetTouch (0).position).x > 0.5;

			print ("yang coordinates" + yangPosition.position);
			print ("screen width " + Screen.width);
			print ("converted touch coordinates" + Camera.main.ScreenToViewportPoint (Input.GetTouch (0).position));

			if (touchCheck) {
				//move apart
				//if (Camera.main.WorldToViewportPoint (yangPosition.position).x > 0.1) 
				{
					yangPosition.Translate (new Vector3 (-0.1f * moveSpeed, 0, 0));

					//if( Camera.main.WorldToViewportPoint(yinPosition.position).x > 0.6 && Camera.main.WorldToViewportPoint(yinPosition.position).x < 0.9 )
					yinPosition.Translate (new Vector3 (0.1f * moveSpeed, 0, 0));
					//yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-5f, yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
					//yangPosition.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (5f, yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
				}
				print ("touched right side");
			} else {
				//move closer
				if (Camera.main.WorldToViewportPoint (yangPosition.position).x < 0.35) 
				{
					yangPosition.Translate (new Vector3 (0.1f * moveSpeed, 0, 0));

					//if( Camera.main.WorldToViewportPoint(yinPosition.position).x > 0.6 && Camera.main.WorldToViewportPoint(yinPosition.position).x < 0.9 )
					yinPosition.Translate (new Vector3 (-0.1f * moveSpeed, 0, 0));
					//yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (5f, yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
					//yangPosition.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-5f, yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
				}
				print ("touched left side");
			}
		} else {
			yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
			yangPosition.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, yinPosition.gameObject.GetComponent<Rigidbody2D> ().velocity.y);
		}
	}


	IEnumerator moveDot()
	{
		coroutineRunning = true;

		for (int i = 0; i < 10; i++) {
			if (positionInward) {
				yangPosition.transform.position = new Vector3 (yangPosition.transform.position.x - 0.2f,
					yangPosition.transform.position.y,
					yangPosition.transform.position.z);

				yinPosition.transform.position = new Vector3 (yinPosition.transform.position.x + 0.2f,
					yinPosition.transform.position.y,
					yinPosition.transform.position.z);
			} else {
				yangPosition.transform.position = new Vector3 (yangPosition.transform.position.x + 0.2f,
					yangPosition.transform.position.y,
					yangPosition.transform.position.z);

				yinPosition.transform.position = new Vector3 (yinPosition.transform.position.x - 0.2f,
					yinPosition.transform.position.y,
					yinPosition.transform.position.z);
			}
			yield return new WaitForSeconds (0.001f);
		}

		positionInward = !positionInward;

		coroutineRunning = false;
	}


	public static bool TouchRelease() {
		bool b = false;
		for (int i = 0; i < Input.touches.Length; i++) {
			b = Input.touches[i].phase == TouchPhase.Ended;
			if (b)
				break;
		}
		return b;
	}

	IEnumerator createRipples()
	{
		while (true) {
			water.Splash (-8.5f, rippleIntensity * 0.5f);
			//water.Splash (0, intensity * 0.5f);
			water.Splash (15f, rippleIntensity * 0.5f);

			rippleIntensity += 0.5f;
			yield return new WaitForSeconds (1f);
		}
	}
}
