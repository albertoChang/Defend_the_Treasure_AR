using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemyHealthParameters
{
    [Tooltip("Enemy's Health")]
    public float maxhealth;
    [Tooltip("Threshold of the applied force")]
    public float armor;
    public float damageFactor;
    public Image healthbar;
    public int pirate_points;
}

[System.Serializable]
public class EnemyFX
{
    [Tooltip("Spawn this GameObject when the turret is hitting")]
    public GameObject damageFX;
    [Tooltip("Spawn this GameObject when the turret is destroyed")]
    public GameObject deactivateFX;
}

/*
[System.Serializable]
public class EnemyAudio
{

    public AudioClip destroyClip;
}
*/

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour {

    public EnemyHealthParameters parameters;
    public EnemyFX VFX;
    //public EnemyAudio SFX;

    private Rigidbody rbody;
    private float health;
    Animator anim;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        health = parameters.maxhealth;
        anim = GetComponent<Animator>();
    }

    public void ReceiveDamage(float damage, Vector3 position)
    {

        if (damage <= health)
        {

            health -= (damage - parameters.armor);
            //Debug.Log("Me queda : " + health);
            //rbody.AddExplosionForce(damage * parameters.damageFactor, position, 0.25f);
            parameters.healthbar.fillAmount = health / parameters.maxhealth;

            if (VFX.damageFX != null)
            {
                GameObject newDamageFX = Instantiate(VFX.damageFX, position, Quaternion.identity);
                Destroy(newDamageFX, 3);
            }

        }
        else
        {
            // Setear trigger para que el enemigo haga una animación de muerto y desaparezca
            if (VFX.deactivateFX != null)
            {
                GameObject newDeactivateFX = Instantiate(VFX.deactivateFX, transform.position, Quaternion.identity);
                Destroy(newDeactivateFX, 3);
            }
            Score.scoreValue += parameters.pirate_points;
            anim.SetTrigger("Dead");
            Destroy();
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > parameters.armor)
        {
            ReceiveDamage(collision.relativeVelocity.magnitude, transform.position);
        }
    }

    public void Destroy()
    {

        GetComponent<Collider>().enabled = false;
        //GetComponent<Renderer>().enabled = false;
        //GetComponent<AudioSource>().PlayOneShot(SFX.destroyClip);
        Destroy(gameObject, 2);
    }
}
