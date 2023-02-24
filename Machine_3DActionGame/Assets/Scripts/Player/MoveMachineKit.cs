using UnityEngine;

public class MoveMachineKit
{
    private const float moveSpeed = 10f;
    private const float dashSpeedd = 2f;
    private const float gravity = -9.81f;
    private bool isBoost;
    private Vector3 moveDire = Vector3.zero;

    public void MovementCalculation(CharacterController characterController, Camera camera, Transform transform)
    {
        var keyValue = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDire = keyValue;
        moveDire = transform.TransformDirection(moveDire);
        moveDire *= isBoost ? moveSpeed * dashSpeedd : moveSpeed;

        moveDire.y += gravity * Time.deltaTime;
        characterController.Move(moveDire * Time.deltaTime);
    }

    public void GetIsBoost(bool canBoost)
    {
        isBoost = canBoost;
    }

}

public class BoostMachineKit
{
    private readonly float maxBoostValue = 0f;
    private const float minBoostValue = 0f;
    private float currentBoostValue = 0;
    private readonly float consumptionTimeValue = 5f;
    private readonly float consumptionBoostValue = 5f;
    private readonly float recoveryTimeValue = 0;
    private readonly float recoveryValue = 0;

    public BoostMachineKit(float maxBoostValue, float consumptionBoostValue, float recoveryTimeValue, float recoveryValue, float consumptionTimeValue)
    {
        this.maxBoostValue = maxBoostValue;
        this.consumptionBoostValue = consumptionBoostValue;
        this.recoveryTimeValue = recoveryTimeValue;
        this.recoveryValue = recoveryValue;
        this.consumptionTimeValue = consumptionTimeValue;
        InitBoostValue();
    }

    private void InitBoostValue()
    {
        currentBoostValue = maxBoostValue;
    }
    public float GetCurrentBoostValue()
    {
        return currentBoostValue;
    }
    public void ConsumptionBoost()
    {
        if (currentBoostValue < minBoostValue) return;

        currentBoostValue = Mathf.Max(currentBoostValue -= consumptionBoostValue, 0);
    }

    public void ConsumptionBoostValueRecovery()
    {
        currentBoostValue = Mathf.Min(currentBoostValue += recoveryValue, maxBoostValue); ;
    }

    public bool CanBoostRecovery()
    {
        return currentBoostValue != maxBoostValue ? true : false;
    }

    public bool CanBoostConsumption()
    {
        return currentBoostValue > minBoostValue ? true : false;
    }

    public float GetRecoveryTime()
    {
        return recoveryTimeValue;
    }

    public float GetConsumptionTime()
    {
        return consumptionTimeValue;
    }

}
