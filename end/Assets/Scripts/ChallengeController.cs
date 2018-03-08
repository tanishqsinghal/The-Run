using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeController : MonoBehaviour {

	public float scrollSpeed = 5.0f;

	public GameObject[] challenges;
	public float frequency = 0.5f;

	float counter = 0.0f;
	public Transform challengesSpawnPoint;

	bool isGameOver = false;

	void Start () {
		GenerateRandomChallenge ();
		SpeedAugmeneter ();
	}

	void SpeedAugmeneter ()
	{
		StartCoroutine ("SpeedChanger");
	}

	IEnumerator SpeedChanger()
	{
		yield return new WaitForSecondsRealtime (15);
		scrollSpeed++;
		SpeedAugmeneter();
	}

	void Update () {

		if (isGameOver)
			return;

		if (counter <= 0.0f) 
		{
			GenerateRandomChallenge ();
		}
		else
		{
			counter -= Time.deltaTime * frequency;
		}

		GameObject currentChild;

		for (int i = 0; i < transform.childCount; i++) 
		{
			currentChild = transform.GetChild (i).gameObject;
			ScrollChallenge (currentChild);

			if (currentChild.transform.position.x <= -15.0f)
			{
				Destroy (currentChild);
			}
		}

	}

	void ScrollChallenge(GameObject currentChallenge)
	{
		currentChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
	}

	void GenerateRandomChallenge()
	{

		GameObject newChallenge =  Instantiate (challenges [Random.Range (0, challenges.Length)], challengesSpawnPoint.position, Quaternion.identity) as GameObject;

		newChallenge.transform.parent = transform;

		counter = 1.0f;
	}

	public void GameOver()
	{
		isGameOver = true;
		//gameObject.SendMessage ("GameOver", SendMessageOptions.RequireReceiver);
		transform.GetComponent<GameController>().GameOver();
	}
		
}
