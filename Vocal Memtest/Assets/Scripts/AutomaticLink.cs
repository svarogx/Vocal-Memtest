using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticLink : MonoBehaviour {

	public GameObject CreditsPanel;
	public GameObject ConfigPanel;
	public GameObject PanelRecord;

	private Text RecordText;

	void Awake(){
		RecordText = PanelRecord.GetComponentInChildren<Text> ();
	}


	void Start(){
		CreditsPanel.SetActive (false);
		ConfigPanel.SetActive (false);

		int record = 0;
		if (!PlayerPrefs.HasKey ("Record")) {
			PlayerPrefs.SetInt ("Record", record);
			PlayerPrefs.Save ();
		} else {
			record = PlayerPrefs.GetInt ("Record");
		}
		RecordText.text = record.ToString ();

		StartCoroutine ("EscapeButton");
	}


	IEnumerator EscapeButton(){
		while (true) {
			yield return new WaitForSeconds (0.05f);
			if (Input.GetKey (KeyCode.Escape)) {
				if (CreditsPanel.activeInHierarchy) {
					CloseCredits ();
				} else if (ConfigPanel.activeInHierarchy) {
					CloseConfig ();
				} else {
					BackgroundControl.sharedInstance.QuitGame ();
				}
			}
		}
			
	}


// * * * *	Inicio y Fin	* * * *
	public void OpenCredits(){
		CreditsPanel.SetActive (true);
	}

	public void CloseCredits(){
		CreditsPanel.SetActive (false);
	}

	public void OpenConfig(){
		ConfigPanel.SetActive (true);
	}

	public void CloseConfig(){
		ConfigPanel.SetActive (false);
	}

	public void LetsBegin(){
		BackgroundControl.sharedInstance.SceneGame ();
	}

// * * * *	Direcciones URL	* * * *
	public void OpenWWWPlaySolutions(){
		Application.OpenURL ("https://playsolutionsc.blogspot.com.co");
	}

	public void OpenWWWSoundImage(){
		Application.OpenURL("http://www.soundimage.org");
	}

	public void OpenWWWAlejandraAngel(){
		Application.OpenURL ("https://www.facebook.com/alejandraangelfotografia/");
	}

	public void OpenWWWDEFHARO(){
		Application.OpenURL ("http://www.defharo.com");
	}

	public void OpenWWWGOOGLE(){
		Application.OpenURL ("http://www.google.com");
	}

// * * * *	CONFIGURACION	* * * *
	public void VolumeChanged(Slider slider){
		BackgroundControl.sharedInstance.SetVolume (slider.value);
	}

	public void MuteChanged(MuteInit mute){
		if (!mute.canProceed) {
			mute.canProceed = true;
			return;
		}
		BackgroundControl.sharedInstance.ToggleMute ();
	}
}
