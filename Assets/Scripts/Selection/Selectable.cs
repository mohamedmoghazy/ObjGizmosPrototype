using UnityEngine;
using Spaces.Controls;

namespace Spaces.Selection
{
	public class Selectable : PointerInputModule, ISelectable
	{
		private Color _mainColor;
		private Color _highlightColor;
		private bool _canSelect = true;

		protected Vector2 objectPositionOnScreen;

		public bool canSelect
		{
			get { return _canSelect; }
			set { _canSelect = value; }
		}

		public Color HighLightColor
		{
			get { return _highlightColor; }
			set { _highlightColor = value; }
		}

		public Color MainColor
		{
			get { return _mainColor; }
			set { _mainColor = value; }
		}


		public virtual void OnSelect()
		{
		}

		public virtual void OnDeselect()
		{
		}

		public override void OnPointerOver()
		{
			ChangeColor(HighLightColor);
		}

		public override void OnPointerExit()
		{
			ChangeColor(_mainColor);
		}

		public override void OnDrag()
		{
			//Get the Screen positions of the object
			base.OnDrag();
			objectPositionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
		}

		private void ChangeColor(Color color)
		{
			Debug.Log("Change Color");
			gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, gameObject.GetComponent<Renderer>().material.color.a);
		}

	}
}
