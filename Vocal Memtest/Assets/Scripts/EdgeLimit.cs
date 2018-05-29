using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLimit : MonoBehaviour {

	[Range(1,4)] public int edgeID = 1;
	public float edgeDelta = 1.0f;
	[Range(0.5f, 2.5f)]public float spawnTime = 0.5f;

//	private float edgeHeight;
//	private float edgeWidth;
	private Vector3 edgeZero;
	private Vector3 edgeScreen;

	private Vector3 edgePosition;

//	private BoxCollider2D edgeCtrl;

	void Start () {
//		edgeCtrl = this.GetComponent<BoxCollider2D> ();

/*		edgeScreen = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
		edgeZero = Camera.main.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, 0.0f));
		switch (edgeID) {
		case 1:		// top
			edgeWidth = edgeScreen.x * 2;
			edgeHeight = edgeDelta;
			edgePosition = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height, 0.0f));
			edgePosition.y = edgePosition.y + edgeDelta / 2;
			break;
		case 2:		// right
			edgeHeight = edgeScreen.y * 2;
			edgeWidth = edgeDelta;
			edgePosition = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height / 2, 0.0f));
			edgePosition.x = edgePosition.x + edgeDelta / 2;
			break;
		case 3:		// bottom
			edgeWidth = edgeScreen.x * 2;
			edgeHeight = edgeDelta;
			edgePosition = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, 0, 0.0f));
			edgePosition.y = edgePosition.y - edgeDelta / 2;
			break;
		case 4: 	// left
			edgeHeight = edgeScreen.y * 2;
			edgeWidth = edgeDelta;
			edgePosition = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height / 2, 0.0f));
			edgePosition.x = edgePosition.x - edgeDelta / 2;
			break;
		default:
			break;
		}
		edgeCtrl.size = new Vector2 (edgeWidth, edgeHeight);
		transform.position = edgePosition;
*/
		StartCoroutine ("EdgeResize");
		StartCoroutine ("SpawnLetter");
	}
	
	IEnumerator SpawnLetter(){
		yield return new WaitForSeconds (0.1f);
		float x = 0.0f;
		float y = 0.0f;
		Vector2 speed = new Vector2(0,0);
		while(true){
			switch (edgeID) {
			case 1:		// top
				y = transform.position.y;
				x = Random.Range (edgeZero.x, edgeScreen.x);
				speed.x = Random.Range (2, 11);
				speed.x = (Random.Range (-1.0f, 1.0f) >= 0) ? speed.x : -speed.x; 
				speed.y = -Random.Range (2, 11); 
				break;
			case 2:		// right
				x = transform.position.x;
				y = Random.Range (edgeZero.y, edgeScreen.y);
				speed.y = Random.Range (2, 11);
				speed.y = (Random.Range (-1.0f, 1.0f) >= 0) ? speed.y : -speed.y; 
				speed.x = -Random.Range (2, 11); 
				break;
			case 3:		// bottom
				y = transform.position.y;
				x = Random.Range (edgeZero.x, edgeScreen.x);
				speed.x = Random.Range (2, 11);
				speed.x = (Random.Range (-1.0f, 1.0f) >= 0) ? speed.x : -speed.x; 
				speed.y = Random.Range (2, 11); 
				break;
			case 4: 	// left
				x = transform.position.x;
				y = Random.Range (edgeZero.y, edgeScreen.y);
				speed.y = Random.Range (2, 11);
				speed.y = (Random.Range (-1.0f, 1.0f) >= 0) ? speed.y : -speed.y; 
				speed.x = Random.Range (2, 11); 
				break;
			default:
				break;
			}
			Vector3 letterPosition = new Vector3 (x, y, 0.0f);
			float spin = Random.Range (100, 500);

			GameObject letter = BackgroundControl.sharedInstance.GetPooledObject ("Letter");
			if (letter != null) {
				letter.transform.position = letterPosition;
//				letter.GetComponent<letterControl> ().originName = this.gameObject.name;
				letter.GetComponent<letterControl> ().speedVector = speed;
				letter.GetComponent<letterControl> ().speedRotation = spin;
				letter.SetActive (true);
			}
			yield return new WaitForSeconds (spawnTime);
		}
	}

	IEnumerator EdgeResize(){
		Vector3 tmpVector = Vector3.zero;
		edgeScreen = tmpVector;
		while (true) {
			tmpVector = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
			if (tmpVector.x != edgeScreen.x) {
				edgeScreen = tmpVector;
				edgeZero = Camera.main.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, 0.0f));
				switch (edgeID) {
				case 1:		// top
//					edgeWidth = edgeScreen.x * 2;
//					edgeHeight = edgeDelta;
					edgePosition = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height, 0.0f));
					edgePosition.y = edgePosition.y + edgeDelta / 2;
					break;
				case 2:		// right
//					edgeHeight = edgeScreen.y * 2;
//					edgeWidth = edgeDelta;
					edgePosition = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height / 2, 0.0f));
					edgePosition.x = edgePosition.x + edgeDelta / 2;
					break;
				case 3:		// bottom
//					edgeWidth = edgeScreen.x * 2;
//					edgeHeight = edgeDelta;
					edgePosition = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, 0, 0.0f));
					edgePosition.y = edgePosition.y - edgeDelta / 2;
					break;
				case 4: 	// left
//					edgeHeight = edgeScreen.y * 2;
//					edgeWidth = edgeDelta;
					edgePosition = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height / 2, 0.0f));
					edgePosition.x = edgePosition.x - edgeDelta / 2;
					break;
				default:
					break;
				}
//				edgeCtrl.size = new Vector2 (edgeWidth, edgeHeight);
				transform.position = edgePosition;
			}
			yield return new WaitForSeconds (1.0f);
		}
	}
}