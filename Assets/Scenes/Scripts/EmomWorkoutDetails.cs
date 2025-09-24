using TMPro;
using UnityEngine;

namespace Scenes.Scripts
{
    public class EmomWorkoutDetails : WorkoutDetails, IEmomWorkout
    {
        [SerializeField]
        private RoundsCounter m_roundsCounter;
        [SerializeField]
        private WheelPickerOpener m_roundTimeMinWheel;
        [SerializeField]
        private WheelPickerOpener m_roundTimeSecondsWheel;
        [SerializeField] 
        private TextMeshProUGUI m_workoutTime;
        
        public int Rounds => m_roundsCounter.Rounds;
        public int RoundSeconds => m_roundTimeMinWheel.Value * 60 + m_roundTimeSecondsWheel.Value;
        
        protected override void Awake()
        {
            base.Awake();
            
            m_roundTimeMinWheel.ValueConfirmed += OnOnValueConfirmed;
            m_roundTimeMinWheel.WheelOpened += OnOnMinWheelOpened;
            
            m_roundTimeSecondsWheel.ValueConfirmed += OnOnValueConfirmed;
            m_roundTimeSecondsWheel.WheelOpened += OnOnSecondsWheelOpened;
            m_roundsCounter.RoundsChanged += OnRoundsChanged;
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_roundTimeMinWheel.ValueConfirmed -= OnOnValueConfirmed;
            m_roundTimeMinWheel.WheelOpened -= OnOnMinWheelOpened;
            
            m_roundTimeSecondsWheel.ValueConfirmed -= OnOnValueConfirmed;
            m_roundTimeSecondsWheel.WheelOpened -= OnOnSecondsWheelOpened;
            
            m_roundsCounter.RoundsChanged -= OnRoundsChanged;
        }

        private void OnOnMinWheelOpened()
        {
            m_roundTimeMinWheel.Open(Canvas);
        }
        
        private void OnOnSecondsWheelOpened()
        {
            m_roundTimeSecondsWheel.Open(Canvas);
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
            var roundMinTime = m_roundTimeMinWheel.Value;
            var roundSecondsTime = m_roundTimeSecondsWheel.Value;
            var totalSeconds = (roundMinTime * 60 + roundSecondsTime) * roundsCounter;

            var minutes = totalSeconds / 60;
            var seconds = totalSeconds % 60;

            m_workoutTime.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}