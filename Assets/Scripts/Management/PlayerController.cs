using UnityEngine;
using ObjetsProperties;
using UnityEngine.InputSystem;

namespace Management
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        
        private DefaultInput _input;
        private Fighter _fighter;
        private Rigidbody2D _rb;

        private Vector2 _aimPoint;
        private Vector2 _direction;

        private void Awake()
        {
            _input = new DefaultInput();
            _fighter = GetComponent<Fighter>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _fighter.LookAt(Camera.main.ScreenToWorldPoint(_aimPoint));
        }

        private void FixedUpdate()
        {
            _rb.velocity = _direction;
        }

        private void OnEnable()
        {
            _input.Enable();
            
            _input.OnFoot.MovementDirection.performed += SetDirectionOfMovement;
            _input.OnFoot.MovementDirection.canceled += SetDirectionOfMovement;

            _input.OnFoot.Aim.performed += Aim;
            _input.OnFoot.Attack.started += Fire;
        }

        private void OnDisable() => _input.Disable();

        private void SetDirectionOfMovement(InputAction.CallbackContext ctx)
            => _direction = ctx.ReadValue<Vector2>() * speed;

        private void Aim(InputAction.CallbackContext ctx)
            => _aimPoint = ctx.ReadValue<Vector2>();

        private void Fire(InputAction.CallbackContext ctx)
            => _fighter.Fire(Camera.main.ScreenToWorldPoint(_aimPoint));
    }
}
