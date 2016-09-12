using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    public AudioClip[] musics;
    
    private static MusicPlayer _instance = null;
    private AudioSource _audio;
	
	void Start () {
		if (_instance != null && _instance != this) {
			Destroy (gameObject);
		} else {
			_instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            _audio = GetComponent<AudioSource>();

            _audio.clip = musics[0];
            _audio.loop = true;
            _audio.Play();
        }
		
	}


    void OnLevelWasLoaded(int level)
    {
        if (_audio)
        {
            _audio.Stop();
            _audio.clip = musics[level];
            _audio.loop = true;
            _audio.Play();
        }
    }
    
}
