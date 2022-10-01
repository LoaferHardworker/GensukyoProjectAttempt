using System;
using UnityEngine;

namespace GameObjects.Weapon
{
	public class SimpleGun : Weapon
	{
		[SerializeField] private Bullet bulletPrefab;
		[SerializeField] private Transform spawnPoint;

		private void Start()
		{
			spawnPoint = spawnPoint != null ? spawnPoint : transform;
		}

		public override void Fire(Vector2 target)
		{
			LookAt(target);

			if (bulletPrefab == null)
				throw new NullReferenceException("Bullet prefab is null!");

			var spawnedBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
			spawnedBullet.tag = tag;
		}
	}
}