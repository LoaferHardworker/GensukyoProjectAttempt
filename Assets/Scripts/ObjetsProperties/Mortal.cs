using System;
using UnityEngine;

namespace ObjetsProperties
{
    public class Mortal : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;

        public int Health
        {
            get => health;
            set
            {
                health = Math.Min(value, maxHealth);
                
                if (health < 0)
                    Destroy(gameObject);
            }
        }
    }
}
