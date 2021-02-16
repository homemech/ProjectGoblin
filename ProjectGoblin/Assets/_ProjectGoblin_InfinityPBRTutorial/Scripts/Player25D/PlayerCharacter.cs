using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] float speed = 3;
    [HideInInspector] public float movementInput;
    [HideInInspector] public bool interactInput;

    public Animator animator;
    private float _previousMovementInput;
    private CharacterController _characterController;

    private void OnEnable()
    {
        _inputReader.moveEvent += OnMove;
        _inputReader.interactEvent += OnInteract;
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMove;
        _inputReader.interactEvent -= OnInteract;
    }

    private void Start()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
    }

    private void OnMove(float movement)
    {
        movementInput = movement;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movementInput == 0)
        {
            animator.SetBool("Move", false);
        }

        if (movementInput < 0f)
        {
            animator.SetBool("Move", true);
            transform.eulerAngles = new Vector3(0, -90, 0);
            _characterController.Move(Vector3.left * speed * Time.deltaTime);
        }

        if (movementInput > 0f)
        {
            animator.SetBool("Move", true);
            transform.eulerAngles = new Vector3(0, 90, 0);
            _characterController.Move(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnInteract()
    {
        interactInput = true;
    }
}