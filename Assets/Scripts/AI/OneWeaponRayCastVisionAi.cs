using System.Collections;
using ObjetsProperties;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
	public class OneWeaponRayCastVisionAi : MonoBehaviour
	{
		[SerializeField] [Range(0f, 35f)] private float speed = 5f;
		
		[Header("AI movement properties")]
		[SerializeField] private Vector2 movementUpdateDelayRange = new Vector2(0.02f, 0.4f);
		[SerializeField] [Range(0f, 35f)] private float companionDistance = 5f;
		[SerializeField] [Range(0f, 35f)] private float companionValue = 5f;
		[SerializeField] [Range(0f, 35f)] private float enemyDistance = 5f;
		[SerializeField] [Range(0f, 35f)] private float enemyValue = 7f;
		[SerializeField] [Range(0f, 35f)] private float wallDistance = 7f;
		[SerializeField] [Range(0f, 35f)] private float wallValue = 2f;

		[Header("AI attack properties")]
		[SerializeField] [Range(0f, 3f)] private float updateAimRate;
		[SerializeField] [Range(1f, 5f)] [Tooltip("Factor for delay of weapon")] private float maxAttackDelay = 1f; 
		
		private RaycastVision _vision;
		private Rigidbody2D _rb;
		private Fighter _fighter;

		private Vector2 _velocity;
		private Transform _target;

		private void Start()
		{
			_vision = GetComponent<RaycastVision>();
			_rb = GetComponent<Rigidbody2D>();
			_fighter = GetComponent<Fighter>();

			StartCoroutine(Move());
			StartCoroutine(Aim());
			StartCoroutine(Fire());
		}

		private void Update()
		{
			if (_target) _fighter.LookAt(_target.position);
		}

		private void FixedUpdate()
		{
			_rb.velocity = _velocity.normalized * speed;
		}

		private IEnumerator Move()
		{
			while (true)
			{
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

		private IEnumerator Aim()
		{
			while (true)
			{
				var distanceToTarget = float.PositiveInfinity;
				_target = null;
				
				foreach (var hit in _vision.LookAround())
				{
					if (!hit.collider) continue;
					
					if (hit.collider.GetComponent<Fighter>()
						&&hit.collider.GetComponent<Mortal>()
					    && hit.collider.CompareTag(tag) == false
					    && hit.distance < distanceToTarget)
					{
						_target = hit.collider.transform;
						distanceToTarget = hit.distance;
					}
				}
				
				yield return new WaitForSeconds(updateAimRate);
			}
		}

		private IEnumerator Fire()
		{
			while (true)
			{
				yield return new WaitForSeconds(_fighter.Weapon.Delay * Random.Range(1, maxAttackDelay));
				yield return new WaitUntil(() => _target);
				_fighter.Fire(_target.position);
			}
		}

		private static Vector2 EvaluateVelocityChanging(Vector2 dir, float distance, float value)
		{
			return dir.normalized * ((dir.magnitude - distance) * value);
		}
	}
}