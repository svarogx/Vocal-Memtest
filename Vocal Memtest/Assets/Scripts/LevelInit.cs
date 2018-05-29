using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInit : MonoBehaviour {

	public GameObject[] tags;

	private int level = 0;

	private Slider slider;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();

//		if (!PlayerPrefs.HasKey ("Level")) {
//			PlayerPrefs.SetInt ("Level", level);
//			PlayerPrefs.Save ();
//		} else {
			level = PlayerPrefs.GetInt ("Level");
//		}

		slider.value = level;
		for (int i = 0; i < tags.Length; i++) {
			if (i == level)
				tags [i].SetActive (true);
			else
				tags [i].SetActive (false);
		}
	}
	
	public void OnSliderChange(){
		if (slider.value == level)
			return;
		level = (int)slider.value;
		PlayerPrefs.SetInt ("Level", level);
		PlayerPrefs.Save ();
		for (int i = 0; i < tags.Length; i++) {
			if (i == (int)slider.value)
				tags [i].SetActive (true);
			else
				tags [i].SetActive (false);
		}
	}
}
