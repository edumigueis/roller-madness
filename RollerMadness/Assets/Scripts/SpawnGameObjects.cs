using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour {

	public GameObject spawnPrefab;

	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;
	
	public Transform chaseTarget;
	
	private float savedTime;
	private float secondsBetweenSpawning;

	// Use this for initialization
	void Start () {
		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
		{
			MakeThingToSpawn();
			savedTime = Time.time; // store for next spawn
			secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		}	
	}

	void MakeThingToSpawn()
	{
		string playerDifficulty = PlayerPrefs.GetString("Difficulty");
		// create a new gameObject
		GameObject clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;

		// set chaseTarget if specified
		if ((chaseTarget != null) && (clone.gameObject.GetComponent<Chaser> () != null))
		{
			switch(playerDifficulty)
			{
				case "Rookie":
					clone.gameObject.GetComponent<Chaser>().SetSpeed(2f);
					break;
				case "Easy":
					clone.gameObject.GetComponent<Chaser>().SetSpeed(3.5f);
					break;
				case "Normal":
					clone.gameObject.GetComponent<Chaser>().SetSpeed(5f);
					break;
				case "Hard":
					clone.gameObject.GetComponent<Chaser>().SetSpeed(6f);
					break;
			}
			clone.gameObject.GetComponent<Chaser>().SetTarget(chaseTarget);
		}
	}
}
