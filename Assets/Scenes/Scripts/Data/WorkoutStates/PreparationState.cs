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
        
        public IEnumerator Run()
        {
            yield return m_ctx.Countdown(PreparationTime, false, remaining =>
            {
                m_ctx.SetOnTime(Mathf.CeilToInt(remaining));
            });
            
            m_ctx.ShowWorkoutHeader();
            m_ctx.SetPhaseTextCurrent(true);
            m_ctx.SetOnTime(m_ctx.OnSeconds);
            m_ctx.TransitionTo(new WorkState(m_ctx));
        }
    }
}