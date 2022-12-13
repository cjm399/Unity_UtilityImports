using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Editor
{
	using UnityEditor;
	[CustomEditor(typeof(PlaceItemsInGrid))]
	public class PlaceItemsInGridEditor : Editor
	{
		private PlaceItemsInGrid script;

		private void OnEnable()
		{
			script = (PlaceItemsInGrid)target;
		}

		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Space Out Children"))
			{
				script.SpaceOutChildren();
			}

			base.OnInspectorGUI();
		}
	}
}