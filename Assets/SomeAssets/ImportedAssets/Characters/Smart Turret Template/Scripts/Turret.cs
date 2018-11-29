using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TurretParameters {

	[Header("Status")]
	[Tooltip("Activate or deactivate the Turret")]
	public bool active;
	public bool canFire;

	[Header("Shooting")]
	[Tooltip("Burst the force when hit")]
	public float power;
	[Tooltip("Pause between shooting")]
	[Range(0.1f,1)]
	public float ShootingDelay;
	[Tooltip("Radius of the turret view")]
	public float radius;
}

[System.Serializable]
public class TurretFX {

	[Tooltip("Muzzle transform position")]
	public Transform muzzle;
	[Tooltip("Spawn this GameObject when shooting")]
	public GameObject shotFX;
}
	
[System.Serializable]
public class TurretAudio {
	
	public AudioClip shotClip;
}

[System.Serializable]
public class TurretTargeting {

	[Tooltip("Speed of aiming at the target")]
	public float aimingSpeed;
	[Tooltip("Pause before the aiming")]
	public float aimingDelay;
	[Tooltip("GameObject with folowing tags will be identify as enemy")]
	public string[] tagsToFire;
	public List<Collider> targets = new List<Collider>();
	public Collider target;
}
	
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class Turret : MonoBehaviour {

	public TurretParameters parameters;
	public TurretTargeting targeting;
	public TurretFX VFX;
	public TurretAudio SFX;

    public Camera arcam;
    public LayerMask layer_mask;
    public bool buttonShot;

    private void Awake() {
		
		GetComponent<SphereCollider> ().isTrigger = true;
		GetComponent<SphereCollider> ().radius = parameters.radius;
		GetComponent<BoxCollider> ().size =  new Vector3(2,2,2);
		GetComponent<BoxCollider> ().center =  new Vector3(0,1,0);
	}

    public void setActiveShot()
    {
        buttonShot = true;
    }

    public void setDeactiveShot()
    {
        buttonShot = false;
    }

    private void FixedUpdate () {
        
		if (parameters.active == false) {
			return;
		}

		if (targeting.target == null) {
			ClearTargets ();
		}
					
		Aiming ();
        if (buttonShot)
        {
            Invoke("Shooting", parameters.ShootingDelay);
        }
	}

	#region Aiming and Shooting

	private void Shot() {

		GetComponent<AudioSource> ().PlayOneShot (SFX.shotClip, Random.Range(0.25f,0.75f));
		GetComponent<Animator> ().SetTrigger ("Shot");
		GameObject newShotFX = Instantiate (VFX.shotFX, VFX.muzzle);
		Destroy (newShotFX, 2);
	}
		
	private void Shooting() {

		if (parameters.canFire == false) {
			return;
		}
			
		RaycastHit hit;
        //Debug.DrawRay(VFX.muzzle.position, VFX.muzzle.transform.forward * parameters.radius, Color.black);
        if (Physics.Raycast (VFX.muzzle.position, VFX.muzzle.transform.forward, out hit, parameters.radius, layer_mask)) {

            Shot();
            if (CheckTags (hit.collider) == true) {
				hit.collider.GetComponent<EnemyHealth> ().ReceiveDamage (parameters.power, hit.point);
			}

			ClearTargets ();	
			CancelInvoke ();
		}
	}

	public void Aiming()
    {
        RaycastHit hit;
        float distance = 10000;
        //Debug.DrawRay(arcam.transform.position, arcam.transform.forward * distance, Color.yellow);

        if (Physics.Raycast(arcam.transform.position, arcam.transform.forward, out hit, distance, layer_mask))
        {
            transform.LookAt(hit.point);
            VFX.muzzle.LookAt(hit.point);            
        }
        
	}

	#endregion

	#region Targeting
		
	private void OnTriggerEnter(Collider other) {

		if (parameters.active == false) {
			return;
		}
        //Debug.Log("Entre al trigger");
		ClearTargets ();

		if (CheckTags (other) == true) {
			if (targeting.targets.Count == 0) {
				targeting.target = other.GetComponent<Collider> ();
			}

			targeting.targets.Add (other.GetComponent<Collider> ());
		}
	}

	private void OnTriggerExit(Collider other) {

		if (parameters.active == false) {
			return;
		}

		ClearTargets ();

		if (CheckTags (other) == true) {
			targeting.targets.Remove (other.GetComponent<Collider>());
			if (targeting.targets.Count != 0) {
				targeting.target = targeting.targets.First ();
			} else {
				targeting.target = null;
			}
		}
	}

	private bool CheckTags (Collider toMatch) {

		bool Match = false;

		for (int i = 0; i < targeting.tagsToFire.Length; i++) {
			if (toMatch.tag == targeting.tagsToFire [i]) {
				Match = true;
			}
		}

		return(Match);
	}

	private void ClearTargets () {

		if (targeting.target != null) {
			if (targeting.target.GetComponent<Collider> ().enabled == false) {
				targeting.targets.Remove (targeting.target);
			}
		}

		foreach (Collider target in targeting.targets.ToList()) {

			if (target == null) {
				targeting.targets.Remove (target);
			}

			if (targeting.targets.Count != 0) {
				targeting.target = targeting.targets.First ();
			} else {
				targeting.target = null;
			}
		}
	}

	#endregion
}