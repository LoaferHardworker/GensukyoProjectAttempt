using System;
using UnityEngine;

namespace AI
{
	public class RaycastVision : MonoBehaviour
	{
		[SerializeField] private int countOfRays = 60;
		[SerializeField] private float raysDistance = 12f;

		public float RaysDistance => raysDistance;

		private Vector2[] _raysDirections;
		private Vector2[] _startPositions;

		private bool _first = true;

		private void Init()
		{
			GetComponent<Collider2D>();
			
			_raysDirections = new Vector2[countOfRays];
			_startPositions = new Vector2[countOfRays];
			
			for (int i = 0; i < countOfRays; ++i)
			{
				var angle = 2 * Math.PI * i / countOfRays;
				var direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
				_raysDirections[i] = direction;

				var position = (Vector2)transform.position;
				
				var hits = Physics2D.RaycastAll(
					position + direction * raysDistance,
					-direction,
					raysDistance);

				_startPositions[i] = hits[hits.Length - 1].point + direction * 0.01f - position;
			}
		}

		public RaycastHit2D[] LookAround()
		{
			if (_first)
			{
				_first = false;
				Init();
			}
			
			var raycastHits = new RaycastHit2D[countOfRays];
			
			for (int i = 0; i < countOfRays; ++i)
			{
				var hit = Physics2D.Raycast((Vector2)transform.position + _startPositions[i],
					_raysDirections[i], raysDistance);

				raycastHits[i] = hit;
				
				DebugDrawHit(hit, i);
			}

			return raycastHits;
		}

		private void DebugDrawHit(RaycastHit2D hit, int i)
		{
			if (hit.collider)
				Debug.DrawRay(transform.position + (Vector3) _startPositions[i], _raysDirections[i] * hit.distance,
					Color.red);
			else
				Debug.DrawRay(transform.position + (Vector3)_startPositions[i], _raysDirections[i] * raysDistance,
					Color.white);
		}
	}
}