using UnityEngine;
using UnityEngine.InputSystem;

public class MachineActionController : MonoBehaviour
{
    private MoveMachine moveMachine;
    private CharacterController characterController;
    [SerializeField]
    private PlayerInput inputMap;
    [SerializeField]
    private Camera camera;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        moveMachine = new MoveMachine();
    }

    //private void OnEnable()
    //{
    //    inputMap.actions["Movement"].performed += OnMove;
    //}

    //private void OnDisable()
    //{
    //    inputMap.actions["Movement"].performed -= OnMove;
    //}

    private void Update()
    {
        moveMachine.MovementCalculation(characterController, camera, this.transform);
    }

    //public void OnMove(InputAction.CallbackContext context)
    //{
    //    var value = context.ReadValue<Vector2>();
    //    var direction = new Vector3(value.x, 0, value.y);
    //    moveMachine.MovementCalculation(characterController, camera, this.transform, direction);
    //    Debug.Log("‚“‚“‚“"+value);
    //}

}
