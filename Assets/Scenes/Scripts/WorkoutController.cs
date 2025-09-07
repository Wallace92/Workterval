using System.Collections.Generic;
using System.Linq;
using Scenes.Scripts.Data;
using Scenes.Scripts.Factories;
using Scenes.Scripts.Models;
using UnityEngine;

namespace Scenes.Scripts
{
    public class WorkoutController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] 
        private WorkoutData m_workoutData;
        [SerializeField] 
        private WorkoutControllerView m_workoutControllerView;
        
        private List<IExercise> m_exercises = new();

        private void Awake()
        {
            m_exercises = m_workoutData.Exercises
                .Select(ExerciseFactory.Create)
                .ToList();
            
            m_workoutControllerView.ShowThumbnails(m_exercises);
            
            m_workoutControllerView.WorkoutStarted += OnWorkoutStarted;
        }

        private void OnWorkoutStarted(IExercise[] exercises, IWorkout workout)
        {
            var onOffWorkout = workout as IOnOffWorkout;
            
            
            foreach (var exercise in exercises)
            {
                switch (exercise)
                {
                    case IWallBallExercise wallBallExercise:
                    {
                        var reps = wallBallExercise.Condition == ExerciseCondition.Max
                            ? "Max"
                            : wallBallExercise.Repetitions.ToString("F1") + "s";
                        Debug.Log($"Wall Ball: {reps} reps");
                        break;
                    }
                    case IRowExercise rowExercise:
                        Debug.Log($"Row: {rowExercise.Kcal} kcal");
                        break;
                    case IBurpeeExercise burpeeExercise:
                        Debug.Log($"Burpee: {burpeeExercise.Repetitions} reps");
                        break;
                }
            }
            //StartCoroutine(RunWorkout(exercises));
        }

        // private IEnumerator RunWorkout(IExercise[] exercises)
        // {
        //     foreach (var ex in exercises)
        //     {
        //         switch (ex)
        //         {
        //             case TimeExercise timeExercise:
        //             {
        //                 var timeEx = timeExercise;
        //                 
        //                 while (!timeEx.IsCompleted)
        //                 {
        //                     timeEx.Tick(Time.deltaTime);
        //                     
        //                     yield return null;
        //                 }
        //                 break;
        //             }
        //             case RepetitionsExercise:
        //             {
        //                 while (!ex.IsCompleted)
        //                     yield return null;
        //                 break;
        //             }
        //         }
        //         
        //         yield return new WaitForSeconds(0.5f);
        //     }
        // }
        //
        // public void OnPrimaryAction()
        // {
        //     // znajdź bieżące ćwiczenie typu „licznik” i zwiększ
        //     var current = m_exercises.FirstOrDefault(e => !e.IsCompleted);
        //     current?.OnPrimaryAction();
        // }
    }
}