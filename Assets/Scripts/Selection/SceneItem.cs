using UnityEngine;
using Spaces.Gizmos;

namespace Spaces.Selection
{
	public class SceneItem : Selectable
	{
		protected GameObject gizmosRoot;
		protected GameObject topAnchor;
		protected GameObject bottomAnchor;
		protected GameObject leftAnchor;
		protected GameObject rightAnchor;

		private Vector3 initialPosition;
		private Vector3 collisionPoint;
		private Vector3 corPoint;
		private Plane movePlane;
		private float hitDist, t;

		void Awake()
		{
			MainColor = gameObject.GetComponent<Renderer>().material.color;
			HighLightColor = new Color(MainColor.r + 0.1f, MainColor.g + 0.1f, MainColor.b + 0.1f);
		}

		public override void OnSelect()
		{
			base.OnSelect();

			//Draw the Gizmos Root and and set his position.
			initialzeGizmo(ref gizmosRoot, "Gizmos", null);
			gizmosRoot.transform.position = transform.position;

			initialzeGizmo(ref topAnchor, "topAnchor", gizmosRoot.transform);
			topAnchor.transform.localPosition = new Vector3(0, transform.localScale.y, 0);


			initialzeGizmo(ref bottomAnchor, "bottomAnchor", gizmosRoot.transform);
			bottomAnchor.transform.localScale = new Vector3(transform.localScale.x + 0.2f, bottomAnchor.transform.localScale.y+ 0.2f, bottomAnchor.transform.localScale.z+ 0.2f);
			bottomAnchor.transform.localPosition = Vector3.zero;

			initialzeGizmo(ref leftAnchor, "leftAnchor", gizmosRoot.transform);
			initialzeGizmo(ref rightAnchor, "rightAnchor", gizmosRoot.transform);


			//initialize the Scale Gizmo.
			ScaleGizmo scaleGizmo = new ScaleGizmo();
			scaleGizmo.Draw(topAnchor.transform, GizmoType.Default, scaleGizmo);

			//initialize the Translation Gizmo.
			//TranslateGizmo translateGizmo = new TranslateGizmo();
			//translateGizmo.Draw(topAnchor.transform, GizmoType.Default, translateGizmo);

			gizmosRoot.transform.localScale = transform.parent.localScale;

			if (transform.gameObject.GetComponent<Collider>())
				transform.gameObject.GetComponent<Collider>().isTrigger = false;

			if (!transform.gameObject.GetComponent<Rigidbody>())
			{
				transform.gameObject.AddComponent<Rigidbody>();
				transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			}

			Spaces.UI.UIHandler.instance.ShowManipulationTool();


		}

		public override void OnDeselect()
		{
			base.OnDeselect();
			Destroy(gizmosRoot);
			Resources.UnloadUnusedAssets();

			if (transform.gameObject.GetComponent<Collider>())
				transform.gameObject.GetComponent<Collider>().isTrigger = true;

			if (transform.gameObject.GetComponent<Rigidbody>())
				Destroy(transform.gameObject.GetComponent<Rigidbody>());

			foreach (var triggeredSceneItem in SelectionHandler.instance.triggeredSceneItems)
			{
				triggeredSceneItem.GetComponent<Renderer>().material.color = new Color(triggeredSceneItem.GetComponent<Renderer>().material.color.r,
																					   triggeredSceneItem.GetComponent<Renderer>().material.color.g,
																					   triggeredSceneItem.GetComponent<Renderer>().material.color.b, 1f);
				triggeredSceneItem.canSelect = true;

			}
			SelectionHandler.instance.triggeredSceneItems.Clear();


			Spaces.UI.UIHandler.instance.HideManipulationTool();

		}


