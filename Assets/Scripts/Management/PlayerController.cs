using System.Collections;
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
        private Coroutine _fire;

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
            _input.OnFoot.Attack.started += BeginFire;
            _input.OnFoot.Attack.canceled += StopFire;
        }

        private void OnDisable() => _input.Disable();

        private void SetDirectionOfMovement(InputAction.CallbackContext ctx)
            => _direction = ctx.ReadValue<Vector2>() * speed;

        private void Aim(InputAction.CallbackContext ctx)
            => _aimPoint = ctx.ReadValue<Vector2>();

        private IEnumerator Fire()
        {
            while (true)
            {
                _fighter.Fire(Camera.main.ScreenToWorldPoint(_aimPoint));
                yield return new WaitForSeconds(_fighter.Weapon.Delay);
            }
        }
        
        private void BeginFire(InputAction.CallbackContext obj) => _fire = StartCoroutine(Fire());

        private void StopFire(InputAction.CallbackContext ctx) => StopCoroutine(_fire);
    }
}
