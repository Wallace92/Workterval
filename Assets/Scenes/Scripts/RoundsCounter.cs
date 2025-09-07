using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class RoundsCounter : MonoBehaviour
    {
        public event Action<int> RoundsChanged = delegate { };
        
        [Header("UI")]
        [SerializeField] 
        private Button m_incRoundsButton;
        [SerializeField] 
        private Button m_decRoundsButton;
        [SerializeField] 
        private TextMeshProUGUI m_roundsTMP;

        [Header("Config")]
        private int m_roundValue = 1;
        private readonly int m_step = 1;
        
        public int Rounds => m_roundValue;

        private void Awake()
        {
            m_incRoundsButton.onClick.AddListener(OnIncRoundsClicked);
            m_decRoundsButton.onClick.AddListener(OnDecRoundsClicked);

            m_decRoundsButton.interactable = false;
            m_roundsTMP.text = m_roundValue.ToString();
        }

        private void OnDestroy()
        {
            m_incRoundsButton.onClick.RemoveListener(OnIncRoundsClicked);
            m_decRoundsButton.onClick.RemoveListener(OnDecRoundsClicked);
        }
        
        private void OnDecRoundsClicked()
        {
            SetRounds(m_roundValue - m_step);
        }

        private void OnIncRoundsClicked()
        {
            SetRounds(m_roundValue + m_step);
        }

        private void SetRounds(int rounds)
        {
            m_decRoundsButton.interactable = rounds > 1;
            
            m_roundValue = rounds;
            m_roundsTMP.text = rounds.ToString();
            
            RoundsChanged(Rounds);
        }
    }
}