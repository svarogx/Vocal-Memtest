using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ObjectPoolItem{
	public int amountToPool;
	public GameObject objectToPool;
	public bool shouldExpand;
}

public class BackgroundControl : MonoBehaviour {

	public static BackgroundControl sharedInstance = null;

	public bool dataPoolingEnabled = true;

	public List<ObjectPoolItem> itemsToPool;

	public string introScene;
	public AudioClip introMusic;
	public string gameScene;
	public AudioClip gameMusic;
	public AudioClip gameEnd;

	private List<GameObject> pooledObjects;
	private Sprite[] sprites;

	private SpriteRenderer srBackground;
	private AudioSource asBackground;

	private Vector3 screenSize;

	void Awake(){
		if (sharedInstance == null) {
			sharedInstance = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}

		srBackground = GetComponent<SpriteRenderer> ();			
		asBackground = GetComponent<AudioSource> ();

		SceneIntro ();
	}

	void Start () {
		sprites = Resources.LoadAll<Sprite>("alphabet");

		pooledObjects = new List<GameObject> ();
		foreach (ObjectPoolItem item in itemsToPool) {
			for (int i = 0; i < item.amountToPool; i++) {
				GameObject obj = (GameObject)Instantiate (item.objectToPool);
				obj.SetActive (false);
				obj.transform.SetParent(this.transform);
				pooledObjects.Add (obj);
			}
		}

		StartCoroutine("BackgroundResize");
	}

	IEnumerator BackgroundResize(){
		Vector3 tmpVector = Vector3.zero;
		screenSize = tmpVector;
		while (true) {
			tmpVector = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
			if (screenSize.x != tmpVector.x) {
				screenSize = tmpVector;
				srBackground.size = new Vector2 (screenSize.x * 2, screenSize.y * 2);
			}
			yield return new WaitForSeconds (1.0f);
		}
	}


	public GameObject GetPooledObject(string tag){
		if (!dataPoolingEnabled)
			return null;

		Sprite tmpImage = sprites [Random.Range (0, sprites.Length)];
		Color tmpColor = Color.white;
		tmpColor.r = Random.Range (0.0f, 1.0f);
		tmpColor.g = Random.Range (0.0f, 1.0f);
		tmpColor.b = Random.Range (0.0f, 1.0f);

		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy && pooledObjects [i].tag == tag) {
				pooledObjects [i].GetComponent<SpriteRenderer> ().sprite = tmpImage;
				pooledObjects [i].GetComponent<SpriteRenderer> ().color = tmpColor;
				return pooledObjects [i];
			}
		}
		foreach (ObjectPoolItem item in itemsToPool) {
			if (item.objectToPool.tag == tag) {
				if (item.shouldExpand) {
					GameObject obj = (GameObject)Instantiate (item.objectToPool);
					obj.transform.SetParent(this.transform);
					obj.SetActive (false);
					obj.GetComponent<SpriteRenderer> ().sprite = tmpImage;
					obj.GetComponent<SpriteRenderer> ().color = tmpColor;
					pooledObjects.Add (obj);
					return obj;
				}
			}
		}
		return null;
	}

	public void SceneIntro(){
		Scene scene = SceneManager.GetActiveScene ();
		if (scene.name == introScene) {
			if (!asBackground.isPlaying) {
				asBackground.clip = introMusic;
				asBackground.loop = true;
				asBackground.Play ();
			}
			return;
		}
		SceneManager.LoadScene (introScene);		
		asBackground.Stop ();
		asBackground.clip = introMusic;
		asBackground.loop = true;
		asBackground.Play ();
	}

	public void SceneGame(){
//		Scene scene = SceneManager.GetActiveScene ();
//		if (scene.name == gameScene) {
//			SceneManager.LoadScene (gameScene);		
//			return;
//		}
		SceneManager.LoadScene (gameScene);		
		bool tmpMute = asBackground.mute;
		asBackground.Stop ();
		asBackground.clip = gameMusic;
		asBackground.loop = true;
		asBackground.Play ();
		if (tmpMute)
			Invoke ("MuteON", 0.05f);
	}

	public void EndGame(){
		asBackground.Stop ();
		asBackground.clip = gameEnd;
		asBackground.loop = false;
		asBackground.Play ();
	}

	private void MuteON(){
		asBackground.mute = true;
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void Reset(){
		Debug.Log ("Reset");
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);		
	}

	public void SetVolume(float level){
		if (level >= 1.0f)
			level = 1.0f;
		if (level <= 0.0f)
			level = 0.0f;
		asBackground.volume = level;
	}

	public void ToggleMute(){
		asBackground.mute = !asBackground.mute;
	}

}