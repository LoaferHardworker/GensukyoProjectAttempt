using UnityEngine;

namespace ObjetsProperties
{
    public class Movable : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 direction;
        
        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public Vector2 Direction
        {
            get => direction;
            set => direction = value.normalized;
        }

        private void FixedUpdate()
        {
            transform.Translate(direction * (Time.deltaTime * speed));
        }
    }
}
