using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour {

	public float stepTime = 0.1f;

	public Image background;
	private Color backColor;

	public GameObject logo;
	private RectTransform logoRect;
	private Image logoImage;
	private Vector3 logoScale;
	private Color logoColor;

	public float initialTransition = 1.0f;

	public float logoTransition = 2.0f;

	public string nextScene;

	public AudioClip[] logoAudio;
	private AudioSource logoSound;

	void Awake(){
		logoRect = logo.GetComponent<RectTransform> ();
		logoImage = logo.GetComponent<Image> ();
		logoSound = GetComponent<AudioSource> ();
	}

	void Start () {
		// Inicializacion de memoria Player
		if (!PlayerPrefs.HasKey ("MaxLetter")){
			PlayerPrefs.SetInt ("MaxLetter", 1);
			PlayerPrefs.Save ();
		}
		if (!PlayerPrefs.HasKey ("MinLetter")){
			PlayerPrefs.SetInt ("MinLetter", 1);
			PlayerPrefs.Save ();
		}
		if (!PlayerPrefs.HasKey ("PrintLetter")){
			PlayerPrefs.SetInt ("PrintLetter", 1);
			PlayerPrefs.Save ();
		}
		if (!PlayerPrefs.HasKey ("ScriptLetter")){
			PlayerPrefs.SetInt ("ScriptLetter", 1);
			PlayerPrefs.Save ();
		}
		if (!PlayerPrefs.HasKey ("Level")) {
			PlayerPrefs.SetInt ("Level", 0);
			PlayerPrefs.Save ();
		}

		backColor = background.color;
		backColor.a = 0.0f;
		background.color = backColor;

		logoColor = logoImage.color;
		logoColor.a = 1.0f;
		logoImage.color = logoColor;

		logoScale = Vector3.zero;
		logoRect.localScale = logoScale;

		logoSound.Stop ();
		logoSound.loop = false;
		logoSound.playOnAwake = false;
		StartCoroutine ("TransitionControl");
	}
	
	IEnumerator TransitionControl(){
		AsyncOperation ao = SceneManager.LoadSceneAsync (nextScene, LoadSceneMode.Single);
		ao.allowSceneActivation = false;

		float secTime1 = 0.0f;
		float deltaAlpha = 1 / (initialTransition / stepTime);
		while(secTime1 <= initialTransition){
			yield return new WaitForSeconds (stepTime);
			secTime1 += stepTime;
			backColor.a += deltaAlpha;
			background.color = backColor;
		}

		logoSound.clip = logoAudio [Random.Range (0, logoAudio.Length)];
		logoSound.Play ();

		float deltaScale = 1 / (logoTransition / stepTime);
		while (logoScale.x < 1) {
			yield return new WaitForSeconds (stepTime);
			logoScale.x += deltaScale; 
			logoScale.y += deltaScale; 
			logoScale.z += deltaScale; 
			logoRect.localScale = logoScale;
		}

		while (logoSound.isPlaying) {
			yield return new WaitForSeconds (stepTime);
		}

		secTime1 = 0.0f;
		deltaAlpha = 1 / (initialTransition / stepTime);
		while(secTime1 <= initialTransition){
			yield return new WaitForSeconds (stepTime);
			secTime1 += stepTime;
			logoColor.a = initialTransition - secTime1;
			logoImage.color = logoColor;
		}

		ao.allowSceneActivation = true;
		while (ao.progress < 1.0f) {
			yield return new WaitForSeconds (stepTime);
		}

	}


}
