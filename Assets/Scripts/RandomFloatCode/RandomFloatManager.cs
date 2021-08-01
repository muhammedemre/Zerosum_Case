using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomFloatManager : MonoBehaviour
{
    public RandomFloatingNumbersScriptableObject floatingNumbersScriptableObject;
    public void GenerateTheArray()
    {
        for (int i = 0; i < floatingNumbersScriptableObject.floatArray.Length; i++)
        {
            floatingNumbersScriptableObject.floatArray[i] = Random.Range(0f, 1f);
        }
        EditorUtility.SetDirty(floatingNumbersScriptableObject);
    }

}
