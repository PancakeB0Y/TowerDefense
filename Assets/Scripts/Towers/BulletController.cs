using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float travelSpeed = 10f;

    public System.Action<GameObject> onHit;

    GameObject target;

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, travelSpeed * Time.deltaTime);
    }

    public void Shoot(GameObject target)
    {
        this.target = target;
    }

    public void SetBulletSpeed(float bulletSpeed)
    {
        travelSpeed = bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if(enemy == null)
        {
            return;
        }

        onHit?.Invoke(other.gameObject);
        Destroy(gameObject);
    }
}
