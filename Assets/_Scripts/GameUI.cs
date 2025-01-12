using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.EventSystems;
#endif

// Lớp GameUI kế thừa từ Singleton<GameUI>
public class GameUI : Singleton<GameUI>
{
    // Danh sách các UIElement đang hoạt động
    private List<UIElement> ActiveElements;
    
    // Từ điển chứa các UIElement dựa trên kiểu của chúng
    private Dictionary<Type, UIElement> Elements;
    
    // Tham chiếu tới FrontPanel và BehindPanel
    private RectTransform FrontPanel, BehindPanel;
    
    // Đường dẫn tới thư mục chứa các prefab của UIElement
    private readonly string FolderPath = "GameUI/";

    // Thuộc tính tính toán số lượng UIElement đang hoạt động
    public int ActiveCount => ActiveElements.Count;

    // Phương thức Awake, được gọi khi GameUI khởi tạo
    protected override void Awake()
    {
        base.Awake();
        
        // Khởi tạo danh sách và từ điển
        ActiveElements = new List<UIElement>();
        Elements = new Dictionary<Type, UIElement>();
        
        // Tạo các panel Front và Behind
        CreateFrontAndBehindPanel();
    }

    // Phương thức ẩn tất cả các UIElement đang hoạt động
    public void HideAll()
    {
        // Ẩn từng UIElement trong danh sách ActiveElements
        while (ActiveElements.Count > 0)
            ActiveElements[0].Hide();
    }

    // Phương thức ẩn UIElement đang ở trên cùng
    public void HideOnTop()
    {
        int activeCount = ActiveCount;
        if (activeCount == 0) return;
        
        // Lấy UIElement ở vị trí trên cùng và ẩn nó nếu không cần ẩn thủ công
        UIElement element = ActiveElements[--activeCount];
        if (!element.ManualHide) element.Hide();
    }

    // Phương thức Block, chặn và hiển thị hay ẩn FrontPanel
    public void Block(bool value)
    {
        FrontPanel.gameObject.SetActive(value);
        if (value) FrontPanel.SetAsLastSibling();
        else FrontPanel.SetAsFirstSibling();
    }

    // Phương thức lấy UIElement theo kiểu Element được chỉ định
    public Element Get<Element>() where Element : UIElement
    {
        Type type = typeof(Element);

        // Kiểm tra xem Element đã có trong từ điển chưa
        if (Elements.ContainsKey(type)) return Elements[type] as Element;

        // Nếu chưa có, tải prefab của Element từ Resources
        Element prefab = Resources.Load<Element>(FolderPath + type.Name);
        
        // Tạo một instance mới của Element
        Element element = prefab != null ? Instantiate(prefab) : default;
        
        // Đặt parent cho Element nếu instance được tạo thành công
        if (element != null) SetParent(element.transform, transform);
        
        return element;
    }

    // Phương thức kiểm tra xem UIElement có hoạt động không dựa trên tên
    public bool CheckUIElent(string elementName)
    {
        foreach (var element in Elements.Values)
        {
            if (element.GetType().Name == elementName)
            {
                return ActiveElements.Contains(element);
            }
        }

        return false;
    }

    // Phương thức nộp UIElement vào danh sách ActiveElements
    public void Submit(UIElement element)
    {
        if (ActiveElements.Contains(element)) return;
        
        ActiveElements.Add(element);
        element.transform.SetAsLastSibling();
        
        UpdateBehindPanelSibling();
    }

    // Phương thức gỡ bỏ UIElement khỏi danh sách ActiveElements
    public void Unsubmit(UIElement element)
    {
        if (ActiveElements.Remove(element)) UpdateBehindPanelSibling();
    }

    // Phương thức đăng ký UIElement vào từ điển Elements
    public void Register(UIElement element)
    {
        Type type = element.GetType();
        if (Elements.ContainsKey(type)) return;

        Elements.Add(type, element);
    }

    // Phương thức gỡ đăng ký UIElement khỏi từ điển Elements
    public void Unregister(UIElement element)
    {
        Elements.Remove(element.GetType());
    }

    // Phương thức cập nhật thứ tự của BehindPanel
    private void UpdateBehindPanelSibling()
    {
        BehindPanel.SetParent(transform);
        for (int i = ActiveCount - 1; i >= 0; i--)
        {
            if (!ActiveElements[i].UseBehindPanel) continue;

            // Nếu có phần tử sử dụng BehindPanel, đặt nó làm con của phần tử và hiện BehindPanel
            BehindPanel.gameObject.SetActive(true);
            SetParent(BehindPanel, ActiveElements[i].transform);
            BehindPanel.SetAsFirstSibling();
            return;
        }

        // Ẩn BehindPanel nếu không có phần tử nào sử dụng nó
        BehindPanel.gameObject.SetActive(false);
    }

    // Phương thức tạo panel Front và Behind
    private void CreateFrontAndBehindPanel()
    {
        FrontPanel = CreatePanel("FrontPanel", new Color(1, 1, 1, 0));
        BehindPanel = CreatePanel("BehindPanel", new Color(0, 0, 0, 0.9725f));

        Button button = BehindPanel.gameObject.AddComponent<Button>();
        button.transition = Selectable.Transition.None;
        button.onClick.AddListener(HideOnTop);
    }

    // Phương thức đặt parent cho transform
    private void SetParent(Transform target, Transform parent)
    {
        target.SetParent(parent, false);
        target.localScale = Vector3.one;
        target.localPosition = Vector3.zero;
    }

    // Phương thức hiển thị UIElement ở trên cùng
    public void ShowOnTop(UIElement element)
    {
        if (ActiveElements.Contains(element))
        {
            ActiveElements.Remove(element); // Loại bỏ phần tử khỏi vị trí hiện tại
        }

        ActiveElements.Add(element); // Thêm lại vào cuối danh sách (vị trí trên cùng)
        element.transform.SetAsLastSibling(); // Đưa nó lên vị trí cuối cùng trong thứ tự Sibling
        UpdateBehindPanelSibling(); // Cập nhật BehindPanel nếu cần thiết
    }

    // Phương thức tạo panel với tên và màu nhất định
    private RectTransform CreatePanel(string name, Color color)
    {
        GameObject panelObject = new GameObject(name);
        panelObject.AddComponent<Image>().color = color;
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        SetParent(panelRect, transform);
        panelRect.anchorMax = Vector2.one;
        panelRect.anchorMin = Vector2.zero;
        panelRect.offsetMax = Vector2.one * 2;
        panelRect.offsetMin = Vector2.one * -2;
        panelObject.SetActive(false);

        return panelRect;
    }

#if UNITY_EDITOR
    // MenuItem trong Unity Editor để tạo GameUI
    [MenuItem("GameObject/UI/GameUI", priority = 0)]
    private static void CreateContext()
    {
        GameObject canvasObject = new GameObject("MainCanvas");
        canvasObject.layer = 5;
        
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 10;

        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(720, 1280);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

        canvasObject.AddComponent<GraphicRaycaster>();
        canvasObject.AddComponent<GameUI>();

        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null) return;

        GameObject eventSystemObject = new GameObject("EventSystem");
        eventSystemObject.AddComponent<EventSystem>();
        eventSystemObject.AddComponent<StandaloneInputModule>();

        Selection.activeObject = canvasObject;
    }
#endif
}
