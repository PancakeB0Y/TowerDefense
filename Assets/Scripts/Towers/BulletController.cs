using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float travelSpeed = 0.1f;
    public System.Action<GameObject> onHit;

    GameObject target;

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, travelSpeed);
        
    }

    public void Shoot(GameObject target)
    {
        this.target = target;
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
