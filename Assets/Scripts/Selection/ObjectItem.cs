using Spaces.Gizmos;

namespace Spaces.Selection
{
	public class ObjectItem : SceneItem
	{
		public override void OnSelect()
		{
			base.OnSelect();

			//initialize the Rotation Gizmo
			RotationGizmo rotationGizmo = new RotationGizmo();
			rotationGizmo.Draw(bottomAnchor.transform, GizmoType.Circle, rotationGizmo);
		}

		public override void OnDeselect()
		{
			base.OnDeselect();
		}
	}
}
