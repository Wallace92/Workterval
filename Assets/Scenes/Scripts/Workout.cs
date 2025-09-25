using System.Collections;
using Scenes.Scripts.Data.WorkoutStates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class Workout : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_onTime;
        [SerializeField]
        private TextMeshProUGUI m_offTime;
        [SerializeField]
        private TextMeshProUGUI m_roundsText;
        [SerializeField]
        private TextMeshProUGUI m_workoutTime;
        [SerializeField]
        private TextMeshProUGUI m_totalTime;
        [SerializeField]
        private TextMeshProUGUI m_nextPhaseText;
        [SerializeField]
        private TextMeshProUGUI m_opfTimeText;
        [SerializeField]
        private Image m_bigTimeImage;
        [SerializeField]
        private Button m_resetBtn;
        [SerializeField]
        private Button m_stopBtn;
        [SerializeField]
        private Button m_playBtn;

        private IWorkout m_workout;
        private int m_onSeconds;
        private int m_offSeconds;
        private int m_rounds;
        private int m_totalSeconds;

        private bool m_isPaused;
        private IWorkoutState m_state;
        private Coroutine m_stateRoutine;
        
        public bool HasRest => m_offSeconds > 0;
        public int OnSeconds => m_onSeconds;
        public int OffSeconds => m_offSeconds;
        public int Rounds => m_rounds;

        public int CurrentRound { get; set; }

        public float TotalElapsed { get; private set; }

        private void Awake()
        {
            m_resetBtn.onClick.AddListener(OnResetClicked);
            m_stopBtn.onClick.AddListener(OnStopClicked);
            m_playBtn.onClick.AddListener(OnPlayClicked);
        }

        private void OnDestroy()
        {
            m_resetBtn.onClick.RemoveListener(OnResetClicked);
            m_stopBtn.onClick.RemoveListener(OnStopClicked);
            m_playBtn.onClick.RemoveListener(OnPlayClicked);
            
            if (m_stateRoutine != null)
            {
                StopCoroutine(m_stateRoutine);
            }
        }

        public void StartWorkout(IWorkout workout)
        {
            m_workout = workout;

            if (workout is IOnOffWorkout onOffWorkout)
            {
                m_onSeconds = onOffWorkout.OnSeconds;
                m_offSeconds = onOffWorkout.OffSeconds;
                m_rounds = onOffWorkout.Rounds;
                m_totalSeconds = onOffWorkout.TotalSeconds;
                m_opfTimeText.transform.parent.gameObject.SetActive(true);
            }
            else if (workout is IEmomWorkout emomWorkout)
            {
                m_onSeconds = emomWorkout.RoundSeconds;
                m_offSeconds = 0;
                m_rounds = emomWorkout.Rounds;
                m_totalSeconds = emomWorkout.RoundSeconds * emomWorkout.Rounds;
                m_opfTimeText.transform.parent.gameObject.SetActive(false);
                m_offTime.transform.parent.gameObject.SetActive(false);
            }

            CurrentRound = 1;
            TotalElapsed = 0f;
           
            SetPlayVisibility(false);
            TransitionTo(new PreparationState(this));
        }

        public void TransitionTo(IWorkoutState next)
        {
            if (m_state != null)
            {
                m_state.Exit();
            }
            
            if (m_stateRoutine != null)
            {
                StopCoroutine(m_stateRoutine);
                m_stateRoutine = null;
            }
            
            m_state = next;
            m_state.Enter();
            
            m_stateRoutine = StartCoroutine(m_state.Run());
        }

        public IEnumerator Countdown(float duration, bool addToTotal, System.Action<float> onFrame)
        {
            var remaining = duration;
            
            while (remaining > 0f)
            {
                if (!m_isPaused)
                {
                    remaining -= Time.deltaTime;
                   
                    if (addToTotal)
                    {
                        TotalElapsed += Time.deltaTime;
                    }
                    
                    onFrame?.Invoke(Mathf.Max(0f, remaining));
                }
                
                yield return null;
            }
        }

        public void ShowPreparation()
        {
            m_nextPhaseText.text = "Get Ready";
            m_bigTimeImage.color = Color.yellow;
            m_roundsText.transform.gameObject.SetActive(true);
            m_roundsText.text = $"{m_rounds} rounds";
            m_workoutTime.text = "00:00";
            m_totalTime.text = FormatMmss(m_totalSeconds);
            
            if (HasRest)
            {
                m_offTime.text = FormatMmss(m_offSeconds);
                m_opfTimeText.text = "Work";
            }
        }

        public void UI_ShowWorkoutHeader()
        {
            m_bigTimeImage.color = Color.green;
            m_totalTime.text = FormatMmss(m_totalSeconds);
            m_workoutTime.text = "00:00";
            m_roundsText.text = $"{CurrentRound}/{m_rounds}";
            m_roundsText.transform.gameObject.SetActive(true);
            
            if (HasRest)
            {
                m_offTime.text = FormatMmss(m_offSeconds);
                UI_SetPhaseTextCurrent(false);
            }
        }

        public void UI_SetPhaseTextCurrent(bool currentIsWork)
        {
            m_nextPhaseText.text = currentIsWork ? "Work" : "Rest";
            m_opfTimeText.text = currentIsWork ? "Rest" : "Work";
        }

        public void SetOnTime(string t)
        {
            m_onTime.text = t;
        }

        public void UI_SetOffTime(string t)
        {
            m_offTime.text = t;
        }

        public void UI_SetWorkoutTime(string t)
        {
            m_workoutTime.text = t;
        }

        public void UI_SetRounds(string t)
        {
            m_roundsText.text = t;
        }

        public void UI_SetOffGroupVisible(bool v)
        {
            m_offTime.transform.parent.gameObject.SetActive(v);
        }

        public void UI_Completed()
        {
            m_onTime.text = "Completed";
            m_onTime.fontSize = 150;
            m_offTime.transform.parent.gameObject.SetActive(false);
            m_nextPhaseText.text = "Well done!";
            m_roundsText.transform.gameObject.SetActive(false);
            SetPlayVisibility(true);
        }

        public static string FormatMmss(int seconds)
        {
            var m = seconds / 60;
            var s = seconds % 60;
            return $"{m:00}:{s:00}";
        }
        
        private void OnResetClicked()
        {
            StartWorkout(m_workout);
        }

        private void OnStopClicked()
        {
            SetPlayVisibility(true);
            m_isPaused = true;
        }

        private void OnPlayClicked()
        {
            SetPlayVisibility(false);
            m_isPaused = false;
        }

        private void SetPlayVisibility(bool showPlay)
        {
            m_playBtn.gameObject.SetActive(showPlay);
            m_stopBtn.gameObject.SetActive(!showPlay);
        }
    }
}
