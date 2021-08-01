using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * The following code is written poorly in terms of performance.
 * Find the all problems, critical or micro-optimization, and mark them. Suggest fixes if you can.
 */

public class Optimization : MonoBehaviour
{
	public float speed = 1f;

	private List<MeshFilter> meshFilters = new List<MeshFilter>();

	private void Start()
	{
		//meshFilters.AddRange(FindObjectsOfType<MeshFilter>().ToList());
		meshFilters = FindObjectsOfType<MeshFilter>().ToList();
	}

	private void Update()
	{
		foreach (var meshFilter in meshFilters)
		{
			if (meshFilter.GetComponent<MeshCollider>())
			{
				Mesh mesh = meshFilter.mesh;
				Vector3[] verticesOfTheMesh = mesh.vertices;
				for (var i = 0; i < verticesOfTheMesh.Length; i++)
				{
					verticesOfTheMesh[i] += Vector3.up * (speed * Time.deltaTime);
				}
			}
		}
	}
}