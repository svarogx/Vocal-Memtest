using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour {

	public GameObject RefreshButton;
	public GameObject PanelScore;
	public GameObject PanelRecord;
	public GameObject PanelTimer;
	public GameObject PanelTry;
	public GameObject PanelEnd;

	private Text ScoreText;
	private Text RecordText;
	private Text TimerText;
	private Text TryText;

	private float timer;
	private int score;
	private int record;
	private bool timeout;
	private bool repeat;
	private int level;
	private int tryLeft;

	void Awake(){
		ScoreText = PanelScore.GetComponentInChildren<Text> ();
		RecordText = PanelRecord.GetComponentInChildren<Text> ();
		TimerText = PanelTimer.GetComponentInChildren<Text> ();
		TryText = PanelTry.GetComponentInChildren<Text> ();

		PanelEnd.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		score = 0;
		record = 0;
		timeout = true;
		repeat = false;
		tryLeft = 0;
		if (!PlayerPrefs.HasKey ("Record")) {
			PlayerPrefs.SetInt ("Record", record);
			PlayerPrefs.Save ();
		} else {
			record = PlayerPrefs.GetInt ("Record");
		}

		level = 0;
		if (!PlayerPrefs.HasKey ("Level")) {
			PlayerPrefs.SetInt ("Level", level);
			PlayerPrefs.Save ();
		} else {
			level = PlayerPrefs.GetInt ("Level");
		}

		switch (level) {
		case 0:
			RefreshButton.SetActive (true);
			PanelTimer.SetActive (false);
			PanelTry.SetActive (false);
			break;
		case 1:
			RefreshButton.SetActive (false);
			PanelTimer.SetActive (false);
			PanelTry.SetActive (true);
			tryLeft = 3;
			break;
		case 2:
			RefreshButton.SetActive (false);
			PanelTimer.SetActive (true);
			PanelTry.SetActive (true);
			tryLeft = 1;
			break;
		}
		ScoreText.text = score.ToString ();
		RecordText.text = record.ToString ();
		TimerText.text = timer.ToString ("F1");
		TryText.text = tryLeft.ToString ();
	}

	public void StartTimer(int userLevel){
		if (level != 2)
			return;
		float factor = 0.5f * ((float)(userLevel - 1) / 9.0f);
		factor = (factor > 0.5f) ? 0.5f : factor;
		factor = 1.5f - factor;
		timer = factor * userLevel;
		timeout = false;
		StartCoroutine ("Countdown"); 
	}

	private void StopTimer(){
		StopCoroutine ("Countdown"); 
		TimerText.text = "";
	}

	IEnumerator Countdown(){
		float delta = 0.1f;
		while (timer > 0.0f) {
			TimerText.text = timer.ToString ("F1");
			TimerText.color = Color.green;
			timer -= delta;
			yield return new WaitForSeconds (delta);
		}
		timer = 0.0f;
		TimerText.text = timer.ToString ("F1");
		TimerText.color = Color.red;
		timeout = true;
	}

	public void RepeatPulse(){
		repeat = true;
	}

	public void WinRound(){
		StopTimer ();
		int point = 0;
		if (!timeout)
			point = 2;
		else if (!repeat)
			point = 1;
		score += point; 
		ScoreText.text = score.ToString ();
		if (score > record) {
			record = score;
			PlayerPrefs.SetInt ("Record", record);
			RecordText.text = record.ToString ();
		}
		repeat = false;
	}

	public void LoseRound(){
		if (level == 0)
			return;
		tryLeft -= 1;
		TryText.text = tryLeft.ToString ();
		if (tryLeft > 0)
			return;
		BackgroundControl.sharedInstance.EndGame ();
		Time.timeScale = 0.0f;
		PanelEnd.SetActive (true);
	}
}
