using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuVolumeSlider : Slider, ISubmitHandler
{
    [Header("Slider Handle Sprites")]
    [SerializeField] private Image handleImage;
    [SerializeField] private Sprite focusedSprite;
    [SerializeField] private Sprite editingSprite;

    [Header("Keyboard / Gamepad")]
    [SerializeField] private float volumeStep = 0.05f;

    private Sprite normalSprite;

    private bool mouseOver;
    private bool mousePressed;
    private bool editing;
    private bool isFocused;

    protected override void Awake()
    {
        base.Awake();
        normalSprite = handleImage.sprite;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        RefreshAppearance();
    }

    // JOYSTICK/KEYBOARD SELECTION
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);        
        isFocused = true;

        if (IsInteractable())
        {
            SFXManager.Instance.Play(SFXManager.Instance.menuNavigationSound);
        }

        RefreshAppearance();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);

        isFocused = false;
        editing = false;

        RefreshAppearance();
    }

    // EDITING MODE
    public void OnSubmit(BaseEventData eventData)
    {
        if (!IsActive() || !IsInteractable())
            return;
        
        editing = !editing;
        SFXManager.Instance.Play(SFXManager.Instance.menuConfirmSound);

        RefreshAppearance();
        eventData.Use();
    }

    // NAVIGATION
    public override void OnMove(AxisEventData eventData)
    {
        if (!IsActive() || !IsInteractable())
            return;
        
        if (editing)
        {
            switch (eventData.moveDir)
            {
                case MoveDirection.Left:
                    value -= volumeStep;
                    break;
                case MoveDirection.Right:
                    value += volumeStep;
                    break;
            }
        }
        else
        {
            Navigate(eventData.moveDir);
        }

        eventData.Use();
    }

    private void Navigate(MoveDirection direction)
    {
        Selectable next = null;

        switch (direction)
        {
            case MoveDirection.Up:
                next = FindSelectableOnUp();
                break;
            case MoveDirection.Down:
                next = FindSelectableOnDown();
                break;
            case MoveDirection.Left:
                next = FindSelectableOnLeft();
                break;
            case MoveDirection.Right:
                next = FindSelectableOnRight();
                break;
        }

        if (next != null && next.IsActive() && next.IsInteractable())
        {
            EventSystem.current.SetSelectedGameObject(next.gameObject);
        }
    }

    // MOUSE INTERACTION
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsActive() || !IsInteractable())
            return;

        base.OnPointerEnter(eventData);
        mouseOver = true;

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        
        RefreshAppearance();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        mouseOver = false;
        RefreshAppearance();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!IsActive() || !IsInteractable() || eventData.button != PointerEventData.InputButton.Left)
            return;
        
        base.OnPointerDown(eventData);

        editing = false;
        mousePressed = true;

        SFXManager.Instance.Play(SFXManager.Instance.menuConfirmSound);
        RefreshAppearance();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            mousePressed = false;
            RefreshAppearance();
        }
    }

    // HANDLE APPEARANCE
    public void RefreshAppearance()
    {
        if (handleImage == null)
            return;

        bool available = IsActive() && IsInteractable();
        bool selected = isFocused;

        if (!available)
        {
            handleImage.sprite = normalSprite;
        }
        else if (mousePressed || editing)
        {
            handleImage.sprite = editingSprite;
        }
        else if ((mouseOver && Cursor.visible) || isFocused)
        {
            handleImage.sprite = focusedSprite;
        }
        else
        {
            handleImage.sprite = normalSprite;
        }
    }

    protected override void OnDisable()
    {
        mouseOver = false;
        mousePressed = false;
        editing = false;
        isFocused = false;

        base.OnDisable();
        RefreshAppearance();
    }
}
