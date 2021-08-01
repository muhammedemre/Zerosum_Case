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

		return false;
	}


	public static Vector2[] GeneratePoints(int size)
	{
		/*
		 * Generate 'size' number of random points, making sure they are distributed as evenly as possible (Trying to achieve maximum distance between every neighbor).
		 * Boundary corners are (0, 0) and (1, 1). (Point (1.2, 0.45) is not valid because it's outside the boundaries.)
		 * Is there a known algorithm that achieves this?
		 */

		return null;
	}


	public static Texture2D GenerateTexture(int width, int height, Color color)
	{
		/*
		 * Create a Texture2D object of specified 'width' and 'height', fill it with 'color' and return it. Do it as performant as possible.
		 */

		return null;
	}

	public static void SelectAllActiveGameObjects()
	{
		/*
		 * Implement this function so that it selects all the active GameObjects in the hierarchy window of the editor.
		 * Do it in a single line.
		 * It should be called through the Unity toolbar menu item "GameObject/Select All Active".
		 */
	}
}
