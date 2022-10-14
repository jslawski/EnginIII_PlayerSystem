using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
public class Weapon : Equipment
{
    public Vector2 attackZoneDimensions;
    public float windUpTime;   
    public Vector2Int attackChanceRange;
    public int attackPower;
    public float critChance;
}
