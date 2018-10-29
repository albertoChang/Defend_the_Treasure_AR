using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public GameObject tower;

    Animator anim;
    Transform target;
    NavMeshAgent nav;
    Transform cachedTransform;

	// Use this for initialization
	void Start () {
        cachedTransform = transform;
        target = cachedTransform;

        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
	}

    public void activateAgent()
    {
        anim.SetTrigger("StartRunning");
        target = tower.transform;
        nav.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tower)
        {
            nav.isStopped = true ;
        }
    }

    // Update is called once per frame
    void Update () {
        
	}
}
