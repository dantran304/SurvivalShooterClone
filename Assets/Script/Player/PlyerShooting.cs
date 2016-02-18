using UnityEngine;
using System.Collections;

public class PlyerShooting : MonoBehaviour {
    public int damagePerShot = 20;
    public float timeBetweenBullet = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectDisplayTime = 0.2f;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponentInChildren<ParticleSystem>();
        gunLine = GetComponentInChildren<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponentInChildren<Light>();
    }

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(Input.GetButton("Fire1") && timer >= timeBetweenBullet){
            Shoot();
        }

        if (timer >= timeBetweenBullet * effectDisplayTime)
        {
            DisableEffects();
        }
	}

    void DisableEffects()
    {
        gunLight.enabled = false;
        gunLine.enabled = false;
    }
    void Shoot()
    {
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamg(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

}
