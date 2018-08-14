using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	[SerializeField]
	private GameObject blinkingText;

	private AudioSource audioSource;
	[SerializeField]
	private AudioClip selectAudio;

	[SerializeField]
	private TextMeshProUGUI distance;
	[SerializeField]
	private TextMeshProUGUI highestDistance;
	

	void Start ()
	{
		StartCoroutine(TextBlink());
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.R))
		{
			audioSource.clip = selectAudio;
			audioSource.Play();
			SceneManager.LoadScene(1);
		}

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			audioSource.clip = selectAudio;
			audioSource.Play();
			SceneManager.LoadScene(0);
		}

		distance.text = GameManager.instance.Distance + "m";
		highestDistance.text = GameManager.instance.HighestDistance + "m";
	}

	IEnumerator TextBlink()
	{
		while (true)
		{
			blinkingText.SetActive(true);
			yield return new WaitForSecondsRealtime(0.5f);
			blinkingText.SetActive(false);
			yield return new WaitForSecondsRealtime(0.5f);
		}
	}
}
