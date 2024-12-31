using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerDataConfig playerDataConfig;

    [SerializeField] float playerMaxhealth;
    [SerializeField] float playerCurrentHealth;
    [SerializeField] float playerDamage;
    [SerializeField] float playerArmor;
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float playerAttackSpeed;

    [SerializeField] Joystick joystick;
    [SerializeField] Animator playerAnimation;
    [SerializeField] CharacterController characterController;
    [SerializeField] float rotationSpeed = 700f;
    [SerializeField] Vector3 playerDirection;
    [SerializeField] string walkString = "Walk";
    [SerializeField] int walkHash;

    void Start()
    {
        playerMaxhealth = PlayerDataManager.Instance.Health;
        playerCurrentHealth = playerMaxhealth;
        playerDamage = PlayerDataManager.Instance.Damage;
        playerArmor = PlayerDataManager.Instance.Armor;
        playerMoveSpeed = PlayerDataManager.Instance.MoveSpeed;
        playerAttackSpeed = PlayerDataManager.Instance.AttackSpeed;

        playerAnimation = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        walkHash = Animator.StringToHash(walkString);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        playerDirection = new Vector3(horizontal, 0, vertical);
        playerDirection.Normalize();
        if (playerDirection.magnitude >= 0.01f )
        {
            float targetAngle = Mathf.Atan2(playerDirection.x, playerDirection.z) * Mathf.Rad2Deg;
            float agle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, agle, 0f);
            playerAnimation.SetBool(walkHash, true);
            playerAnimation.speed = playerDirection.magnitude;
        }
        else
        {
            playerAnimation.SetBool(walkHash, false);
        }
        Vector3 moveDirection = playerDirection * 0 * Time.deltaTime;
        characterController.Move(moveDirection);
        
    }
}
