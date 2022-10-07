using ObjetsProperties;
using UnityEngine;

namespace UI
{
	public class LinearEnergyBar : MonoBehaviour
	{
		public Fighter fighter;
		private RectTransform _rect;
		private float xScale;

		private void Start()
		{
			fighter.energyIsChanged.AddListener(UpdateBar);
			_rect = GetComponent<RectTransform>();
			xScale = _rect.localScale.x;
		}

		private void UpdateBar()
		{
			var scale = _rect.localScale;
			_rect.localScale = new Vector3(xScale * fighter.Energy / fighter.MaxEnergy, scale.y, scale.z);
		}
	}
}