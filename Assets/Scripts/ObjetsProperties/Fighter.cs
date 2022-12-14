using System;
using System.Collections;
using GameObjects.Weapon;
using UnityEngine;
using UnityEngine.Events;

namespace ObjetsProperties
{
	public class Fighter : MonoBehaviour
	{
		[SerializeField] private Weapon weapon;
		[SerializeField] private int energy;
		[SerializeField] private int maxEnergy;
		[SerializeField] private float energyRecoveryDelay = 1;

		public int Energy
		{
			get => energy;
			private set
			{
				energy = value;
				energyIsChanged.Invoke();
			}
		}

		public int MaxEnergy => maxEnergy;

		public Weapon Weapon => weapon;
		
		public UnityEvent energyIsChanged = new UnityEvent();

		private void Start() => StartCoroutine(IncreaseEnergy());

		public bool Fire(Vector2 point)
		{
			var cost = weapon.Cost;
			if (cost > energy) return false;

			Energy -= cost;
			weapon.Fire(point);
			return true;
		}

		public void LookAt(Vector2 point) => weapon.LookAt(point);

		private IEnumerator IncreaseEnergy()
		{
			while (true)
			{
				yield return new WaitForSeconds(energyRecoveryDelay);
				if (energy == maxEnergy) continue;
				Energy = Math.Min(energy + 1, maxEnergy);
			}
		}
	}
}