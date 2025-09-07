using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts.Views
{
    public class WorkoutTilePreview : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private Image m_icon;

        [SerializeField]
        private TextMeshProUGUI m_title;
        [SerializeField]
        private TextMeshProUGUI m_description;

        public void Initialize(IExercise workout)
        {
            m_icon.sprite = workout.Icon;
            m_title.text = workout.Title;
            m_description.text = workout.ToString();
        }

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}