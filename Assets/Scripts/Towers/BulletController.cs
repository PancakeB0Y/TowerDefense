using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] AbstractBulletTravelBehaviour bulletTravelBehaviour;

    [Header("Properties")]
    [SerializeField] float travelSpeed = 10f;

    public System.Action onHit;

    GameObject target;

    private void Awake()
    {
        bulletTravelBehaviour = GetComponent<AbstractBulletTravelBehaviour>();
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        bulletTravelBehaviour.TravelTowardsTarget(target, travelSpeed);
        bulletTravelBehaviour.onTargetReached += HandleTargetReached;
    }

    public void Shoot(GameObject target)
    {
        this.target = target;
    }

    public void SetBulletSpeed(float bulletSpeed)
    {
        travelSpeed = bulletSpeed;
    }

    void HandleTargetReached()
    {
        EnemyController enemy = target.GetComponent<EnemyController>();

        if (enemy == null)
        {
            return;
        }

        onHit?.Invoke();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if (enemy == null)
        {
            return;
        }

        onHit?.Invoke();
        Destroy(gameObject);
    }
}
