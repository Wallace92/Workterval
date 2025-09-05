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
        private TMP_InputField valueInput;
        
        private IExercise m_exercise;
        public IExercise Exercise => m_exercise;

        public void Awake()
        {
            valueInput.onEndEdit.AddListener(_ => NotifyValueChanged());
        }
        
        public void OnDestroy()
        {
            valueInput.onEndEdit.RemoveAllListeners();
        }
        

        private void NotifyValueChanged()
        {
            int.TryParse(valueInput.text, out var iv);
            float.TryParse(valueInput.text, out var fv);
            // wyślij obie formy – kontroler użyje właściwej
            //onValueChanged?.Invoke(def, Mathf.Max(1, iv), Mathf.Max(1f, fv));
        }

        public void Initialize(IExercise exercise)
        {
            m_exercise = exercise;
            
            icon.sprite = m_exercise.Icon;
            title.text = m_exercise.Title;
        }
    }
}