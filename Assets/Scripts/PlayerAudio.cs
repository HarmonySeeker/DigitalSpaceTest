using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CharacterController))]
public class PlayerAudio : MonoBehaviour
{
    private PlayerController _playerController;
    private CharacterController _characterController;

    [SerializeField] private float walkStepInterval = 0.5f; // Интервал между шагами
    [SerializeField] private float runStepInterval = 0.3f;
    
    private float _stepTimer;
    private bool _isSprinting;

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isSprinting = _playerController.IsSprinting();

        if (_characterController.isGrounded && _characterController.velocity.magnitude > 0.2f)
        {
            _stepTimer -= Time.deltaTime;

            if (_stepTimer <= 0f)
            {
                if (_isSprinting)
                {
                    AudioManager.Instance.PlaySound2D(AudioNames.Sound.PlayerWalk);
                    _stepTimer = runStepInterval;
                    Debug.Log("sprinting");
                }
                else
                {
                    AudioManager.Instance.PlaySound2D(AudioNames.Sound.PlayerWalk);
                    _stepTimer = walkStepInterval;
                    Debug.Log("walking");
                }
            }
        }
        else
        {
            _stepTimer = 0f;
        }
    }

    private void OnEnable()
    {
        _playerController.PlayerJump += PlayerControllerOnPlayerJump;
    }

    private void PlayerControllerOnPlayerJump()
    {
        AudioManager.Instance.PlaySound2D(AudioNames.Sound.PlayerJump);
    }
}
