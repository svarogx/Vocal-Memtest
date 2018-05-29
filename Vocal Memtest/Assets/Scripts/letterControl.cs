using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterControl : MonoBehaviour {

//	[HideInInspector]public string originName;
	[HideInInspector]public Vector2 speedVector; // 2 - 10
	[HideInInspector]public float speedRotation; // 100 - 500
	public float stepTime = 0.03f;
//	private bool isVisible = false;

	private Rigidbody2D rB2;

	void Awake () {
		rB2 = GetComponent<Rigidbody2D> ();
	}
	
	void OnEnable(){
		rB2.velocity = speedVector; 
		StartCoroutine ("Rotation");
	}

//	void OnBecameVisible(){
//		isVisible = true;
//	}

	void OnBecameInvisible(){
//		if (!isVisible)
//			return;
//		isVisible = false;
		gameObject.SetActive (false);
	}

/*	void OnTriggerExit2D(Collider2D obj){
	void OnTriggerEnter2D(Collider2D obj){
		if (obj.gameObject.name == originName)
			return;
		gameObject.SetActive (false);
	}
*/
	IEnumerator Rotation(){
		while (true) {
			transform.Rotate(new Vector3(0.0f, 0.0f, speedRotation * stepTime));
			yield return new WaitForSeconds (stepTime);
		}			
	}
}
