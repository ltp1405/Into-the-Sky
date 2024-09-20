using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverTrigger : MonoBehaviour
{
	public static UnityAction OnGameOver;
	public GameObject gameOverText;

	private GameObject player;
	public TextMeshProUGUI timeTextMesh;
	public PlayerSaveManager saveManager;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			OnGameOver?.Invoke();
			gameOverText.gameObject.SetActive(true);
			timeTextMesh.text = "Time spent: " + ((float) saveManager.besttime).ToString("F2") + " seconds";
			StartCoroutine(EndGame());
		}
	}

	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(5);
		gameOverText.gameObject.SetActive(false);
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
