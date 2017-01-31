using UnityEngine;
using Spaces.Selection;

namespace Spaces.Gizmos
{
	public class TranslateGizmo : Gizmo
	{
		private float translateAmount;
		private float initialPosition;

		public override void Draw<T>(Transform root, GizmoType GizmoType, T type)
		{
			base.Draw(root, GizmoType, type);
			SetLocalPosition(new Vector3(0, 0.6f, 0));
			SetOrientation(root.rotation);
			SetScale(Vector3.one);
		}

		public override void OnDrag()
		{
			translateAmount += Input.GetAxis("Mouse Y") * mouseSensitiveValue * Time.deltaTime;
			translateAmount = Mathf.Clamp(translateAmount, GetInputRange().min, GetInputRange().max);

			var translation = new Vector3(SelectionHandler.instance.CurrentSelection.transform.localPosition.x, translateAmount, 0);
			SelectionHandler.instance.CurrentSelection.gameObject.transform.localPosition = translation;
			transform.localPosition = translation;
		}

		public override void OnPointerDown()
		{
			SetInputRange(0f, 5.0f);
			initialPosition = SelectionHandler.instance.CurrentSelection.transform.localPosition.y;
			translateAmount = initialPosition;
		}

	}
}