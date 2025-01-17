﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public enum deathAction {loadLevelWhenDead,doNothingWhenDead};
	
	// A
	public HealthBar healthBar;

	public float healthPoints = 1f;
	private float totalHealth;
	public float respawnHealthPoints = 1f;		//base health points
	
	public int numberOfLives = 1;					//lives and variables for respawning
	public bool isAlive = true;	

	public GameObject explosionPrefab;
	
	public deathAction onLivesGone = deathAction.doNothingWhenDead;
	
	public string LevelToLoad = "";
	
	private Vector3 respawnPosition;
	private Quaternion respawnRotation;
	

	// Use this for initialization
	void Start () 
	{
		// store initial position as respawn location
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;
		
		if (LevelToLoad=="") // default to current scene 
		{
			LevelToLoad = Application.loadedLevelName;
		}

		if (healthBar != null)
			healthBar.SetMaxHealth(healthPoints);
		totalHealth = healthPoints;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (healthBar != null)
			healthBar.SetMax(totalHealth);
		if (healthPoints <= 0) {				// if the object is 'dead'
			numberOfLives--;					// decrement # of lives, update lives GUI
			
			if (explosionPrefab!=null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}
			
			if (numberOfLives > 0) { // respawn
				transform.position = respawnPosition;	// reset the player to respawn position
				transform.rotation = respawnRotation;
				healthPoints = respawnHealthPoints;	// give the player full health again
			} else { // here is where you do stuff once ALL lives are gone)
				isAlive = false;
				
				switch(onLivesGone)
				{
				case deathAction.loadLevelWhenDead:
					Application.LoadLevel (LevelToLoad);
					break;
				case deathAction.doNothingWhenDead:
					// do nothing, death must be handled in another way elsewhere
					break;
				}
				Destroy(gameObject);

				if (healthBar != null)
					healthBar.EraseHealthBar();
			}
		}
	}
	
	public void ApplyDamage(float amount)
	{	
		healthPoints = healthPoints - amount;	

		// A
		if (healthBar != null) {
			healthBar.SetHealth(healthPoints);
			healthBar.SetMax(totalHealth);
		}
	}
	
	public void ApplyHeal(float amount)
	{
		healthPoints = healthPoints + amount;

		// A
		if (healthBar != null) {
			healthBar.SetHealth(healthPoints);
			healthBar.SetMax(totalHealth);
		}
	}

	public void ApplyBonusLife(int amount)
	{
		numberOfLives = numberOfLives + amount;
	}
	
	public void updateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation) {
		respawnPosition = newRespawnPosition;
		respawnRotation = newRespawnRotation;
	}
}
