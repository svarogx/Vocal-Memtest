using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteInit : MonoBehaviour {

	[HideInInspector] public bool canProceed = false;

	private Toggle toggle;

	void Awake () {
		toggle = GetComponent<Toggle> ();	
	}
	
	void Start(){
		canProceed = false;
		if (toggle.isOn == BackgroundControl.sharedInstance.GetComponent<AudioSource> ().mute)
			canProceed = true;
		else
			toggle.isOn = BackgroundControl.sharedInstance.GetComponent<AudioSource> ().mute;
	}

	void OnDisable(){
		//		Debug.Log ("DISABLE");
	}

	public void OnToggle(){
		if (!canProceed) {
			canProceed = true;
			return;
		}
		BackgroundControl.sharedInstance.ToggleMute ();
	}
}
