using UnityEngine;
using Spaces.Selection;

namespace Spaces.Gizmos
{
	public class ScaleGizmo : Gizmo
	{
		private float scaleAmount;
		private float initialScale;

		public override void Draw<T>(Transform root, GizmoType GizmoType, T type)
		{
			base.Draw(root, GizmoType, type);
			SetLocalPosition(new Vector3(0, 0.3f, 0));
			SetOrientation(root.rotation);
			SetScale(Vector3.one);
		}

		public override void OnDrag()
		{
			scaleAmount += Input.GetAxis("Mouse Y") * mouseSensitiveValue * Time.deltaTime;
			scaleAmount = Mathf.Clamp(scaleAmount, GetInputRange().min, GetInputRange().max);

			SelectionHandler.instance.CurrentSelection.transform.parent.localScale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
			transform.root.localScale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
		}

		public override void OnPointerDown()
		{
			SetInputRange(0.1f, 5.0f);
			initialScale = SelectionHandler.instance.CurrentSelection.transform.parent.localScale.x;
			scaleAmount = initialScale;
		}
	}
}