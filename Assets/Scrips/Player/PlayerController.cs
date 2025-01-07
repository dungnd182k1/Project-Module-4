using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Joystick joystick;

    [SerializeField] Animator playerAnimation;
    [SerializeField] CharacterController characterController;
    [SerializeField] float rotationSpeed = 700f;
    [SerializeField] Vector3 playerDirection;
    [SerializeField] string walkString = "Walk";
    [SerializeField] int walkHash;
    [SerializeField] string walkSpeedString = "WalkSpeed";
    [SerializeField] int walkSpeedHash;
    [SerializeField] float walkSpeed;
    [SerializeField] float walkSpeedLevle = 6.0f; // Tốc độ theo cấp độ của người chơi. Thấp nhất là 6 người chơi đi bộ. Cao nhất là 10 người chơi sẽ chạy.

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        walkHash = Animator.StringToHash(walkString);
        walkSpeedHash = Animator.StringToHash(walkSpeedString);
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
            walkSpeed = new Vector2(horizontal, vertical).magnitude * (walkSpeedLevle/10);
            playerAnimation.SetFloat(walkSpeedHash, walkSpeed);
        }
        else
        {
            playerAnimation.SetBool(walkHash, false);
        }
        Vector3 moveDirection = playerDirection * 0 * Time.deltaTime;
        characterController.Move(moveDirection);
        
    }
}
