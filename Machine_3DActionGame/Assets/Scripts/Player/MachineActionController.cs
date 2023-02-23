using UnityEngine;

public class MachineActionController : MonoBehaviour
{
    private MoveMachine moveMachine;
    private CharacterController characterController;
    [SerializeField]
    private Camera camera;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        moveMachine = new MoveMachine();
    }

    private void Update()
    {
        moveMachine.MovementCalculation(characterController, camera, this.transform);
    }

}
