using UnityEngine;
using ObjetsProperties;
using UnityEngine.InputSystem;

namespace Management
{
    [RequireComponent(typeof(Mortal))]
    [RequireComponent(typeof(Movable))]
    public class PlayerController : MonoBehaviour
    {
        private DefaultInput _input;
        private Mortal _mortal;
        private Movable _movable;

        private void Awake()
        {
            _input = new DefaultInput();
            _mortal = GetComponent<Mortal>();
            _movable = GetComponent<Movable>();
        }

        private void OnEnable()
        {
            _input.Enable();
            
            _input.OnFoot.MovementDirection.performed += SetDirectionOfMovement;
            _input.OnFoot.MovementDirection.canceled += SetDirectionOfMovement;
        }

        void SetDirectionOfMovement(InputAction.CallbackContext ctx)
        {
            _movable.Direction = ctx.ReadValue<Vector2>();
            Debug.Log(ctx.ReadValue<Vector2>());
        }
    }
}
