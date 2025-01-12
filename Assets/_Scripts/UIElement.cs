using System;
using System.Threading.Tasks;
using UnityEngine;

// Lớp trừu tượng UIElement, phải kế thừa từ MonoBehaviour
public abstract class UIElement : MonoBehaviour
{
    // Hành động để gọi khi UI phần tử bị ẩn
    private Action onHidden;

    // Các thuộc tính trừu tượng cần được triển khai bởi lớp con
    public abstract bool ManualHide { get; }
    public abstract bool DestroyOnHide { get; }
    public abstract bool UseBehindPanel { get; }

    // Tham chiếu tới GameObject holder và CanvasGroup được gán từ Unity Editor
    [SerializeField] protected GameObject holder;
    [SerializeField] protected CanvasGroup canvasGroup;

    // Phương thức hiển thị phần tử UI với hành động khi bị ẩn
    public virtual void Show(Action hidden)
    {
        onHidden = hidden;
        Show();
    }

    // Phương thức hiển thị phần tử UI
    public virtual void Show()
    {
        // Đăng ký phần tử UI với hệ thống GameUI
        GameUI.Instance.Submit(this);

        // Hiển thị holder nếu nó không null
        holder?.SetActive(true);

        // Gọi phương thức chạy hoạt ảnh
        Anim(true);
    }

    // Phương thức ẩn phần tử UI
    public virtual void Hide()
    {
        // Gỡ đăng ký phần tử UI khỏi hệ thống GameUI
        GameUI.Instance.Unsubmit(this);

        // Gọi hành động khi bị ẩn
        onHidden?.Invoke();

        if (DestroyOnHide)
        {
            // Nếu cần hủy khi ẩn, gỡ đăng ký và hủy đối tượng
            GameUI.Instance.Unregister(this);
            Destroy(gameObject);
        }
        else
        {
            // Nếu không, chỉ cần ẩn holder
            holder?.SetActive(false);
        }
    }

    // Phương thức Awake, được gọi khi phần tử được khởi tạo
    protected virtual void Awake()
    {
        GameUI.Instance.Register(this);
    }

    // Phương thức chạy hoạt ảnh cho phần tử UI (sử dụng LeanTween)
    protected virtual void Anim(bool show)
    {
        if (show)
        {
            // Đặt alpha bằng 0 rồi tăng lên 1, tạo hiệu ứng fade-in
            canvasGroup.alpha = 0;
            //LeanTween.alphaCanvas(canvasGroup, 1, 0.2f);
        }
        else
        {
            //LeanTween.alphaCanvas(canvasGroup, 0, 0.2f).setOnComplete(Hide);
        }
    }

    // Phương thức ẩn phần tử UI với hoạt ảnh
    public virtual void HideWithAnim()
    {
        Anim(false);
    }
}
