using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;
    ScoreBoard scoreBoard;
    GameObject parentGameObject;
    [SerializeField] AudioClip enemydistroyed;
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        audioSource = GetComponent<AudioSource>();
        parentGameObject = GameObject.FindWithTag("Spawn at Runtime");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
        
    }



    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoints --;
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void KillEnemy()
    {

        audioSource.PlayOneShot(enemydistroyed);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
