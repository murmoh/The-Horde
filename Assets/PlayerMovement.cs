using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float moveSmoothTime = 0.1f;
    public float gravityStrength = 9.8f;
    public float jumpStrength = 2.5f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float climbSpeed = 5f;
    public float wallCheckDistance = 2f;
    private CharacterController controller;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;
    private Vector3 currentForceVelocity;
    public string Mode = "Survival";
    [Header("Stamina Setting")]
    [SerializeField] private Image StaminaUI;
    [SerializeField] private float RecoverSpeed_Stamina;
    public const float StaminaAmount = 100f;
    [SerializeField] public float SA = StaminaAmount;
    public float CurrentStamina = StaminaAmount;
    [Range(-10.0f, 10.0f)] public float Stamina_recover_speed;
    [Range(-10.0f, 10.0f)] public float Stamina_lose_speed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Stamina_n_Recover();
        StaminaUI.fillAmount = CurrentStamina / SA;
        Vector3 playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (playerInput.magnitude > 1f)
        {
            playerInput.Normalize();
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, moveVector * currentSpeed, ref moveDampVelocity, moveSmoothTime);
        if(moveVector != Vector3.zero)
            controller.Move(currentMoveVelocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                currentForceVelocity.y = jumpStrength;
            }
        }


        else if(Mode == "Creative")
        {
            if(Input.GetKey(KeyCode.Space) && controller.isGrounded == false)
                {
                    currentForceVelocity.y += 1f;
                }
            if(Input.GetKey(KeyCode.LeftControl) && controller.isGrounded == false)
                {
                    currentForceVelocity.y -= 1f;
                }
        }

        else
        {
            currentForceVelocity.y -= gravityStrength * Time.deltaTime;
        }

        controller.Move(currentForceVelocity * Time.deltaTime);

    }

    void Stamina_n_Recover()
    {
        if(CurrentStamina <= 0)
        {
            CurrentStamina = 1;
        }
        if(CurrentStamina == 100f &! Input.GetKey(KeyCode.LeftShift))
        {
            RecoverSpeed_Stamina = 1.62f;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            CurrentStamina -=  StaminaAmount / CurrentStamina * Stamina_lose_speed;
            RecoverSpeed_Stamina = 1.62f;
        }
        else
        {
            RecoverSpeed_Stamina -= Time.deltaTime;
            if(RecoverSpeed_Stamina <= 0)
            {
                if(CurrentStamina < 100f)
                {
                    CurrentStamina += StaminaAmount / CurrentStamina * Stamina_recover_speed    ;
                    if(CurrentStamina >= 100f)
                    {
                        CurrentStamina = 100f;
                        RecoverSpeed_Stamina = 1.62f;
                    }
                }
                
            }
        }
    }
}