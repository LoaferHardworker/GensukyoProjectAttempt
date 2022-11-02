using UnityEngine;

namespace UI
{
    public class FollowingCamera : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float smooth = 3;
        
        private void LateUpdate()
        {
            if (!target) return;
            
            var targetPos = (Vector2) target.position;
            transform.Translate(
                Vector2.Lerp(
                    Vector2.zero, targetPos - (Vector2) transform.position, Time.deltaTime * smooth
                )
            );
        }
    }
}
