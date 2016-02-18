using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    public float timeBetweenAttack = 0.5f;
    public int attackDamg = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= timeBetweenAttack && playerInRange && enemyHealth.currentHealth >0)
        {
            Attack();
        }
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("isIdle");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    public void Attack()
    {
        Debug.Log("Attack player");
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamg(attackDamg);
        }

    }
}
