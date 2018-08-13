using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject music;

	private AudioSource clickAudio;

	private void Start()
	{
		clickAudio = GetComponent<AudioSource>();
	}

	public void StartGame()
	{
		clickAudio.Play();
		DontDestroyOnLoad(music);
		SceneManager.LoadScene(0);
	}
}