		public override void OnDrag()
		{
			if (SelectionHandler.instance.CurrentSelection == gameObject.GetComponent<Selectable>())
			{
				base.OnDrag();

				if (movePlane.Raycast(mouseScreenPointToRay, out hitDist))
				{
					// find the collision on movePlane
					collisionPoint = mouseScreenPointToRay.GetPoint(hitDist); // define the point on movePlane
					t = mouseScreenPointToRay.origin.y / (mouseScreenPointToRay.origin.y - collisionPoint.y); // the x,y or z plane you want to be fixed to
					corPoint.x = (collisionPoint.x - mouseScreenPointToRay.origin.x) * t; // calculate the new point t futher along the ray
																						  //corPoint.y = camRay.origin.y + (point.y - camRay.origin.y) * t;
					corPoint.z = (collisionPoint.z - mouseScreenPointToRay.origin.z) * t;

					transform.parent.position = corPoint;

					if (gizmosRoot)
						gizmosRoot.transform.position = transform.parent.position;
				}
			}
		}

		public override void OnPointerDown()
		{

			initialPosition = transform.position; // save position in case draged to invalid place
			movePlane = new Plane(-Camera.main.transform.forward, transform.position); // find a parallel plane to the camera based on obj start pos;

		}

		private void initialzeGizmo(ref GameObject obj, string name, Transform parent)
		{
			obj = new GameObject();

			if (parent != null)
			{
				obj.transform.SetParent(parent);
			}

			obj.name = name;
			obj.transform.position = transform.parent.position;
			obj.transform.rotation = transform.parent.rotation;
		}

		public override void OnSceneItemTriggerEnter(Collider col)
		{
			if (col.gameObject != SelectionHandler.instance.CurrentSelection.gameObject && col.gameObject.GetComponent<SceneItem>())
			{
				col.gameObject.GetComponent<Renderer>().material.color = new Color(col.gameObject.GetComponent<Renderer>().material.color.r, col.gameObject.GetComponent<Renderer>().material.color.g, col.gameObject.GetComponent<Renderer>().material.color.b, .2f);
				if (!SelectionHandler.instance.triggeredSceneItems.Contains(col.GetComponent<SceneItem>()))
				{
					col.GetComponent<SceneItem>().canSelect = false;
					SelectionHandler.instance.triggeredSceneItems.Add(col.GetComponent<SceneItem>());
				}

			}
		}

		public override void OnSceneItemTriggerExit(Collider col)
		{
			if (col.gameObject != SelectionHandler.instance.CurrentSelection.gameObject && col.gameObject.GetComponent<SceneItem>())
			{
				col.gameObject.GetComponent<Renderer>().material.color = new Color(col.gameObject.GetComponent<Renderer>().material.color.r, col.gameObject.GetComponent<Renderer>().material.color.g, col.gameObject.GetComponent<Renderer>().material.color.b, 1f);
				col.GetComponent<Selectable>().canSelect = true;

				if (SelectionHandler.instance.triggeredSceneItems.Contains(col.GetComponent<SceneItem>()))
				{
					SelectionHandler.instance.triggeredSceneItems.Remove(col.GetComponent<SceneItem>());
				}

				Debug.Log("Exit Trigger" + col.transform.parent.name);
			}
		}

		public override void OnPointerOver()
		{
			base.OnPointerOver();
			if (SelectionHandler.instance.CurrentSelection == gameObject.GetComponent<Selectable>())
			{
				foreach (var triggeredSceneItem in SelectionHandler.instance.triggeredSceneItems)
				{
					Debug.Log(triggeredSceneItem.gameObject.name);
					//	triggeredSceneItem.gameObject.GetComponent<BoxCollider>().enabled = false;

				}
			}
		}

		public override void OnPointerExit()
		{
			base.OnPointerExit();
			if (SelectionHandler.instance.CurrentSelection == gameObject.GetComponent<Selectable>())
			{
				foreach (var triggeredSceneItem in SelectionHandler.instance.triggeredSceneItems)
				{
					//triggeredSceneItem.gameObject.GetComponent<BoxCollider>().enabled = true;
				}
			}
		}
	}
}
