using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MoveBehaviour))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] Material slowEffectMaterial;

    [Header("Behaviours")]
    [SerializeField] MoveBehaviour moveBehaviour;

    [Header("Properties")]
    [SerializeField] HPModel hpModel;
    [SerializeField] float speed = 7;
    [SerializeField] public int money = 0;

    [Header("Events")]
    public static System.Action<GameObject> onTargetReached;
    public static System.Action onLastEnemyDied;

    List<Renderer> renderers;

    public static List<GameObject> Enemies { get; private set; } = new List<GameObject>();

    private void Awake()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
        hpModel = GetComponent<HPModel>();

        renderers = GetComponentsInChildren<Renderer>().ToList();
    }

    private void Start()
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.SetSpeed(speed);
        }
    }

    private void OnEnable()
    {
        hpModel.onDeath += Die;
        hpModel.onDeath += DropMoney;
    }

    private void OnDisable()
    {
        hpModel.onDeath -= Die;
        hpModel.onDeath -= DropMoney;
    }

    private void FixedUpdate()
    {
        if(moveBehaviour == null)
        {
            return;
        }

        if (moveBehaviour.IsTargetReached())
        {
            onTargetReached?.Invoke(gameObject);
            Die();
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.SetTargetPosition(targetPosition);
        }
    }

    public void Die()
    {
        //Remove enemy from scene
        Enemies.Remove(gameObject);

        if (WaveManager.instance.isLastWaveSpawned() && Enemies.Count == 0)
        {
            onLastEnemyDied.Invoke();
        }

        Destroy(gameObject);
    }

    void DropMoney()
    {
        //Get money from enemy death
        MoneyManager.instance.GainMoney(money);

        MoneyDropManager.instance.ShowMoney(this);
    }

    public static void AddEnemy(GameObject enemy)
    {
        Enemies.Add(enemy);
    }

    public void ApplySlow(float slowAmount, float slowDuration)
    {
        slowAmount = speed * (slowAmount / 100);

        if (moveBehaviour != null)
        {
            moveBehaviour.SetSpeed(speed - slowAmount);
            ApplySlowEffectMaterial();
            StopAllCoroutines();
            StartCoroutine(SlowCoroutine(slowDuration));
        }
    }

    IEnumerator SlowCoroutine(float slowDuration)
    {
        yield return new WaitForSeconds(slowDuration);

        moveBehaviour.SetSpeed(speed);
        RemoveSlowEffectMaterial();
        yield break;
    }

    void ApplySlowEffectMaterial()
    {
        if(slowEffectMaterial == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Count; i++)
        {
            //Create a copy of the materials and add the shader material
            List<Material> matArray = renderers[i].sharedMaterials.ToList();

            if (matArray[matArray.Count - 1] != slowEffectMaterial)
            {
                matArray.Add(slowEffectMaterial);
            }

            //Assign the new material list
            renderers[i].materials = matArray.ToArray();
        }
    }

    void RemoveSlowEffectMaterial()
    {
        if (slowEffectMaterial == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Count; i++)
        {
            if(renderers[i].materials.Count() > 1)
            {
                //Create a copy of the materials and remove the last material
                List<Material> matArray = renderers[i].materials.ToList();
                matArray.RemoveAt(matArray.Count - 1);

                Destroy(renderers[i].materials[renderers[i].materials.Count() - 1]);

                //Assign the new material list
                renderers[i].materials = matArray.ToArray();
            }
        }
    }
}
