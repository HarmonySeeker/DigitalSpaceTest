using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables: Movement

    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private Movement movement;

    #endregion

    #region Variables: Rotation

    [SerializeField] private float rotationSpeed = 500f;
    private Camera _mainCamera;

    #endregion

    #region Variables: Gravity

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    #endregion

    #region Variables: Jumping
    
    [SerializeField] private float jumpPower;
    private int _numberOfJumps;
    [SerializeField] private int maxNumJumps = 2;

    #endregion

    [SerializeField] private float interactRange;

    #region Events
    public event UnityAction PlayerJump;

    public void OnPlayerJump() => PlayerJump?.Invoke();
    #endregion

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
        PlaySounds();
    }

    private void PlaySounds()
    {

    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0)
        {
            return;
        }

        _direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3 (_input.x, 0.0f, _input.y);
        var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);

        _characterController.Move(_direction * movement.currentSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
        //AudioManager.Instance.PlaySound2D(AudioNames.Sound.PlayerWalk);
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        movement.isSprinting = context.started || context.performed;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded() && _numberOfJumps >= maxNumJumps) return;
        if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());

        _numberOfJumps++;
        _velocity = jumpPower;

        OnPlayerJump();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray r = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
            Debug.DrawLine(r.origin, r.origin + r.direction * interactRange, new Color(1f, 0f, 0f), 500f);

            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)
                && hitInfo.collider.gameObject.CompareTag("Interactable"))
            {
                hitInfo.collider.gameObject.TryGetComponent(out IInteractableObject interactableObj);
                interactableObj.InteractableAction();
            }
        }
    }

    public bool IsSprinting() => movement.isSprinting;

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);

        _numberOfJumps = 0;
    }

    private bool IsGrounded() => _characterController.isGrounded;
}

[Serializable]
public struct Movement
{
    public float speed;
    public float multiplier;
    public float acceleration;

    [HideInInspector] public bool isSprinting;
    [HideInInspector] public float currentSpeed;
}
