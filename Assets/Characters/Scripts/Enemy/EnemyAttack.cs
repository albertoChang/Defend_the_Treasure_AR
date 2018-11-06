using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public GameObject tower;

    Animator anim;
    //TowerHealth towerhealth;
    //EnemyHealth enemyhealth;
    bool towerInRange;
    float timer;

	// Use this for initialization
	void Start () {
        //towerhealth = tower.GetComponent<TowerHealth>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tower)
        {
            anim.SetTrigger("Collision");
            towerInRange = true;
        }
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (timer>= timeBetweenAttacks && towerInRange)
        {
            //Attack();
        }

	}
}
