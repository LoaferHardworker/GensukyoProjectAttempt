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
		[SerializeField] private float energyRecoverySpeed = 1;

		public int Energy
		{
			get => energy;
			set
			{
				energy = value;
				energyIsChanged.Invoke();
			}
		}

		public int MaxEnergy => maxEnergy;

		public Weapon Weapon
		{
			set => weapon = value;
		}
		
		public UnityEvent energyIsChanged = new UnityEvent();

		private void Start()
		{
			StartCoroutine(IncreaseEnergy());
		}

		public bool Fire(Vector2 point)
		{
			var cost = weapon.Cost;
			if (cost > energy)
			{
				Debug.Log($"{energy} energy is not enough: you need {cost}");
				return false;
			}

			Energy -= cost;
			weapon.Fire(point);
			return true;
		}

		public void LookAt(Vector2 point) => weapon.LookAt(point);

		private IEnumerator IncreaseEnergy()
		{
			while (true)
			{
				yield return new WaitForSeconds(energyRecoverySpeed);
				if (energy == maxEnergy) continue;
				Energy = Math.Min(energy + 1, maxEnergy);
			}
		}
	}
}