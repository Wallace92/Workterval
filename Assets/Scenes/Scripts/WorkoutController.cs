using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scenes.Scripts.Data;
using Scenes.Scripts.Factories;
using Scenes.Scripts.Models;
using Scenes.Scripts.Models.Impl;
using Scenes.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class WorkoutController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] 
        private WorkoutData m_workoutData;

        [Header("UI")]
        [SerializeField] 
        private Transform m_gridContent;     
        [SerializeField] 
        private ExerciseTileView m_tilePrefab;
        [SerializeField] 
        private Button m_confirmButton;
        [SerializeField] 
        private Button m_startButton;
        
        private List<IExercise> m_exercises = new();
        private List<IExercise> m_selectedExercises = new();
        
        private readonly Dictionary<string, (int intVal, float floatVal)> userValues = new();

        private void Awake()
        {
            m_exercises = m_workoutData.Exercises
                .Select(ExerciseFactory.Create)
                .ToList();
            
            foreach (var exercise in m_exercises)
            {
                var tile = Instantiate(m_tilePrefab, m_gridContent);
                
                tile.Bind(exercise, OnTileClicked);
            }
            
            m_confirmButton.onClick.RemoveAllListeners();
           // m_confirmButton.onClick.AddListener(ApplyUserConfigs);
            
            m_startButton.onClick.RemoveAllListeners();
            m_startButton.onClick.AddListener(() =>
            {
                //ApplyUserConfigs(); // na wszelki wypadek
                StopAllCoroutines();
                StartCoroutine(RunWorkout());
            });
        }
        
        private void OnTileClicked(IExercise exercise)
        {
            m_selectedExercises.Add(exercise);
        }

        // private void ApplyUserConfigs()
        // {
        //     foreach (var ex in m_selectedExercises)
        //     {
        //       
        //         switch (ex.Type)
        //         {
        //             case ExerciseType.Time:
        //                 var seconds = tuple.floatVal > 0 ? tuple.floatVal : 30f;
        //                 ex.ConfigureFromUser(0, seconds);
        //                 break;
        //             case ExerciseType.Repetitions:
        //                 var reps = tuple.intVal > 0 ? tuple.intVal : 1;
        //                 ex.ConfigureFromUser(reps, 0);
        //                 break;
        //             case ExerciseType.Kcal:
        //                 var kcal = tuple.intVal > 0 ? tuple.intVal : 1;
        //                 ex.ConfigureFromUser(kcal, 0);
        //                 break;
        //         }
        //         ex.Reset();
        //     }
        // }

        private IEnumerator RunWorkout()
        {
            foreach (var ex in m_selectedExercises)
            {
                switch (ex)
                {
                    case TimeExercise timeExercise:
                    {
                        var timeEx = timeExercise;
                        
                        while (!timeEx.IsCompleted)
                        {
                            timeEx.Tick(Time.deltaTime);
                            
                            yield return null;
                        }
                        break;
                    }
                    case RepetitionsExercise:
                    {
                        while (!ex.IsCompleted)
                            yield return null;
                        break;
                    }
                }
                
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        public void OnPrimaryAction()
        {
            // znajdź bieżące ćwiczenie typu „licznik” i zwiększ
            var current = m_exercises.FirstOrDefault(e => !e.IsCompleted);
            current?.OnPrimaryAction();
        }
    }
}