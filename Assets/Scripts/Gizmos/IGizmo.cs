using UnityEngine;
using System.Collections.Generic;

namespace Spaces.Gizmos
{
	public interface IGizmo
	{
		void Draw<T>(Transform root, GizmoType GizmoType, T type) where T : Component;
		void SetPosition(Vector3 pos);
		void SetLocalPosition(Vector3 pos);
		void SetOrientation(Quaternion rot);
		void SetScale(Vector3 scale);
		Vector3 GetPosition();
		Quaternion GetOrientation();
		Vector3 GetScale();
	}
}
