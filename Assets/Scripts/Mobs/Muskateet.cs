using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Muskateet : MonoBehaviour
{

    public float SearchRange;
    private LivingEntity le;

    private GameObject CurrentTarget;

    private float _timeSinceLastShoot = 0f;
    public float TimeBetweenShoots = 1f;
    public float LaunchVelocity = 2f;
    public float OffsetDown = 2.3f;
    public Animator AnimController;

    public bool ShouldShoot = false;
    private float TimeStartShoot = 0f;

    public GameObject BulletPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set our variables related to living entity
        le = gameObject.GetComponent<LivingEntity>();
        le.Health = 5f; // A Muskateet starts at 5 health
        le.Entity_Type = LivingEntity.Type.MONSTER;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldShoot)
        {
            if ((Time.time - TimeStartShoot) < 0.1f) return;
            GameObject bp = Instantiate(BulletPrefab);
            bp.transform.SetPositionAndRotation(transform.position + (Vector3.left * 1.3f) + (Vector3.down * OffsetDown), transform.rotation);
            bp.GetComponent<Rigidbody2D>().AddForce(new Vector2(-LaunchVelocity, 0f));
            _timeSinceLastShoot = 0f;
            ShouldShoot = false;
            return;
        }
        
        _timeSinceLastShoot += Time.deltaTime;
        if (CurrentTarget == null || Vector2.Distance(CurrentTarget.transform.position, gameObject.transform.position) >
            SearchRange)
        {
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach(Object o in objects)
            {
                GameObject go = (GameObject) o;
                if (go.GetComponent<LivingEntity>() != null)
                {
                    LivingEntity le = go.GetComponent<LivingEntity>();
                    if (le.Entity_Type == LivingEntity.Type.FRIENDLY)
                    {
                        // This is a targeatable thing, so let's target it
                        CurrentTarget = go;
                        return;
                    }
                }
            }
        }

        if (CurrentTarget != null)
        {
            // Can we shoot?
            if (_timeSinceLastShoot > TimeBetweenShoots)
            {
                // Fire away captain
                ShouldShoot = true;
                AnimController.SetTrigger("Shoot");
                TimeStartShoot = Time.time;
            }
        }
    }
}
