using System;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts.Views
{
    public class ExerciseTileView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private Button rootButton;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI title;

        [SerializeField]
        private TextMeshProUGUI description;

        [SerializeField]
        private GameObject editorPanel;

        [SerializeField]
        private TextMeshProUGUI editorLabel;

        [SerializeField]
        private TMP_InputField valueInput;

        private Action<IExercise> onClicked;
        private IExercise m_exercise;

        public void Bind(IExercise exercise, Action<IExercise> clicked)
        {
            m_exercise = exercise;
            onClicked = clicked;

            icon.sprite = m_exercise.Icon;
            title.text = m_exercise.Title;

            rootButton.onClick.RemoveAllListeners();
            rootButton.onClick.AddListener(() =>
            {
                ToggleEditor();
                onClicked?.Invoke(exercise);
            });

            valueInput.onEndEdit.RemoveAllListeners();
            valueInput.onEndEdit.AddListener(_ => NotifyValueChanged());

 
            valueInput.text = m_exercise.Description;
            editorPanel.SetActive(false);
        }
        

        private void ToggleEditor() => editorPanel.SetActive(!editorPanel.activeSelf);

        private void NotifyValueChanged()
        {
            int.TryParse(valueInput.text, out var iv);
            float.TryParse(valueInput.text, out var fv);
            // wyślij obie formy – kontroler użyje właściwej
            //onValueChanged?.Invoke(def, Mathf.Max(1, iv), Mathf.Max(1f, fv));
        }
    }
}