using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private AudioSource clickAudio;

	private void Start()
	{
		clickAudio = GetComponent<AudioSource>();
	}

	public void StartGame()
	{
		clickAudio.Play();
		SceneManager.LoadScene(1);
	}
}
