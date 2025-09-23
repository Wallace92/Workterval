using TMPro;
using UnityEngine;

namespace Scenes.Scripts
{
    public class OnOffWorkoutDetails : WorkoutDetails, IOnOffWorkout
    {
        [SerializeField]
        private WheelPickerOpener m_onWheelPickerOpener;
        [SerializeField]
        private WheelPickerOpener m_offWheelPickerOpener;
        [SerializeField] 
        private TextMeshProUGUI m_workoutTime;
        [SerializeField]
        private RoundsCounter m_roundsCounter;
        
        public int OnSeconds => m_onWheelPickerOpener.Value;
        public int OffSeconds => m_offWheelPickerOpener.Value;
        public int Rounds => m_roundsCounter.Rounds;
        public int TotalSeconds => (OnSeconds + OffSeconds) * Rounds;
        
        protected override void Awake()
        {
            base.Awake();
            
            m_onWheelPickerOpener.ValueConfirmed += OnOnValueConfirmed;
            m_offWheelPickerOpener.ValueConfirmed += OnOffValueConfirmed;
            
            m_onWheelPickerOpener.WheelOpened += OnOnWheelOpened;
            m_offWheelPickerOpener.WheelOpened += OnOffWheelOpened;
            
            m_roundsCounter.RoundsChanged += OnRoundsChanged;

            SetTotalTime();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_onWheelPickerOpener.ValueConfirmed -= OnOnValueConfirmed;
            m_offWheelPickerOpener.ValueConfirmed -= OnOffValueConfirmed;
            
            m_onWheelPickerOpener.WheelOpened -= OnOnWheelOpened;
            m_offWheelPickerOpener.WheelOpened -= OnOffWheelOpened;
       
            m_roundsCounter.RoundsChanged -= OnRoundsChanged;
        }

        private void OnOffWheelOpened()
        {
            m_offWheelPickerOpener.Open(Canvas);
        }

        private void OnOnWheelOpened()
        {
            m_onWheelPickerOpener.Open(Canvas);
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
    }
}