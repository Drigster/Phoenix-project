using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector3 _moveDirection;

    [SerializeField] private float _walkSpeed = 5f;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.Movement.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Movement.Disable();
    }

    void Update()
    {
        _moveDirection.x = _playerInput.Player.Movement.ReadValue<Vector2>().x;
        _moveDirection.y = _playerInput.Player.Movement.ReadValue<Vector2>().y;

        _animator.SetFloat("Horizontal", _moveDirection.x);
        _animator.SetFloat("Vertical", _moveDirection.y);
        _animator.SetFloat("Speed", _moveDirection.sqrMagnitude);

        if (_moveDirection.x == 1 || _moveDirection.x == -1)
        {
            _animator.SetFloat("LastHorizontal", _moveDirection.x);
            _animator.SetFloat("LastVertical", 0);
        }

        if(_moveDirection.y == 1 || _moveDirection.y == -1)
        {
            _animator.SetFloat("LastVertical", _moveDirection.y);
            _animator.SetFloat("LastHorizontal", 0);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveDirection.normalized * _walkSpeed;
    }
}
