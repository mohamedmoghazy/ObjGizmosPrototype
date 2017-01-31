using UnityEngine;
using Spaces.Selection;

namespace Spaces.Gizmos
{
	public class WallScaleGizmo : Gizmo
	{
		private float scaleAmount;
		private float initialPosition;
		private float initialScale;
		private float initialGizmoPosition;
		private Transform anchor;

		public override void Draw<T>(Transform root, GizmoType GizmoType, T type)
		{
			base.Draw(root, GizmoType, type);
			switch (GizmoType)
			{
				case GizmoType.LeftScale:
					SetLocalPosition(new Vector3(SelectionHandler.instance.CurrentSelection.transform.localPosition.x -
												 (SelectionHandler.instance.CurrentSelection.transform.localScale.x / 2), SelectionHandler.instance.CurrentSelection.transform.localScale.y / 2));
					break;

				case GizmoType.RightScale:
					SetLocalPosition(new Vector3(SelectionHandler.instance.CurrentSelection.transform.localPosition.x +
												 (SelectionHandler.instance.CurrentSelection.transform.localScale.x / 2), SelectionHandler.instance.CurrentSelection.transform.localScale.y / 2));
					break;

				case GizmoType.UpperScale:
					SetLocalPosition(new Vector3(0, 0.1f, 0));
					break;

			}
			SetOrientation(root.rotation);
			SetScale(Vector3.one);
		}

		public override void OnPointerDown()
		{
			initialScale = SelectionHandler.instance.CurrentSelection.transform.localScale.x;
			initialPosition = SelectionHandler.instance.CurrentSelection.transform.parent.localPosition.x;
			initialGizmoPosition = transform.parent.localPosition.x;
			scaleAmount = 0;
			if (gameObject.transform.parent.tag == "UpperScale")
			{
				scaleAmount = SelectionHandler.instance.CurrentSelection.transform.localScale.y;
				//initialGizmoPosition = SelectionHandler.instance.CurrentSelection.transform.localScale.y;

			}
		}

		public override void OnDrag()
		{

			switch (gameObject.transform.parent.tag)
			{
				case "LeftScale":
					//scale the mesh using the scale amount value
					scaleAmount += Input.GetAxis("Mouse X") * mouseSensitiveValue * Time.deltaTime;
					//scaleAmount = Mathf.Clamp(scaleAmount, -20, 0);
					SelectionHandler.instance.CurrentSelection.transform.localScale = new Vector3(initialScale - scaleAmount, SelectionHandler.instance.CurrentSelection.transform.localScale.y, SelectionHandler.instance.CurrentSelection.transform.localScale.z);
					anchor = transform.root.FindChild("rightAnchor").GetChild(0);
					HorizontalScaleAlignment();
					break;
				case "RightScale":
					//scaleAmount = Mathf.Clamp(scaleAmount, 0, 20);
					scaleAmount += Input.GetAxis("Mouse X") * mouseSensitiveValue * Time.deltaTime;
					SelectionHandler.instance.CurrentSelection.transform.localScale = new Vector3(initialScale + scaleAmount, SelectionHandler.instance.CurrentSelection.transform.localScale.y, SelectionHandler.instance.CurrentSelection.transform.localScale.z);
					anchor = transform.root.FindChild("leftAnchor").GetChild(0);
					HorizontalScaleAlignment();
					break;

				case "UpperScale":
					scaleAmount += Input.GetAxis("Mouse Y") * mouseSensitiveValue * Time.deltaTime;
					scaleAmount = Mathf.Clamp(scaleAmount, 1, 10);
					SelectionHandler.instance.CurrentSelection.transform.localScale = new Vector3(SelectionHandler.instance.CurrentSelection.transform.localScale.x, scaleAmount, SelectionHandler.instance.CurrentSelection.transform.localScale.z);
					anchor = transform.root.FindChild("topAnchor");
					VerticalScaleAlignment();
					break;
			}


		}

		private void HorizontalScaleAlignment()
		{
			//move the gizmo using the scale amount value
			transform.parent.localPosition = new Vector3(initialGizmoPosition + scaleAmount / 2, transform.parent.localPosition.y, transform.parent.localPosition.z);

			//move the opposite Anchor
			anchor.localPosition = new Vector3(-transform.parent.localPosition.x, anchor.localPosition.y, anchor.localPosition.z);

			//shift the mesh 
			SelectionHandler.instance.CurrentSelection.transform.parent.localPosition = new Vector3(initialPosition + scaleAmount / 2 * SelectionHandler.instance.CurrentSelection.transform.parent.localScale.x, SelectionHandler.instance.CurrentSelection.transform.parent.localPosition.y, SelectionHandler.instance.CurrentSelection.transform.parent.localPosition.z);

			//shift the Gizmos Root to adopt the mesh position
			transform.root.position = SelectionHandler.instance.CurrentSelection.transform.position;

			//Scale and move the Buttom Anchor
			transform.root.FindChild("bottomAnchor").localScale = new Vector3(SelectionHandler.instance.CurrentSelection.transform.localScale.x + 0.2f, transform.root.FindChild("bottomAnchor").localScale.y, transform.root.FindChild("bottomAnchor").localScale.z);

		}

		private void VerticalScaleAlignment()
		{
			anchor.localPosition = new Vector3(anchor.localPosition.x, scaleAmount);
			transform.root.FindChild("leftAnchor").GetChild(0).localPosition = new Vector3(transform.root.FindChild("leftAnchor").GetChild(0).localPosition.x, scaleAmount / 2);
			transform.root.FindChild("rightAnchor").GetChild(0).localPosition = new Vector3(transform.root.FindChild("rightAnchor").GetChild(0).localPosition.x, scaleAmount / 2);
		}

	}
}
