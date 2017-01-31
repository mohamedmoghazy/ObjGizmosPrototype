using System.Collections;
using Spaces.Selection;
using UnityEngine;

namespace Spaces.Controls
{
	public class CameraController : MonoBehaviour
	{
		public float minRotX = -360.0f;
		public float maxRotX = 360.0f;

		public float minRotY = -45.0f;
		public float maxRotY = 45.0f;

		public float sensRotX = 100.0f;
		public float sensRotY = 100.0f;

		float rotationY = 0.0f;
		float rotationX = 0.0f;

		public float minPosX = -10.0f;
		public float maxPosX = 10.0f;

		public float minPosY = -19.0f;
		public float maxPosY = 9.0f;

		public float sensPosX = 50.0f;
		public float sensPosY = 50.0f;

		float positionY = -9.0f;
		float positionX = 0.0f;

		private float zoomSpeed = 1.0f;
		public float zoomMax = 12;
		public float zoomMin = -5;

		private float focusSpeed = 15.0f;


		void Update()
		{
			if (!SelectionHandler.instance.IsSelecting())
			{
				if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Space))
				{
					Move();
				}
				else if (Input.GetKey(KeyCode.Mouse0))
				{
					Rotate();
				}

			}
			else
			{
				if (Input.GetKeyDown(KeyCode.F))
				{
					StartCoroutine(CoFocusTo(SelectionHandler.instance.CurrentSelection.transform));
				}
			}

			Zoom();

		}

		/// <summary>
		/// Focus the Camera to the specefied point.
		/// </summary>
		/// <param name="point">the Point value used to calculate the focus.</param>
		public IEnumerator CoFocusTo(Transform point)
		{
			float step = focusSpeed * Time.deltaTime;
			Quaternion lastCamraRot = Camera.main.transform.parent.localRotation;
			while (Camera.main.transform.root.position != point.position || Camera.main.transform.localPosition != new Vector3(0, 2, -6f))
			{
				yield return null;
				Camera.main.transform.root.position = Vector3.MoveTowards(transform.position, point.position, step);
				var target = new Quaternion(0, 0, 0, 1);
				Camera.main.transform.parent.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 20.0f);
				Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, new Vector3(0, 2, -6f), step);
			}
			yield return null;
		}

		/// <summary>
		/// Rotate this Camera.
		/// </summary>
		public void Rotate()
		{
			rotationX += Input.GetAxis("Mouse X") * sensRotX * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * sensRotY * Time.deltaTime;
			rotationY = Mathf.Clamp(rotationY, minRotY, maxRotY);
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}

		/// <summary>
		/// Move this Camera.
		/// </summary>
		public void Move()
		{
			positionX = Input.GetAxis("Mouse X") * sensPosX * Time.deltaTime;
			positionY = Input.GetAxis("Mouse Y") * sensPosY * Time.deltaTime;
			transform.Translate(new Vector3(-positionX, 0, -positionY));
		}

		/// <summary>
		/// Zoom this Camera.
		/// </summary>
		public void Zoom()
		{
			var scroll = Input.GetAxis("Mouse ScrollWheel");
			transform.GetChild(0).Translate(0, 0, scroll * zoomSpeed);
		}

	}
}