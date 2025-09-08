using System;
using TMPro;
using UnityEngine;

namespace Scenes.Scripts
{
    public class OnOffWorkout : MonoBehaviour, IOnOffWorkout
    {
        private enum WorkoutPhase
        {
            On, 
            Off
        }
        
        [SerializeField] 
        private TextMeshProUGUI m_onTime;
        [SerializeField]
        private TextMeshProUGUI m_offTime;
        [SerializeField]
        private TextMeshProUGUI m_rounds;
        [SerializeField]
        private TextMeshProUGUI m_totalTime;
        [SerializeField]
        private TextMeshProUGUI m_nextPhaseText;
        [SerializeField]
        private TextMeshProUGUI m_opfTimeText;

        private bool m_workoutActive;
        private int m_currentRound;
        private float m_phaseRemaining;      
        private float m_totalElapsed;
        private WorkoutPhase m_phase;
        
        public int OnSeconds { get; private set; }
        public int OffSeconds { get; private set; }
        public int Rounds { get; private set; }
        public int TotalSeconds { get; private set; }
        
        private void Update()
        {
            if (!m_workoutActive)
            {
                return;
            }
            
            UpdateTimes();
            
            if (m_phaseRemaining > 0f)
            {
                return;
            }

            if (m_phase == WorkoutPhase.On)
            {
                SwitchToOffPhase();
            }
            else
            {
                SwitchToOnPhase();
            }
        }

        public void StartWorkout(IOnOffWorkout workout)
        {
            OnSeconds = workout.OnSeconds;
            OffSeconds = workout.OffSeconds;
            Rounds = workout.Rounds;
            TotalSeconds = workout.TotalSeconds;
            
            m_currentRound = 1;
            m_phase = WorkoutPhase.On;
            m_phaseRemaining = OnSeconds;
            m_totalElapsed = 0f;
            
            m_workoutActive = true;
            m_onTime.text = FormatMmss(workout.OnSeconds);
            m_offTime.text = FormatMmss(workout.OffSeconds);
            m_rounds.text = $"{m_currentRound}/{Rounds}";
            m_totalTime.text = "00:00";
            
            SetNextPhaseText();
        }

        private void SetNextPhaseText()
        {
            m_nextPhaseText.text = m_phase == WorkoutPhase.On ? "Work" : "Rest";
            m_opfTimeText.text = m_phase == WorkoutPhase.On ? "Rest" : "Work";
        }
        
        private void SwitchToOffPhase()
        {
            m_phase = WorkoutPhase.Off;
            m_phaseRemaining = OffSeconds;

            m_offTime.text = FormatMmss(OnSeconds);
            m_onTime.text = FormatMmss(Mathf.CeilToInt(m_phaseRemaining));
            
            SetNextPhaseText();
        }
        
        private void SwitchToOnPhase()
        {
            m_currentRound++;
            
            m_rounds.text = $"{m_currentRound}/{Rounds}";

            if (m_currentRound > Rounds)
            {
                WorkoutCompleted();
                return;
            }

            m_phase = WorkoutPhase.On;
            m_phaseRemaining = OnSeconds;
            
            SetNextPhaseText();

            m_offTime.text = FormatMmss(OffSeconds); 
            m_onTime.text = FormatMmss(Mathf.CeilToInt(m_phaseRemaining));
        }
        
        private void WorkoutCompleted()
        {
            m_workoutActive = false;
            m_onTime.text = "Completed";
            m_onTime.fontSize = 150;
            m_offTime.transform.parent.gameObject.SetActive(false);
            m_nextPhaseText.text = "Well done!";
            m_rounds.transform.gameObject.SetActive(false);
        }
        
        private void UpdateTimes()
        {
            m_totalElapsed += Time.deltaTime;
            m_totalTime.text = FormatMmss(Mathf.FloorToInt(m_totalElapsed));
            
            m_phaseRemaining -= Time.deltaTime;
            m_onTime.text = FormatMmss(Mathf.Max(0, Mathf.CeilToInt(m_phaseRemaining)));
        }
        
        private static string FormatMmss(int seconds)
        {
            var m = seconds / 60;
            var s = seconds % 60;
            
            return $"{m:00}:{s:00}";
        }
    }
}