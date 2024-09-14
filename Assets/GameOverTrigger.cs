using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
	public GameObject gameOverText;

	private GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			gameOverText.gameObject.SetActive(true);
		}
	}

	// void OnTriggerExit(Collider other)
	// {
	// 	// if(other.gameObject == player)
	// 	// {
	// 	// 	manager.SetShowMsg(false);
	// 	// 	Destroy(gameObject);
	// 	// }
	// }
}
