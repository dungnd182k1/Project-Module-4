using UnityEngine;

// Định nghĩa lớp trừu tượng Singleton, phải kế thừa từ MonoBehaviour
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    // Biến tĩnh để lưu trữ instance của Singleton
    private static T Current;

    // Thuộc tính tĩnh để lấy instance của Singleton
    public static T Instance
    {
        get
        {
            // Nếu không có instance nào tồn tại, tìm instance trong scene
            if (Current == null) Current = FindObjectOfType<T>();

            // Nếu không tìm thấy trong scene, tạo một đối tượng mới và thêm component T vào
            if (Current == null) Current = new GameObject(typeof(T).Name).AddComponent<T>();

            return Current;
        }
    }

    // Xóa instance hiện tại nếu instance này là đối tượng hiện tại
    private void ClearInstance()
    {
        if (Current == this) Current = null;
    }

    // Phương thức này được gọi khi đối tượng được khởi tạo
    protected virtual void Awake()
    {
        // Nếu chưa có instance nào, đặt instance này là instance hiện tại
        if (Current == null) Current = this as T;

        // Nếu đã có instance khác tồn tại, hủy đối tượng này
        else if (Current != this) Destroy(gameObject);
    }

    // Phương thức này được gọi khi đối tượng bị hủy
    protected virtual void OnDestroy() => ClearInstance();

    // Phương thức này được gọi khi ứng dụng thoát
    protected virtual void OnApplicationQuit() => ClearInstance();
}
