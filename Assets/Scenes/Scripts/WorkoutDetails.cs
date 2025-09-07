using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class WorkoutDetails : MonoBehaviour
    {
        [SerializeField]
        private Button m_activationButton;
        [SerializeField]
        private TMP_InputField m_onInputField;
        [SerializeField]
        private TMP_InputField m_offInputField;
        [SerializeField]
        private Button m_incRoundsButton;
        [SerializeField]
        private Button m_decRoundsButton;
        [SerializeField]
        private TextMeshProUGUI m_roundsTMP;
        [SerializeField]
        private Transform m_workoutContainer;
        [SerializeField]
        private RoundsCounter m_roundsCounter;
        private void Awake()
        {
            m_activationButton.onClick.AddListener(OnActivationButtonClicked);
            m_onInputField.onEndEdit.AddListener(OnOnEdited);
            m_offInputField.onEndEdit.AddListener(OnOffEdited);
            
            m_roundsCounter.RoundsChanged += OnRoundsChanged;
        }

        private void OnRoundsChanged(int rounds)
        {
            Debug.Log($"OnRoundsChanged: {rounds}");
        }

        private void OnDestroy()
        {
            m_activationButton.onClick.RemoveListener(OnActivationButtonClicked);
            m_onInputField.onEndEdit.RemoveListener(OnOnEdited);
            m_offInputField.onEndEdit.RemoveListener(OnOffEdited);
        }

        private void OnOffEdited(string arg0)
        {
           //
        }

        private void OnOnEdited(string arg0)
        {
            //throw new NotImplementedException();
        }

        private void OnActivationButtonClicked()
        {
            m_workoutContainer.gameObject.SetActive(!m_workoutContainer.gameObject.activeSelf);
        }
    }
}