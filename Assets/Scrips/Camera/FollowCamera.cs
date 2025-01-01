using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Đối tượng người chơi
    public Vector3 offset;   // Offset giữa camera và người chơi
    public float smoothSpeed = 0.125f; // Tốc độ mượt mà khi di chuyển camera

    void LateUpdate()
    {
        if (player != null)
        {
            // Tính toán vị trí mong muốn
            Vector3 desiredPosition = player.position + offset;

            // Di chuyển camera mượt mà đến vị trí đó
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Cập nhật vị trí của camera
            transform.position = smoothedPosition;

        }
    }
}
