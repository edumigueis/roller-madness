using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
	public bool destroySelfOnImpact = false;
	public GameObject explosionPrefab;

    void Start()
    {
        Vector3 chasePosition = GameObject.FindWithTag ("Player").GetComponent<Transform>().position;
        transform.position = new Vector3(chasePosition.x, chasePosition.y + 9, chasePosition.z);

    }

    void Update()
    {
    }

    void OnTriggerEnter (Collider other)
	{
        if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<Health> () != null) {	// if the hit object has the Health script on it, deal damage
				other.gameObject.GetComponent<Health> ().ApplyDamage (10f);
		
				if (destroySelfOnImpact) {
					Destroy (gameObject, 1f);
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
        else if (other.gameObject.tag == "LimitesMapa")
        {
            Destroy (gameObject, 1f);
        }
    }
}
