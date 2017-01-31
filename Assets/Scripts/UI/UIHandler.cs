using UnityEngine;
using System.Collections;

namespace Spaces.UI
{
	public class UIHandler : MonoBehaviour
	{
		public static UIHandler instance;
		public GameObject ManipulationTool;

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

		public void ShowManipulationTool()
		{
			ManipulationTool.GetComponent<Animator>().SetTrigger("show");
			Debug.Log("ShowManipulationTool");
		}

		public void HideManipulationTool()
		{
			ManipulationTool.GetComponent<Animator>().SetTrigger("hide");
			Debug.Log("HideManipulationTool");

		}


	}
}
