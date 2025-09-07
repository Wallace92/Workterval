using TMPro;
using UnityEngine;

namespace Scenes.Scripts
{
    public class OnOffWorkout : MonoBehaviour, IOnOffWorkout
    {
        public int OnSeconds { get; private set; }
        public int OffSeconds { get; private set; }
        public int Rounds { get; private set; }
        public int TotalSeconds { get; private set; }
        
        [SerializeField] 
        private TextMeshProUGUI m_onTime;
        [SerializeField]
        private TextMeshProUGUI m_offTime;
        [SerializeField]
        private TextMeshProUGUI m_rounds;
        [SerializeField]
        private TextMeshProUGUI m_totalTime;

        private bool m_workoutActive;
        private bool m_isOnPhase;          
        private int m_currentRound;
        private float m_phaseRemaining;      
        private float m_totalElapsed;    
        
        private void Update()
        {
            if (!m_workoutActive)
            {
                return;
            }
            
            m_totalElapsed += Time.deltaTime;
            m_totalTime.text = FormatMmss(Mathf.FloorToInt(m_totalElapsed));

            m_phaseRemaining -= Time.deltaTime;
            m_onTime.text = Mathf.Max(0, Mathf.CeilToInt(m_phaseRemaining)).ToString();

            if (m_phaseRemaining > 0f)
            {
                return;
            }

            if (m_isOnPhase)
            {
                m_isOnPhase = false;
                m_phaseRemaining = OffSeconds;

                m_offTime.text = OnSeconds.ToString(); 
                m_onTime.text = Mathf.CeilToInt(m_phaseRemaining).ToString(); 
            }
            else
            {
                m_currentRound++;
                m_rounds.text = $"{Mathf.Min(m_currentRound, Rounds)}/{Rounds}";

                if (m_currentRound > Rounds)
                {
                    m_workoutActive = false;
                    m_onTime.text = "0";
                    return;
                }

                m_isOnPhase = true;
                m_phaseRemaining = OnSeconds;

                m_offTime.text = OffSeconds.ToString(); 
                m_onTime.text = Mathf.CeilToInt(m_phaseRemaining).ToString();
            }
        }
        
        public void StartWorkout(IOnOffWorkout workout)
        {
            OnSeconds = workout.OnSeconds;
            OffSeconds = workout.OffSeconds;
            Rounds = workout.Rounds;
            TotalSeconds = workout.TotalSeconds;
            
            m_currentRound = 1;
            m_isOnPhase = true;
            m_phaseRemaining = OnSeconds;
            m_totalElapsed = 0f;
            
            m_workoutActive = true;
            m_onTime.text = workout.OnSeconds.ToString();
            m_rounds.text = $"{m_currentRound}/{Rounds}";
            m_totalTime.text = "00:00";
        }
        
        private static string FormatMmss(int seconds)
        {
            int m = seconds / 60;
            int s = seconds % 60;
            return $"{m:00}:{s:00}";
        }
    }
}