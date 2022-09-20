using System;
using UnityEngine;
using UnityEngine.Events;

namespace ObjetsProperties
{
    public class Mortal : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;

        public UnityEvent healthIsChanged = new UnityEvent();

        public int Health
        {
            get => health;
            set
            {
                health = Math.Min(value, maxHealth);
                healthIsChanged.Invoke();
                
                if (health <= 0)
                    Destroy(gameObject);
            }
        }

        public int MaxHealth => maxHealth;
    }
}
