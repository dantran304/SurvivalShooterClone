using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public int startingHealth = 20;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;

    public AudioClip deathClip;
    AudioSource enemyAudio;

    Animator anim;
    ParticleSystem hitParticle;
    CapsuleCollider capsuleCollider;

    bool isDead;
    bool isSinking;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    public void TakeDamg (int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }
        enemyAudio.Play();
        currentHealth -= amount;
        hitParticle.transform.position = hitPoint;
        hitParticle.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("isDead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;

        //ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
	}
}
