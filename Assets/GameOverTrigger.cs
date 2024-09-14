using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public string message = "";
	public string message2 = "";
	public KeyCode changeMessageKey;

	private GameObject player;
	private bool used = false;

	private ControlsTutorial manager;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControlsTutorial> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if((other.gameObject == player) && !used)
		{
			manager.SetShowMsg(true);
			manager.SetMessage(message);
			used = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		// if(other.gameObject == player)
		// {
		// 	manager.SetShowMsg(false);
		// 	Destroy(gameObject);
		// }
	}
}
