using UnityEngine;

namespace Scenes.Scripts.Data
{
    [CreateAssetMenu(menuName = "Workout/Exercise Definition")]
    public class ExerciseDefinition : ScriptableObject
    {
        public string Title;
        public Sprite Image;
        public ExerciseType ExerciseType;
    }
}