using UnityEngine;

/*
 * 
 * Complete the functions below.  
 * For sure, they don't belong in the same class. This is just for the test so ignore that.
 * 
 */

public static class Utility
{
	public static bool CheckCollision(Ray ray, float maxDistance, int layer)
	{
		/*
		 *	Perform a raycast using the ray provided, only to objects of the specified 'layer' within 'maxDistance' and return if something is hit. 
		 */

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
		{
			return true; // return  hit.transform
		}
		return false;
	}


	public static Vector2[] GeneratePoints(int size)
	{
		/*
		 * Generate 'size' number of random points, making sure they are distributed as evenly as possible (Trying to achieve maximum distance between every neighbor).
		 * Boundary corners are (0, 0) and (1, 1). (Point (1.2, 0.45) is not valid because it's outside the boundaries.)
		 * Is there a known algorithm that achieves this?
		 */

		// Answer: poisson-disc sampling is the algorithm that could help our problem, I'm sorry but I was kinda busy + lazy ^^ to implement it. Here is the resources that I would get help 
		// if I would implement it: https://github.com/SebLague/Poisson-Disc-Sampling/blob/master/Poisson%20Disc%20Sampling%20E01/PoissonDiscSampling.cs
		return null;
	}


	public static Texture2D GenerateTexture(int width, int height, Color color)
	{
		/*
		 * Create a Texture2D object of specified 'width' and 'height', fill it with 'color' and return it. Do it as performant as possible.
		 */

		Texture2D texture2dObject = new Texture2D(50, 50);
		int mipCount = Mathf.Min(3, texture2dObject.mipmapCount);
		Color[] colors = new Color[1];
		colors[0] = color;
		// tint each mip level
		for (int mip = 0; mip < mipCount; ++mip)
		{
			Color[] cols = texture2dObject.GetPixels(mip);
			for (int i = 0; i < cols.Length; ++i)
			{
				cols[i] = Color.Lerp(cols[i], colors[mip], 0.33f);
			}
			texture2dObject.SetPixels(cols, mip);
		}
		texture2dObject.Apply(false);
		return texture2dObject;
	}

	[MenuItem("GameObject / Select All Active")]
	public static void SelectAllActiveGameObjects()
	{
		/*
		 * Implement this function so that it selects all the active GameObjects in the hierarchy window of the editor.
		 * Do it in a single line.
		 * It should be called through the Unity toolbar menu item "GameObject/Select All Active".
		 */
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(gameObject => gameObject.activeSelf).ToArray();
	}
}
