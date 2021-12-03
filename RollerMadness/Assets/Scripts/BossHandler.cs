using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;

    public Material bossFullLife;
    public Material bossHalfLife;
    public Material bossDying;
    public Material bossDead;
    public Material bossAttacking;

    public AudioSource bossBattleBegin;
    public AudioClip spawnEnemiesSound;
    public AudioClip powerAttackSound;
    public AudioClip bossHit1;
    public AudioClip bossHit2;


    private float totalHealth;
    private float currentHealth;

    private bool attacking = false;
    private bool spawning = false;
    private bool powerAttacking = false;
    private float timePassedSpawn = 0f;
    private float timePassedPower = 0f;

    void Start()
    {
        totalHealth   = boss.GetComponent<Health>().healthPoints;
        currentHealth = boss.GetComponent<Health>().healthPoints;
        bossBattleBegin.Play();
    }

    void Update()
    {
        if (currentHealth != boss.GetComponent<Health>().healthPoints)
        {
            // A
            int hit1or2 = Random.Range(1, 10);
            if (hit1or2 <= 5) {
                bossBattleBegin.clip = bossHit1;
                bossBattleBegin.Play();
            }
            else {
                bossBattleBegin.clip = bossHit2;
                bossBattleBegin.Play();
            }

            if (currentHealth <= totalHealth)
                boss.GetComponent<MeshRenderer>().material = bossFullLife;
            if (currentHealth <= (totalHealth * 0.75f))
                boss.GetComponent<MeshRenderer>().material = bossHalfLife;
            if (currentHealth <= (totalHealth * 0.45f))
                boss.GetComponent<MeshRenderer>().material = bossDying;
            if (currentHealth <= (totalHealth * 0.20f))
                boss.GetComponent<MeshRenderer>().material = bossDead;
            
            currentHealth = boss.GetComponent<Health>().healthPoints;
        }
        if (attacking)
            boss.GetComponent<MeshRenderer>().material = bossAttacking;

        timePassedSpawn += Time.deltaTime;
        timePassedPower += Time.deltaTime;
        if(timePassedSpawn > 15f)
        {
            SpawnEnemiesAttack();
            timePassedSpawn -= 15f;
        }
        if(timePassedSpawn > 3f && spawning)
            StopSpawnEnemies();

        if(timePassedPower > 25f)
        {
            PowerAttack();
            timePassedPower -= 25f;
        }
        if(timePassedPower > 3f && powerAttacking)
            PowerAttackFinish();

        // Olhando para o personagem
        Vector3 dir = player.transform.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        lookRot.x = 0; lookRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(5f * Time.deltaTime));

    }

    private void SpawnEnemiesAttack()
    {
        transform.Find("SpawnerInimigos").gameObject.SetActive(true);
        transform.Find("SpawnerInimigos1").gameObject.SetActive(true);
        transform.Find("SpawnerInimigos2").gameObject.SetActive(true);
        spawning = true;
        attacking = true;
        bossBattleBegin.clip = spawnEnemiesSound;
        bossBattleBegin.Play();
    }
    private void StopSpawnEnemies()
    {
        transform.Find("SpawnerInimigos").gameObject.SetActive(false);
        transform.Find("SpawnerInimigos1").gameObject.SetActive(false);
        transform.Find("SpawnerInimigos2").gameObject.SetActive(false);  
        spawning = false; 
        attacking = false;
    }
    private void PowerAttack()
    {
        transform.Find("SpawnerPower").gameObject.SetActive(true);
        powerAttacking = true;
        attacking = true;
        bossBattleBegin.clip = powerAttackSound;
        bossBattleBegin.Play();
    }
    private void PowerAttackFinish()
    {
        transform.Find("SpawnerPower").gameObject.SetActive(false);
        powerAttacking = false;
        attacking = false;
    }
}
