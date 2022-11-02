using ObjetsProperties;
using UnityEngine;

namespace GameObjects.Weapon
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class Bullet : MonoBehaviour
	{
		private Rigidbody2D _rb;

		[SerializeField] private int rebounds;
		[SerializeField] private float speed = 10;
		[SerializeField] private int damage = 1;
		[SerializeField] private float strength = 1;

		public float Strength
		{
			get => strength;
			set
			{
				strength = value;
				if (_rb != null) _rb.velocity = transform.right * speed * strength;
			}
		}

		private void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_rb.velocity = transform.right * speed * strength;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var mortal = other.GetComponent<Mortal>();
			if (mortal != null) Damage(mortal);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			var mortal = other.gameObject.GetComponent<Mortal>();
			if (mortal != null) Damage(mortal);
			else if (rebounds-- <= 0) Destroy(gameObject);
		}

		private void Damage(Mortal mortal)
		{
			if (mortal.gameObject.CompareTag(gameObject.tag) == false)
				mortal.Health -= damage;
			Destroy(gameObject);
		}
	}
}