using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureDamage : MonoBehaviour {

	public int value = 10;
	public GameObject explosionPrefab;

    public float speed;
	public GameObject treasure;
	public GameObject boss;

    private bool isColetado = false;

	void Update()
    {
		if (isColetado)
		{
			// Calculate direction vector.
			Vector3 direction = treasure.transform.position - boss.transform.position;

			// Normalize resultant vector to unit Vector.
			direction = -direction.normalized;

			// Move in the direction of the direction vector every frame.
			treasure.transform.position += direction * Time.deltaTime * speed;
		}
    }

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			if (GameManager.gm!=null)
			{
				// tell the game manager to Collect
				GameManager.gm.Collect (value);
			}
			
			isColetado = true;
		}

        // A
        if (other.gameObject.tag == "Boss") {
			// A
            if (other.gameObject.GetComponent<Health> () != null)
				other.gameObject.GetComponent<Health> ().ApplyDamage (value);

			// explode if specified
			if (explosionPrefab != null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}
			
			// destroy after collection
			Destroy (gameObject);
		}
	}
}

