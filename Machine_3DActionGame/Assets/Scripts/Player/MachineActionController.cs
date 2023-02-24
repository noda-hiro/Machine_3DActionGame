using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MachineActionController : MonoBehaviour
{
    private MoveMachineKit moveMachine;
    private BoostMachineKit boostMachine;
    private CharacterController characterController;
    private Coroutine boostValueRecoveryCoroutine;
    private Coroutine boostValueConsumptionCoroutine;

    [SerializeField]
    private BoostParameter boostParameter;
    [SerializeField]
    private Camera camera;
    private InputAction inputAction;
    [SerializeField]
    private PlayerInput PlayerInput;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputAction = new InputAction();
        moveMachine = new MoveMachineKit();
        boostMachine = new BoostMachineKit(boostParameter.maxBoostValue, boostParameter.consumptionBoostValue, boostParameter.recoveryTimeValue, boostParameter.recoveryValue,boostParameter.consumptionTimeValue);
        SetUpInputMap();
    }

    private void Update()
    {
        moveMachine.MovementCalculation(characterController, camera, transform);
        Debug.Log(boostMachine.GetCurrentBoostValue());
    }

    #region ブースト関連

    private IEnumerator BoostConsumption()
    {
        while (boostMachine.CanBoostConsumption())
        {
            boostMachine.ConsumptionBoost();
            yield return new WaitForSeconds(boostMachine.GetConsumptionTime());
        }
        StopBoosConsumption();
    }

    private IEnumerator BoostRecovery()
    {
        while (boostMachine.CanBoostRecovery())
        {
            boostMachine.ConsumptionBoostValueRecovery();
            yield return new WaitForSeconds(boostMachine.GetRecoveryTime());
        }
        StopBoostRecovery();
    }
    #endregion

    #region コルーチン関連
    private void StartBoostRecovery()
    {
        if (boostValueRecoveryCoroutine != null) return;

        boostValueRecoveryCoroutine = StartCoroutine(BoostRecovery());
    }

    private void StopBoostRecovery()
    {
        if (boostValueRecoveryCoroutine == null) return;

        StopCoroutine(boostValueRecoveryCoroutine);
        boostValueRecoveryCoroutine = null;
    }

    private void StartBoostConsumption()
    {
        if (boostValueConsumptionCoroutine != null) return;

        moveMachine.GetIsBoost(true);
        boostValueConsumptionCoroutine = StartCoroutine(BoostConsumption());
    }
    private void StopBoosConsumption()
    {
        if (boostValueConsumptionCoroutine == null) return;

        moveMachine.GetIsBoost(false);
        StopCoroutine(boostValueConsumptionCoroutine);
        boostValueConsumptionCoroutine = null;
    }

    #endregion

    #region ボタン処理
    private void SetUpInputMap()
    {
        inputAction = new InputAction();
        inputAction = PlayerInput.currentActionMap[boostParameter.boostName];
    }

    private void OnEnable()
    {
        inputAction.performed += OnStarted;
        inputAction.canceled += OnCanceled;
    }

    private void OnDisable()
    {
        inputAction.performed -= OnStarted;
        inputAction.canceled -= OnCanceled;
    }

    public void OnStarted(InputAction.CallbackContext context)
    {
        StopBoostRecovery();
        StartBoostConsumption();
    }

    public void OnCanceled(InputAction.CallbackContext context)
    {
        StopBoosConsumption();
        StartBoostRecovery();
    }
    #endregion

}
