using System.Collections.Generic;
using UnityEngine;

namespace Spaces.Selection
{
	public class SelectionHandler : MonoBehaviour, ISelect
	{
		public static SelectionHandler instance;

		public Selectable _currentSelection;

		public List<SceneItem> triggeredSceneItems = new List<SceneItem>();


		public Selectable CurrentSelection
		{
			get
			{
				return _currentSelection;
			}

			set
			{
				if (IsSelecting())
				{
					Deselect();
				}

				_currentSelection = value;

			}
		}
		public void Awake()
		{
			//Check if instance already exists
			if (instance == null)
			{
				//if not, set instance to this
				instance = this;
			}

			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
		}


		public void Select(Selectable selectable)
		{
			if (_currentSelection != selectable && selectable.canSelect && !(selectable is Gizmos.Gizmo))
			{
				CurrentSelection = selectable;
				_currentSelection.OnSelect();
			}
		}

		public void Deselect()
		{
			_currentSelection.OnDeselect();
			_currentSelection = null;

		}

		public void Reset()
		{
			if (IsSelecting())
			{
				Deselect();
				//triggeredSceneItems.Clear();
			}
		}

		public bool IsSelecting()
		{
			if (_currentSelection != null)
				return true;
			else
				return false;

		}

		public Transform GetTransformOfCurrentSelection()
		{

			if (_currentSelection != null)
				return _currentSelection.transform;
			else
				return null;

		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				RaycastHit hit;
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider.gameObject.GetComponent<ISelectable>() != null)
					{
						Select(hit.collider.gameObject.GetComponent<Selectable>());
					}
					else
					{
						Reset();
					}
				}
				else
				{
					Reset();
				}

			}
		}
	}
}