using UnityEngine;
using UnityEngine.InputSystem;

public class BallAbilities : MonoBehaviour
{
    public Rigidbody2D _rb;

    [SerializeField] private float _moveSpeed = 3;

    private Vector2 _moveDirection;

    public InputActionReference move;
    public InputActionReference fire;

    // Update is called once per frame
    void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveDirection.x * _moveSpeed, 
            _moveDirection.y * _moveSpeed);
        Debug.Log(_rb.velocity);
    }

    private void OnEnable()
    {
        fire.action.started += Fire;
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        Debug.Log("Fired");
    }
}
