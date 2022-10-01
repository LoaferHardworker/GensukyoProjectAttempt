using UnityEngine;

namespace GameObjects.Weapon
{
	public abstract class Weapon : MonoBehaviour
	{
		[SerializeField] private int cost = 1;
		[SerializeField] private float delay = 0.5f;

		public int Cost => cost;
		public float Delay => delay;

		public abstract void Fire(Vector2 target);
		public void LookAt(Vector2 target)
		{
			var position = (Vector2)transform.position;
			var angle = Vector2.Angle(Vector2.right, target - position);
		
			transform.eulerAngles = new Vector3(0, 0, (target.y < position.y ? -1 : 1) * angle);
		}
	}
}