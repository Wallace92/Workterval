using System.Collections;
using UnityEngine;

namespace Scenes.Scripts.Data.WorkoutStates
{
    public class PreparationState : IWorkoutState
    {
        private const float PreparationTime = 10f;
        
        private readonly Workout m_ctx;

        public PreparationState(Workout ctx)
        {
            m_ctx = ctx;
        }

        public void Enter()
        {
            m_ctx.ShowPreparation();
        }

        public void Exit()
        {
            
        }

        public IEnumerator Run()
        {
            yield return m_ctx.Countdown(PreparationTime, false, remaining =>
            {
                m_ctx.SetOnTime(Workout.FormatMmss(Mathf.CeilToInt(remaining)));
            });

            if (m_ctx.HasRest)
            {
                m_ctx.UI_SetOffGroupVisible(true);
            }
            
            m_ctx.UI_ShowWorkoutHeader();
            m_ctx.UI_SetPhaseTextCurrent(true);
            m_ctx.SetOnTime(Workout.FormatMmss(m_ctx.OnSeconds));
            m_ctx.TransitionTo(new WorkState(m_ctx));
        }
    }
}