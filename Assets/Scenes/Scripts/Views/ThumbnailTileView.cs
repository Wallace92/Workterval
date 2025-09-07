using System;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts.Views
{
    public class ThumbnailTileView : MonoBehaviour
    {
        public event Action<ThumbnailTileView> ThumbnailClicked = delegate { };
        
        [Header("UI")]
        [SerializeField]
        private Image m_icon;

        [SerializeField]
        private TextMeshProUGUI m_title;
        [SerializeField]
        private TextMeshProUGUI m_description;
        
        [SerializeField]
        private Button m_thumbnailButton;
        
        private IExercise m_exercise;
        public IExercise Exercise => m_exercise;

        private void Awake()
        {
            m_thumbnailButton.onClick.AddListener(OnThumbnailButton);
        }

        private void OnDestroy()
        {
            m_thumbnailButton.onClick.RemoveListener(OnThumbnailButton);
        }

        private void OnThumbnailButton()
        {
            ThumbnailClicked?.Invoke(this);
        }

        public void Initialize(IExercise exercise)
        {
            m_exercise = exercise;
            m_icon.sprite = exercise.Icon;
            m_title.text = exercise.Title;
        }

        public void Display(bool isActive)
        {
            m_icon.color = isActive ? Color.green : Color.white;
        }
    }
}