using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemControl : MonoBehaviour, IPointerDownHandler {

	[HideInInspector]public int vocalIndex = -1;

	[HideInInspector]public Sprite[] itemVocal;

	[HideInInspector]public AudioClip vocalSound;
	[HideInInspector]public AudioClip vocalWrong;

	[HideInInspector]public bool isPlaying = false;

	[HideInInspector]public float minRandom;
	[HideInInspector]public float maxRandom;

	public Image imageControl;
	public Sprite imgOK;
	public Sprite imgBAD;
	public Sprite imgTUTORIAL;

	private Image vocalRender;
	private AudioSource vocalAudio;
	private GameController gameControl;

	public List<int> vocalEnabled;

	void Awake(){
		gameControl = GetComponentInParent<GameController> ();
		vocalRender = GetComponent<Image> ();
		vocalAudio = GetComponent<AudioSource> ();
	}

	void Start () {
		bool maxLetter = (PlayerPrefs.GetInt ("MaxLetter") > 0) ? true : false; 
		bool minLetter = (PlayerPrefs.GetInt ("MinLetter") > 0) ? true : false; 
		bool printLetter = (PlayerPrefs.GetInt ("PrintLetter") > 0) ? true : false; 
		bool scriptLetter =  (PlayerPrefs.GetInt ("ScriptLetter") > 0) ? true : false; 

		vocalEnabled = new List<int> ();
		if (maxLetter && printLetter)
			vocalEnabled.Add (0);	// 0 - Mayuscula Print
		if (minLetter && printLetter)
			vocalEnabled.Add (1);	// 1 - Minuscula Print 
		if (maxLetter && scriptLetter)
			vocalEnabled.Add (2);	// 2 - Mayuscula Script		
		if (minLetter && scriptLetter)
			vocalEnabled.Add (3);	// 3 - Minuscula Script

		StartCoroutine("ColorControl");
	}

	IEnumerator ColorControl(){
		while (vocalIndex == -1) {
			yield return new WaitForSeconds (0.1f);
		}
		vocalRender.sprite = itemVocal[vocalEnabled[Random.Range(0, vocalEnabled.Count)]];
		IdleStatus ();
		while (true){
			yield return new WaitForSeconds (Random.Range(minRandom, maxRandom));
			ChangeVocal ();
		}
	}

	private void IdleStatus(){
		isPlaying = false;
		Color tmpC = Color.white;
		tmpC.r = Random.Range (0.0f, 1.0f);
		tmpC.g = Random.Range (0.0f, 1.0f);
		tmpC.b = Random.Range (0.0f, 1.0f);
		tmpC.a = 0.5f;
		vocalRender.color = tmpC;
		imageControl.enabled = false;
	}

	private void ChangeVocal(){
		vocalRender.sprite = itemVocal[vocalEnabled[Random.Range(0, vocalEnabled.Count)]];
	}

	public void OnPointerDown(PointerEventData data){
		if (isPlaying)
			return;
		gameControl.VocalTouch(vocalIndex);
	}

	public void WinStatus(bool modeSys){
		if (isPlaying)
			return;
		vocalAudio.clip = vocalSound;
		vocalAudio.loop = false;
		vocalAudio.Play ();
		Invoke ("IdleStatus", 1.0f);
		isPlaying = true;
		imageControl.enabled = true;
		if (modeSys)
			imageControl.sprite = imgTUTORIAL;
		else
			imageControl.sprite = imgOK;
		
	}

	public void LoseStatus(){
		if (isPlaying)
			return;
		vocalAudio.clip = vocalWrong;
		vocalAudio.loop = false;
		vocalAudio.Play ();
		Invoke ("IdleStatus", 1.0f);
		isPlaying = true;
		imageControl.enabled = true;
		imageControl.sprite = imgBAD;
	}

	public void SetVolume(float level){
		if (level >= 1.0f)
			level = 1.0f;
		if (level <= 0.0f)
			level = 0.0f;
		vocalAudio.volume = level;
	}

	public void SetMute(bool state){
		vocalAudio.mute = state;
	}

}