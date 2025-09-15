using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelPickerOpener : MonoBehaviour
{
    public event Action<int> ValueConfirmed = delegate { };
    
    public PopupWheelPicker picker;     
    public TextMeshProUGUI targetLabel;

    [SerializeField]
    private Button m_wheelButton;
    
    private void Start()
    {
        m_wheelButton.onClick.AddListener(Open);
    }

    private void Open()
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

        picker.Show(start);
        
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