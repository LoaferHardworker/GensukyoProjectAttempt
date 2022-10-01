using System;
using System.Collections.Generic;
using System.Data;
using GameObjects.Weapon;
using UnityEngine;

namespace ObjetsProperties
{
	public class Fighter : MonoBehaviour
	{
		[SerializeField] private List<Weapon> weapons;
		[SerializeField] private int energy;

		private Weapon _actualWeapon;
		
		public List<Weapon> Weapons => weapons;

		private void Start() => Chose(0);

		public void Chose(int id)
		{
			if (weapons.Count == 0)
				throw new DataException("You have no weapon to chose");

			if (id < 0 || id >= weapons.Count)
				throw new IndexOutOfRangeException("Incorrect weapon id");
			
			_actualWeapon = weapons[0];
		}

		public void Fire(Vector2 point) => _actualWeapon.Fire(point);
		
		public void LookAt(Vector2 point) => _actualWeapon.LookAt(point);
	}
}