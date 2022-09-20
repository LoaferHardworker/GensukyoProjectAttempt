using System;
using ObjetsProperties;
using UnityEngine;

namespace UI
{
	public class LinearHealthBar : MonoBehaviour
	{
		public Mortal mortal;
		private RectTransform _rect;
		private float xScale;

		private void Start()
		{
			mortal.healthIsChanged.AddListener(UpdateBar);
			_rect = GetComponent<RectTransform>();
			xScale = _rect.localScale.x;
		}

		private void UpdateBar()
		{
			var scale = _rect.localScale;
			_rect.localScale = new Vector3(xScale * mortal.Health / mortal.MaxHealth, scale.y, scale.z);
		}
	}
}