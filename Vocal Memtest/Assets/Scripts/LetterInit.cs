using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterInit : MonoBehaviour {

	private bool canProceed = true;
	private Toggle toggle;

	private bool varToggle = true;

	// PlayerPrefs Memory
	// MaxLetter	BOOL	
	// MinLetter	BOOL
	// PrintLetter	BOOL
	// ScriptLetter	BOOL

	void Awake(){
		toggle = GetComponent<Toggle> ();
	}

	// Use this for initialization
	void Start () {
		
		switch (this.gameObject.name) {
		case "max":
//			if (!PlayerPrefs.HasKey ("MaxLetter"))
//				PlayerPrefs.SetInt ("MaxLetter", 1);
			varToggle = (PlayerPrefs.GetInt ("MaxLetter") > 0) ? true : false; 
			break;
		case "min":
//			if (!PlayerPrefs.HasKey ("MinLetter"))
//				PlayerPrefs.SetInt ("MinLetter", 1);
			varToggle = (PlayerPrefs.GetInt ("MinLetter") > 0) ? true : false; 
			break;
		case "print":
//			if (!PlayerPrefs.HasKey ("PrintLetter"))
//				PlayerPrefs.SetInt ("PrintLetter", 1);
			varToggle = (PlayerPrefs.GetInt ("PrintLetter") > 0) ? true : false; 
			break;
		case "script":
//			if (!PlayerPrefs.HasKey ("ScriptLetter"))
//				PlayerPrefs.SetInt ("ScriptLetter", 1);
			varToggle = (PlayerPrefs.GetInt ("ScriptLetter") > 0) ? true : false; 
			break;
		}

		if ((toggle.isOn || varToggle) && !(toggle.isOn && varToggle)) {
			canProceed = false;
			toggle.isOn = varToggle;
		}
	}

	public void IsToggle(){
		if (!canProceed) {
			canProceed = true;
			return;
		}
		bool tgglOppose = true;
		switch (this.gameObject.name) {
		case "max":
			tgglOppose = (PlayerPrefs.GetInt ("MinLetter") > 0) ? true : false;
			if (!(tgglOppose || toggle.isOn)) {
				tgglOppose = false;
			} else {
				tgglOppose = true;
				PlayerPrefs.SetInt ("MaxLetter", toggle.isOn ? 1 : 0);
				PlayerPrefs.Save ();
			}
			break;
		case "min":
			tgglOppose =(PlayerPrefs.GetInt ("MaxLetter") > 0) ? true : false;
			if (!(tgglOppose || toggle.isOn)) {
				tgglOppose = false;
			} else {
				tgglOppose = true;
				PlayerPrefs.SetInt ("MinLetter", toggle.isOn ? 1 : 0);
				PlayerPrefs.Save ();
			}
			break;
		case "print":
			tgglOppose =(PlayerPrefs.GetInt ("ScriptLetter") > 0) ? true : false;
			if (!(tgglOppose || toggle.isOn)) {
				tgglOppose = false;
			} else {
				tgglOppose = true;
				PlayerPrefs.SetInt ("PrintLetter", toggle.isOn ? 1 : 0);
				PlayerPrefs.Save ();
			}
			break;
		case "script":
			tgglOppose =(PlayerPrefs.GetInt ("PrintLetter") > 0) ? true : false;
			if (!(tgglOppose || toggle.isOn)) {
				tgglOppose = false;
			} else {
				tgglOppose = true;
				PlayerPrefs.SetInt ("ScriptLetter", toggle.isOn ? 1 : 0);
				PlayerPrefs.Save ();
			}
			break;
		}
		if (!tgglOppose) {
			canProceed = false;
			toggle.isOn = !toggle.isOn;
		}
	}
}
