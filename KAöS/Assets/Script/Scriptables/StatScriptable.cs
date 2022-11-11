using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModifierData
{
    public Enums.Stats stat;
    public float value;
}

[CreateAssetMenu(fileName = "New Stat Modifier", menuName = "Stat Modifier")]
public class StatScriptable : ScriptableObject
{
    public List<ModifierData> modifiers;
}
