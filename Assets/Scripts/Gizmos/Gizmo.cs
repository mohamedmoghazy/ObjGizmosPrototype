using UnityEngine;
using System.Collections;
using System;
using Spaces.Selection;

namespace Spaces.Gizmos
{
	public class Gizmo : Selectable, IGizmo
	{
		/// <summary>
		/// The gizmo game Object .
		/// </summary>
		private GameObject _gizmo;

		/// <summary>
		/// Gets the orientation of the Gizmo.
		/// </summary>
		/// <returns>The orientation.</returns>
		public Quaternion GetOrientation()
		{
			return _gizmo.transform.rotation;
		}

		/// <summary>
		/// Gets the position of the Gizmo.
		/// </summary>
		/// <returns>The position.</returns>
		public Vector3 GetPosition()
		{
			return _gizmo.transform.position;
		}

		/// <summary>
		/// Gets the scale of the Gizmo.
		/// </summary>
		/// <returns>The scale.</returns>
		public Vector3 GetScale()
		{
			return transform.localScale;
		}

		void Awake()
		{
			MainColor = gameObject.GetComponent<Renderer>().material.color;
			HighLightColor = Color.yellow;
		}

		/// <summary>
		/// Draw the Gizmo. 
		/// </summary>
		/// <param name="root">The parent of the Gizmo.</param>
		/// <param name="GizmoType">The Gizmo type.</param>
		/// <param name="type">Type.</param>
		public virtual void Draw<T>(Transform root, GizmoType GizmoType , T type) where T : Component
		{
			_gizmo = Instantiate(Resources.Load("Gizmos/" + GetType().Name + GizmoType)) as GameObject;
			_gizmo.transform.GetChild(0).gameObject.AddComponent<T>();
			_gizmo.transform.SetParent(root);
			_gizmo.gameObject.tag = GizmoType.ToString();
		}

		/// <summary>
		/// Sets the orientation of the Gizmo.
		/// </summary>
		/// <param name="rot">Rot.</param>
		public void SetOrientation(Quaternion rot)
		{
			_gizmo.transform.rotation = rot;
		}

		/// <summary>
		/// Sets the position of the Gizmo.
		/// </summary>
		/// <param name="pos">Position.</param>
		public void SetPosition(Vector3 pos)
		{
			_gizmo.transform.position = pos;
		}

		/// <summary>
		/// Sets the local position of the Gizmo.
		/// </summary>
		/// <param name="pos">Position.</param>
		public void SetLocalPosition(Vector3 pos)
		{
			_gizmo.transform.localPosition = pos;
		}

		/// <summary>
		/// Sets the scale of the Gizmo.
		/// </summary>
		/// <param name="scale">Scale.</param>
		public void SetScale(Vector3 scale)
		{
			_gizmo.transform.localScale = scale;
		}

		/// <summary>
		/// Sets the parent of the Gizmo.
		/// </summary>
		/// <param name="parent">Parent.</param>
		public void SetParent(Transform parent)
		{
			_gizmo.transform.SetParent(parent);
		}

	}

	public enum GizmoType
	{
		Default,
		Circle,
		Rectangle,
		LeftScale,
		RightScale,
		UpperScale,
	}

}
