using UnityEngine;
using Spaces.Helpers;
using Spaces.Selection;

namespace Spaces.Gizmos
{
	public class RotationGizmo : Gizmo
	{
		public override void Draw<T>(Transform root, GizmoType GizmoType, T type)
		{
			base.Draw(root, GizmoType, type);
			SetPosition(root.position);
			SetOrientation(root.rotation);
			SetScale(Vector3.one);
		}

		public override void OnDrag()
		{
			base.OnDrag();

			//Get the angle between the points
			float angle = GizmoHelper.AngleBetweenTwoPoints(objectPositionOnScreen, mousePositionOnScreen);

			var rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
			SelectionHandler.instance.CurrentSelection.gameObject.transform.parent.rotation = rotation;
			transform.root.rotation = rotation;
		}

	}
}