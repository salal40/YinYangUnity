using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Text[] menuTextsArray;
	public Rotate rotateObject;
	bool visible = true;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (visible) {
			if (rotateObject.startGame) {
				visible = false;
				//StartCoroutine ("FadeText", false);
				for (int i = 0; i < menuTextsArray.Length; i++) {
					menuTextsArray [i].CrossFadeAlpha (0, 0.5f, false);
				}
				//menuTextsArray [1].CrossFadeAlpha (0, 0.5f, false);
			}
		}
	}

	IEnumerator FadeText(bool fadeIn)
	{
		print ("Fading.....");
		for (int j = 0; j < 11; j++) {
			for (int i = 0; i < menuTextsArray.Length; i++) {
				menuTextsArray [i].color = new Color (menuTextsArray [i].color.r, menuTextsArray [i].color.g, menuTextsArray [i].color.b, menuTextsArray [i].color.a-25f);
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
}
