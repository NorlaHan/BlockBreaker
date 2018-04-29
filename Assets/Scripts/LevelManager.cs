using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private MusicPlayer musicPlayer;

	public void LoadLevel (string name) {
		Debug.Log ("Level load requested for : " + name);
		Brick.breakableCount = 0;
		Application.LoadLevel (name);
	}

	public void QuitLevel () {
		Debug.Log ("Level quit.");
		Application.Quit();
	}

	public void LoadNextLevel ()	{
		Debug.Log ("Load next level.");
		Brick.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel+1);
	}

	public void BrickDestroyed (){
		if (Brick.breakableCount<=0) {
			LoadNextLevel();
			Debug.Log ("Breakable count remains " + Brick.breakableCount);
		}
	}

	// TODO Find why GameObject.FindObjectOfType<MusicPlayer>() will get the object which is destroyed at awake?
	void Music ()
	{
		musicPlayer = GameObject.FindObjectOfType<MusicPlayer> ();
		if (musicPlayer != null) {
			musicPlayer.ChangeMusic ();
			Debug.Log ("Change music" + musicPlayer.GetInstanceID ());
		}
	}

	// Use this for initialization
	void Start () {
		Music();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
