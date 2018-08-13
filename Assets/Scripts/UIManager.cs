using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI distance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		distance.text = GameManager.instance.Distance + "m";
	}
}
