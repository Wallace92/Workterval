using System.Collections;
using UnityEngine;

namespace Scenes.Scripts.Data.WorkoutStates
{
    public class WorkState : IWorkoutState
    {
        private readonly Workout m_ctx;

        public WorkState(Workout ctx)
        {
            m_ctx = ctx;
        }

        public void Enter()
        {
            m_ctx.UI_SetPhaseTextCurrent(true);

            if (m_ctx.HasRest)
            {
                m_ctx.UI_SetOffTime(Workout.FormatMmss(m_ctx.OffSeconds));
            }
        }

        public void Exit()
        {
            
        }

        public IEnumerator Run()
        {
            yield return m_ctx.Countdown(m_ctx.OnSeconds, true, remaining =>
            {
                m_ctx.SetOnTime(Workout.FormatMmss(Mathf.CeilToInt(remaining)));
                m_ctx.UI_SetWorkoutTime(Workout.FormatMmss(Mathf.FloorToInt(m_ctx.TotalElapsed)));
            });

            if (m_ctx.HasRest)
            {
                m_ctx.TransitionTo(new RestState(m_ctx));
                yield break;
            }

            m_ctx.CurrentRound++;
            m_ctx.UI_SetRounds($"{m_ctx.CurrentRound}/{m_ctx.Rounds}");

            if (m_ctx.CurrentRound > m_ctx.Rounds)
            {
                m_ctx.TransitionTo(new CompletedState(m_ctx));
                yield break;
            }

            m_ctx.UI_SetPhaseTextCurrent(true);
            m_ctx.SetOnTime(Workout.FormatMmss(m_ctx.OnSeconds));
            m_ctx.TransitionTo(new WorkState(m_ctx));
        }
    }
}