using UnityEngine;
using System.Collections;

namespace Spaces.Selection
{
	public interface ISelectable 
	{
		void OnSelect();
		void OnDeselect();
	}
}
