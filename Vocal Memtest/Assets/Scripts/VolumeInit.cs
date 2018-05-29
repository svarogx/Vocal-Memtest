using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeInit : MonoBehaviour {

	private Slider slider;

	void Awake(){
		slider = GetComponent<Slider> ();
	}

	void OnEnable(){
		slider.value = BackgroundControl.sharedInstance.GetComponent<AudioSource> ().volume;
	}

	void OnDisable(){
//		Debug.Log ("DISABLE");
	}

	public void VolumeChange(){
		BackgroundControl.sharedInstance.SetVolume (slider.value);
	}

}
