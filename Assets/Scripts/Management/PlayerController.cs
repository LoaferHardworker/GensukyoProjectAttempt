using UnityEngine;
using ObjetsProperties;
using UnityEngine.InputSystem;

namespace Management
{
    public class PlayerController : MonoBehaviour
    {
        private DefaultInput _input;
        private Movable _movable;
        private Fighter _fighter;

        private Vector2 _aimPoint;

        private void Awake()
        {
            _input = new DefaultInput();
            _movable = GetComponent<Movable>();
            _fighter = GetComponent<Fighter>();
        }

        private void OnEnable()
        {
            _input.Enable();
            
            _input.OnFoot.MovementDirection.performed += SetDirectionOfMovement;
            _input.OnFoot.MovementDirection.canceled += SetDirectionOfMovement;

            _input.OnFoot.Aim.performed += Aim;
            _input.OnFoot.Attack.started += Fire;
        }

        private void FixedUpdate() => _fighter.LookAt(_aimPoint);

        private void SetDirectionOfMovement(InputAction.CallbackContext ctx)
            => _movable.Direction = ctx.ReadValue<Vector2>();

        private void Aim(InputAction.CallbackContext ctx)
            => _aimPoint = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());

        private void Fire(InputAction.CallbackContext ctx)
            => _fighter.Fire(Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>()));
    }
}
