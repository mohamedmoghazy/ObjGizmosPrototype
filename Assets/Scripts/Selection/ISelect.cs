using System;
using UnityEngine;

namespace Spaces.Selection
{
	public interface ISelect
	{
		void Select(Selectable selectale);
		void Deselect();
		void Reset();
		bool IsSelecting();
	}
}
