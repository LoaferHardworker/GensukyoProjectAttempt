using System;
using System.Collections;
using ObjetsProperties;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
	public class OneWeaponRayCastVisionAi : MonoBehaviour
	{
		[SerializeField][Range(0f, 35f)] private float speed = 5f;
		[SerializeField] private Vector2 movementUpdateDelayRange = new Vector2(0.02f, 0.4f);
		[SerializeField][Range(0f, 35f)] private float companionDistance = 5f;
		[SerializeField][Range(0f, 35f)] private float companionValue = 5f;
		[SerializeField][Range(0f, 35f)] private float enemyDistance = 5f;
		[SerializeField][Range(0f, 35f)] private float enemyValue = 7f;
		[SerializeField][Range(0f, 35f)] private float wallDistance = 7f;
		[SerializeField][Range(0f, 35f)] private float wallValue = 2f;
		
		private RaycastVision _vision;
		private Rigidbody2D _rb;
		private Fighter _fighter;

		private Vector2 _velocity;

		private void Start()
		{
			_vision = GetComponent<RaycastVision>();
			_rb = GetComponent<Rigidbody2D>();
			_fighter = GetComponent<Fighter>();

			StartCoroutine(Movement());
		}

		private void FixedUpdate()
		{
			print(_velocity);
			_rb.velocity = _velocity.normalized * speed;
		}

		private IEnumerator Movement()
		{
			while (true)
			{
				var t = Time.time;
				var hits = _vision.LookAround();
				_velocity = Vector2.zero;

				foreach (var hit in hits)
				{
					if (! hit.collider) continue;
					
					var dir = hit.point - (Vector2) transform.position;
					var hitGameObject = hit.collider.gameObject;

					if (hitGameObject.GetComponent<Fighter>())
					{
						if (hitGameObject.CompareTag(tag))
							_velocity += EvaluateVelocityChanging(dir, companionDistance, companionValue);
						else 
							_velocity += EvaluateVelocityChanging(dir, enemyDistance, enemyValue);
					}

					else
					{
						_velocity += EvaluateVelocityChanging(dir, wallDistance, wallValue);
					}
				}
				
				yield return new WaitForSeconds(Random.Range(movementUpdateDelayRange.x, movementUpdateDelayRange.y));
			}
		}

		private static Vector2 EvaluateVelocityChanging(Vector2 dir, float distance, float value)
		{
			return dir.normalized * ((dir.magnitude - distance) * value);
		}
	}
}