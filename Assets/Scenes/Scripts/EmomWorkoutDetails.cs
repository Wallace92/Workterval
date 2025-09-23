using TMPro;
using UnityEngine;

namespace Scenes.Scripts
{
    public class EmomWorkoutDetails : WorkoutDetails, IEmomWorkout
    {
        [SerializeField]
        private RoundsCounter m_roundsCounter;
        [SerializeField]
        private WheelPickerOpener m_roundTimeWheel;
        [SerializeField] 
        private TextMeshProUGUI m_workoutTime;
        
        public int Rounds => m_roundsCounter.Rounds;
        public int RoundSeconds => m_roundTimeWheel.Value;
        
        protected override void Awake()
        {
            base.Awake();
            
            m_roundTimeWheel.ValueConfirmed += OnOnValueConfirmed;
            m_roundTimeWheel.WheelOpened += OnOnWheelOpened;
            m_roundsCounter.RoundsChanged += OnRoundsChanged;
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_roundTimeWheel.ValueConfirmed -= OnOnValueConfirmed;
            m_roundTimeWheel.WheelOpened -= OnOnWheelOpened;
            m_roundsCounter.RoundsChanged -= OnRoundsChanged;
        }
        
        private void OnOnWheelOpened()
        {
            m_roundTimeWheel.Open(Canvas);
        }
        
        private void OnRoundsChanged()
        {
            SetTotalTime();
        }
        
        private void OnOnValueConfirmed(int arg0)
        {
            SetTotalTime();
        }
        
        private void SetTotalTime()
        {
            var roundsCounter = m_roundsCounter.Rounds;
            var roundTime = m_roundTimeWheel.Value;
            var totalSeconds = roundTime * roundsCounter;

            var minutes = totalSeconds / 60;
            var seconds = totalSeconds % 60;

            m_workoutTime.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}