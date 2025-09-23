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
        private WheelPickerOpener m_onWheelPickerOpener;
        [SerializeField]
        private WheelPickerOpener m_offWheelPickerOpener;
        [SerializeField]
        private Transform m_workoutContainer;
        [SerializeField] 
        private TextMeshProUGUI m_workoutTime;
        [SerializeField]
        private RoundsCounter m_roundsCounter;
        [SerializeField]
        private Canvas m_canvas;

        public int OnSeconds => m_onWheelPickerOpener.Value;
        public int OffSeconds => m_offWheelPickerOpener.Value;
        public int Rounds => m_roundsCounter.Rounds;
        public int TotalSeconds => (OnSeconds + OffSeconds) * Rounds;
        
        private void Awake()
        {
            m_activationButton.onClick.AddListener(OnActivationButtonClicked);
            
            m_onWheelPickerOpener.ValueConfirmed += OnOnValueConfirmed;
            m_offWheelPickerOpener.ValueConfirmed += OnOffValueConfirmed;
            
            m_onWheelPickerOpener.WheelOpened += OnOnWheelOpened;
            m_offWheelPickerOpener.WheelOpened += OnOffWheelOpened;
            
            m_roundsCounter.RoundsChanged += OnRoundsChanged;

            SetTotalTime();
        }

        private void OnDestroy()
        {
            m_activationButton.onClick.RemoveListener(OnActivationButtonClicked);
            
            m_onWheelPickerOpener.ValueConfirmed -= OnOnValueConfirmed;
            m_offWheelPickerOpener.ValueConfirmed -= OnOffValueConfirmed;
            
            m_onWheelPickerOpener.WheelOpened -= OnOnWheelOpened;
            m_offWheelPickerOpener.WheelOpened -= OnOffWheelOpened;
       
            m_roundsCounter.RoundsChanged -= OnRoundsChanged;
        }

        private void OnOffWheelOpened()
        {
            m_offWheelPickerOpener.Open(m_canvas);
        }

        private void OnOnWheelOpened()
        {
            m_onWheelPickerOpener.Open(m_canvas);
        }

        private void OnRoundsChanged()
        {
            SetTotalTime();
        }

        private void OnOnValueConfirmed(int arg0)
        {
            SetTotalTime();
        }

        private void OnOffValueConfirmed(int arg0)
        {
            SetTotalTime();
        }

        private void SetTotalTime()
        {
            var roundsCounter = m_roundsCounter.Rounds;
            var onSeconds = m_onWheelPickerOpener.Value;
            var offSeconds = m_offWheelPickerOpener.Value;
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