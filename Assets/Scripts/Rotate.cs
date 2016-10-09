using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public int rotationSpeed = 10;
	int count = 0;
	public bool startGame = false;
	public bool gameStarted = false;
	// Use this for initialization
	void Start () {

		if (gameObject.transform.name == "Yang")
			GetComponent<TrailRenderer> ().sortingLayerName = "TransparentFX";
			GetComponent<TrailRenderer> ().sortingOrder = -1;

		if (gameObject.transform.name == "Yin")
			GetComponent<TrailRenderer> ().sortingLayerName = "TransparentFX";
			GetComponent<TrailRenderer> ().sortingOrder = -2;
	
	}

	// Update is called once per frame
	void Update () {


		if (count < 72) {
			GetComponent<Transform> ().RotateAround (new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), 5);
			count++;
		} else if (count < 97) {

			GetComponent<TrailRenderer> ().time = 0.3f;
			GetComponent<TrailRenderer> ().endWidth = 0.1f;

			if (gameObject.transform.name == "Yang" && count < 97) {
				//print ("Yang");
				GetComponent<Transform> ().Translate (new Vector3 (-0.05f, 0, 0));

				count++;
			}
			if (gameObject.transform.name == "Yin" && count < 97) {
				//print ("Yin");
				GetComponent<Transform> ().Translate (new Vector3 (0.05f, 0, 0));

				count++;
			}
			GetComponent<Transform> ().Translate (new Vector3 (0, 0.1f * Time.deltaTime * 50, 0));
			gameStarted = true;
		} else {
			
			//GetComponent<Transform> ().Translate (new Vector3 (0, 0.1f * Time.deltaTime * 50, 0));
			//GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, 5);
		}

		if (!startGame) {
			count %= 72;
		}

		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
			startGame = true;
	}

}
