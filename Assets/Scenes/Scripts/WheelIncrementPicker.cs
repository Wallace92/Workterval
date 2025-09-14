using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelPickerOpener : MonoBehaviour
{
    public PopupWheelPicker picker;        // reference to the popup in your Canvas
    public TextMeshProUGUI targetLabel;    // where to display the chosen number
    public bool liveUpdate = true;

    void Reset() { targetLabel = GetComponentInChildren<TextMeshProUGUI>(); }

    void Start()
    {
        // if this is a Button, open on click; else add a custom listener as needed
        var btn = GetComponent<Button>();
        if (btn) btn.onClick.AddListener(Open);
    }

    public void Open()
    {
        int start = 0;
        if (targetLabel && int.TryParse(targetLabel.text, out var v) && v >= 0) start = v;

        picker.OnValueChanged -= OnChanged;
        picker.OnConfirm      -= OnConfirm;
        picker.OnCancel       -= OnCancel;

        if (liveUpdate) picker.OnValueChanged += OnChanged;
        picker.OnConfirm += OnConfirm;
        picker.OnCancel  += OnCancel;

        picker.Show(start);
    }

    void OnChanged(int value)
    {
        if (liveUpdate && targetLabel) targetLabel.text = value.ToString();
    }

    void OnConfirm(int value)
    {
        if (targetLabel) targetLabel.text = value.ToString();
    }

    void OnCancel() { /* no-op */ }
}