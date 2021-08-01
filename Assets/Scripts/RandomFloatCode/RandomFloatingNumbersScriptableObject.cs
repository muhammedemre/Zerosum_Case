using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatCollection", menuName = "Create/Random/FloatCollection", order = 0)]
public class RandomFloatingNumbersScriptableObject : ScriptableObject
{
    public float[] floatArray = new float[5];
}
