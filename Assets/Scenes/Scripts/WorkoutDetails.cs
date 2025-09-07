using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class WorkoutDetails : MonoBehaviour, IOnOffWorkout
    {
        [SerializeField]
        private Button m_activationButton;
        [SerializeField]
        private TMP_InputField m_onInputField;
        [SerializeField]
        private TMP_InputField m_offInputField;
        [SerializeField]
        private Transform m_workoutContainer;
        [SerializeField] 
        private TextMeshProUGUI m_workoutTime;
        [SerializeField]
        private RoundsCounter m_roundsCounter;
        
        public int OnSeconds => int.TryParse(m_onInputField.text, out var onVal) ? onVal : 0;
        public int OffSeconds => int.TryParse(m_offInputField.text, out var offVal) ? offVal : 0;
        public int Rounds => m_roundsCounter.Rounds;
        public int TotalSeconds => (OnSeconds + OffSeconds) * Rounds;
        
        private void Awake()
        {
            m_activationButton.onClick.AddListener(OnActivationButtonClicked);
            m_onInputField.onEndEdit.AddListener(OnOnEdited);
            m_offInputField.onEndEdit.AddListener(OnOffEdited);
            
            m_roundsCounter.RoundsChanged += OnRoundsChanged;
        }

        private void OnDestroy()
        {
            m_activationButton.onClick.RemoveListener(OnActivationButtonClicked);
            m_onInputField.onEndEdit.RemoveListener(OnOnEdited);
            m_offInputField.onEndEdit.RemoveListener(OnOffEdited);
            
            m_roundsCounter.RoundsChanged -= OnRoundsChanged;
        }

        private void OnRoundsChanged()
        {
            SetTotalTime();
        }

        private void OnOffEdited(string arg0)
        {
            SetTotalTime();
        }

        private void OnOnEdited(string arg0)
        {
            SetTotalTime();
        }

        private void SetTotalTime()
        {
            var roundsCounter = m_roundsCounter.Rounds;
            var onSeconds = int.TryParse(m_onInputField.text, out var onVal) ? onVal : 0;
            var offSeconds = int.TryParse(m_offInputField.text, out var offVal) ? offVal : 0;
            var totalSeconds = (onSeconds + offSeconds) * roundsCounter;

            var minutes = totalSeconds / 60;
            var seconds = totalSeconds % 60;

            m_workoutTime.text = $"{minutes:D2}:{seconds:D2}";
        }

        private void OnActivationButtonClicked()
        {
            m_workoutContainer.gameObject.SetActive(!m_workoutContainer.gameObject.activeSelf);
        }
    }
}