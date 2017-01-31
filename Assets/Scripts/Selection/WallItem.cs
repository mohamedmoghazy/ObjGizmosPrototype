using UnityEngine;
using System.Collections;
using Spaces.Gizmos;

namespace Spaces.Selection
{
	public class WallItem : SceneItem
	{
		public override void OnSelect()
		{
			base.OnSelect();

			//initialize the Rotation Gizmo
			RotationGizmo rotationGizmo = new RotationGizmo();
			rotationGizmo.Draw(bottomAnchor.transform, GizmoType.Rectangle,rotationGizmo);

			//initialize the Scale Left Gizmo
			WallScaleGizmo wallScaleGizmoleft = new WallScaleGizmo();
			wallScaleGizmoleft.Draw(leftAnchor.transform, GizmoType.LeftScale, wallScaleGizmoleft);

			//initialize the Scale Right Gizmo
			WallScaleGizmo wallScaleGizmoRight = new WallScaleGizmo();
			wallScaleGizmoRight.Draw(rightAnchor.transform, GizmoType.RightScale, wallScaleGizmoRight);

			//initialize the Scale Up Gizmo
			WallScaleGizmo wallScaleGizmoUp = new WallScaleGizmo();
			wallScaleGizmoUp.Draw(topAnchor.transform, GizmoType.UpperScale, wallScaleGizmoUp);

		}


		public override void OnDeselect()
		{
			base.OnDeselect();
		}
	}
}
