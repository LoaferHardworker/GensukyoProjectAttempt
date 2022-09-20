using ObjetsProperties;
using UnityEngine;

namespace GameObjects
{
	public class DamageZone : MonoBehaviour
	{
		public int damage = 1;

		private void OnTriggerEnter2D(Collider2D col)
		{
			Debug.Log("damage zone!");
			
			var mortal = col.gameObject.GetComponent<Mortal>();
			
			if (mortal)
			{
				mortal.Health -= damage;
			}
		}
	}
}
