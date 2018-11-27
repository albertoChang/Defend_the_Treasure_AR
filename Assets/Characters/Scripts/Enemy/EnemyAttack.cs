using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    
    public float timeBetweenAttacks;
    public int attackDamage;
    GameObject tower;
    Tower _tower;

    Animator anim;
    private bool towerInRange;
    float timer;
    
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        tower = GameObject.FindGameObjectWithTag("Tower");
        _tower = tower.GetComponent<Tower>();
    }

    public void FoundTower(bool found)
    {
        towerInRange = found;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        
        if (timer>= timeBetweenAttacks && towerInRange)
        {
            Attack();
        }
        
	}

    private void Attack()
    {
        timer = 0f;
        
        _tower.TakeDamage(attackDamage);
    }
}
