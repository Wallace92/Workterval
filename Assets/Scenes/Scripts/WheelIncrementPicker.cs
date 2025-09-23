using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelPickerOpener : MonoBehaviour
{
    public event Action<int> ValueConfirmed = delegate { };
    public event Action WheelOpened = delegate { };
    
    public PopupWheelPicker picker;     
    public TextMeshProUGUI targetLabel;

    [SerializeField]
    private Button m_wheelButton;

    public int Value => int.TryParse(targetLabel.text, out var onVal) ? onVal : 0;

    private void Start()
    {
        m_wheelButton.onClick.AddListener(WheelOpened.Invoke);
        
        ValueConfirmed.Invoke(Value);
    }
    
    private void OnDestroy()
    {
        m_wheelButton.onClick.RemoveListener(WheelOpened.Invoke);
    }

    public void Open(Canvas parent)
    {
        var start = 0;
        
        if (int.TryParse(targetLabel.text, out var v) && v >= 0)
        {
            start = v;
        }

        picker.OnValueChanged -= OnChanged;
        picker.OnConfirm -= OnConfirm;

        picker.OnValueChanged += OnChanged;
        picker.OnConfirm += OnConfirm;

        picker.Show(start, parent.transform);
        
        m_wheelButton.gameObject.SetActive(false);
    }

    private void OnChanged(int value)
    {
        targetLabel.text = value.ToString();
    }

    private void OnConfirm(int value)
    { 
        targetLabel.text = value.ToString();
        m_wheelButton.gameObject.SetActive(true);
        
        ValueConfirmed.Invoke(value);
    }
}