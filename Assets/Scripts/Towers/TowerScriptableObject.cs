using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower/Create New Tower")]
public class Tower : ScriptableObject
{
    public string towerName;
    public string attackType;
    public string targetingType;
    public GameObject upgradePrefab;
}
