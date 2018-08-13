using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField]
	private GameObject gameOverPanel;
	[SerializeField]
	private AudioSource gameOverAudio;

	private int distance;

	public int Distance
	{
		get { return distance; }
		set { distance = value; }
	}

	public int HighestDistance
	{
		get
		{
			return PlayerPrefs.GetInt("HighestDistance");
		}
		set
		{
			PlayerPrefs.SetInt("HighestDistance", value);
		}
	}

	public Transform player;

	[SerializeField]
	private int minY = -7;

	private bool isPlayerAlive = true;

	private void Awake()
	{
		instance = this;
	}

	void Start ()
	{
		Time.timeScale = 1;
		distance = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		distance = (int)(player.position.x - transform.position.x);

		if(player.position.y < minY && isPlayerAlive)
		{
			GameOver();
			isPlayerAlive = false;
		}
	}

	public void GameOver()
	{
		if(Distance > HighestDistance)
		{
			HighestDistance = Distance;
		}
		Time.timeScale = 0;
		gameOverPanel.SetActive(true);
		gameOverAudio.Play();
	}
}
