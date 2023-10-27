using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class Effect : ScriptableObject
{
    public string name;
    public int effectID;

    [TextArea(4, 5)]
    public string description;

    public float speedModifier;
    public float jumpModifier;
    public int healthModifer;
    public float regenModifer;
    public float staminaModifier;

    public float degenModifier;
    public float slownessModifier;






}
