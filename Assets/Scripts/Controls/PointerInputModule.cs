using UnityEngine;

namespace Spaces.Controls
{
	public class PointerInputModule : MonoBehaviour
	{
		protected Vector2 mousePositionOnScreen;
		protected Ray mouseScreenPointToRay;
		protected float mouseSensitiveValue = 10.0f;
		private RangeAttribute _inputRange;

		public virtual void OnDrag()
		{
			mousePositionOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			mouseScreenPointToRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		}

		public virtual void OnPointerUp()
		{

		}

		public virtual void OnPointerDown()
		{

		}

		public virtual void OnPointerOver()
		{

		}

		public virtual void OnPointerExit()
		{

		}

		public virtual void OnSceneItemTriggerEnter(Collider col)
		{

		}

		public virtual void OnSceneItemTriggerExit(Collider col)
		{

		}

		public void SetInputRange(float min, float max)
		{
			_inputRange = new RangeAttribute(min, max);

		}

		public RangeAttribute GetInputRange()
		{
			return _inputRange;

		}

		void OnMouseUp()
		{
			OnPointerUp();
		}

		void OnMouseDown()
		{
			OnPointerDown();
		}

		void OnMouseDrag()
		{
			OnDrag();
		}

		void OnMouseOver()
		{
			OnPointerOver();
		}

		void OnMouseExit()
		{
			OnPointerExit();
		}

		void OnTriggerEnter(Collider col)
		{
			OnSceneItemTriggerEnter(col);
		}

		void OnTriggerExit(Collider col)
		{
			OnSceneItemTriggerExit(col);
		}


	}
}
