using UnityEngine;
using System.Collections;

namespace Spaces.Helpers
{
	public class GizmoHelper
	{

		public static float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
		{
			return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
		}
	}
}
