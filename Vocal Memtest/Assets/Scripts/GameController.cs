using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class vocals{
	public Sprite[] vocal;
	public AudioClip vocalSound;
}

public class GameController : MonoBehaviour {

	public ItemControl[] vocalArray;
	public vocals[] vocalItem;
	public AudioClip wrongSound;
//	public Text statusBox;

	[Range(1.0f, 5.0f)]
	public float minRandomTime = 1.0f;
	[Range(5.1f, 10.0f)]
	public float maxRandomTime = 5.1f;

	public GameObject optionPanel;
	public GameObject refreshButton;

	private bool runTutorial = false;
	private bool runProcess = false;

	private List<int> listSec;
	private int indexSec;
	private HUDControl HUD;

	void Awake(){
		HUD = GetComponent<HUDControl> ();
	}

	// Use this for initialization
	void Start () {
		optionPanel.SetActive (false);

		// Inicialización Vocales
		for (int i = 0; i < vocalArray.Length; i++) {
			vocalArray [i].itemVocal = vocalItem [i].vocal;
			vocalArray [i].vocalSound = vocalItem [i].vocalSound;
			vocalArray [i].vocalWrong = wrongSound;
			vocalArray [i].minRandom = minRandomTime;
			vocalArray [i].maxRandom = maxRandomTime;
			vocalArray [i].vocalIndex = i;
		}

		// Inicializacón Secuencia
		listSec = new List<int>();
		listSec.Clear ();
		listSec.Add (Random.Range (0, 5));
		Invoke ("InitTutorial", 1.0f);		
		StartCoroutine ("EscapeButton");
	}

	IEnumerator EscapeButton(){
		while (true) {
			yield return new WaitForSeconds (0.05f);
			if (Input.GetKey (KeyCode.Escape)) {
				if (optionPanel.activeInHierarchy) {
					CloseOptions ();
				}  else {
					GoHome ();
				}
			}
		}

	}

	public void VocalTouch(int vocalIndex){
		if (runTutorial)
			return;
		if (runProcess)
			return;
		if (vocalIndex == listSec [indexSec]) {
			vocalArray [vocalIndex].WinStatus (false);
			indexSec += 1;
			if (indexSec >= listSec.Count) {
				runProcess = true;
				listSec.Add (Random.Range (0, 5));
				Invoke ("InitTutorial", 1.0f);
				HUD.WinRound ();
				return;
			}
		} else {
			vocalArray [vocalIndex].LoseStatus ();
			indexSec = 0;
			HUD.LoseRound ();
		}
		runProcess = true;
		Invoke ("EndProcess", 1.0f);
	}

	private void EndProcess(){
		runProcess = false;
	}

	public void PulseTutorial(){
		HUD.RepeatPulse ();
		InitTutorial ();
	}

	private void InitTutorial(){
		indexSec = 0;
		runProcess = false;
		runTutorial = true;
		TutorialMode ();
	}

	private void TutorialMode(){
		vocalArray [listSec [indexSec]].WinStatus (true);
		indexSec += 1;
		if (indexSec < listSec.Count)
			Invoke ("TutorialMode", 1.1f);
		else {
			Invoke ("EndTutorial", 1.0f);
		}
	}

	private void EndTutorial(){
		runTutorial = false;
		indexSec = 0;
		HUD.StartTimer (listSec.Count);
	}

	public void OpenOptions(){
		optionPanel.SetActive (true);
	}

	public void CloseOptions(){
		optionPanel.SetActive (false);
	}

	public void VolumeChange(Slider slider){
		foreach (ItemControl item in vocalArray) {
			item.SetVolume (slider.value);
		}
	}

	public void MuteChange(Toggle toggle){
		foreach (ItemControl item in vocalArray) {
			item.SetMute (toggle.isOn);
		}
	}

	public void GoHome(){
		Time.timeScale = 1.0f;
		BackgroundControl.sharedInstance.SceneIntro ();
	}

	public void Reload(){
		Time.timeScale = 1.0f;
		BackgroundControl.sharedInstance.SceneGame ();
	}

}
