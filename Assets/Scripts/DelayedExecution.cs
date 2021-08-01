using System.Collections;
using UnityEngine;

public class DelayedExecution : MonoBehaviour
{

	/*
	 * 
	 * Write the necessary code to make sure the commented line in the Start function compiles and works as intended.
	 * MyFunction should be called 2 seconds after the Delayed.Execute call.
	 * 
	 */

	private void Start()
	{
		// Delayed.Execute(MyFunction, 2f);
		StartCoroutine(MyFunctionDelayer());
	}

	IEnumerator MyFunctionDelayer()
	{
		yield return new WaitForSeconds(2f);
		MyFunction();
	}
	private void MyFunction()
	{
		Debug.Log("Executed with a delay.");
	}
}

