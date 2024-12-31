using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerPosition; // Lấy vị trí của người chơi
    [SerializeField] Vector3 offset;  // Offset giữa camera và người chơi
    [SerializeField] float smoothSpeed = 0.125f; // Độ mượt khi di chuyển camera

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerPosition != null)
        {
            // Tính toán vị trí mong muốn
            Vector3 desiredPosition = playerPosition.position + offset;

            // Di chuyển camera đến vị trí mong muốn
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Cập nhật ví trí camera
            transform.position = smoothedPosition;

            // Đặt camera nhìn vào người chơi
            // transform.LookAt(playerPosition);
        }
    }
}
