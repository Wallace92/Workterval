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
            m_ctx.SetPhaseTextCurrent(true);

            if (m_ctx.HasRestTime)
            {
                m_ctx.SetOffTime(m_ctx.OffSeconds);
            }
        }

        public IEnumerator Run()
        {
            yield return m_ctx.Countdown(m_ctx.OnSeconds, true, remaining =>
            {
                m_ctx.SetOnTime(Mathf.CeilToInt(remaining));
                m_ctx.SetWorkoutTime(Mathf.FloorToInt(m_ctx.TotalElapsed));
            });

            if (m_ctx.HasRestTime)
            {
                m_ctx.TransitionTo(new RestState(m_ctx));
                yield break;
            }

            m_ctx.CurrentRound++;
            m_ctx.SetRoundsText($"{m_ctx.CurrentRound}/{m_ctx.Rounds}");

            if (m_ctx.CurrentRound > m_ctx.Rounds)
            {
                m_ctx.TransitionTo(new CompletedState(m_ctx));
                yield break;
            }

            m_ctx.SetPhaseTextCurrent(true);
            m_ctx.SetOnTime(m_ctx.OnSeconds);
            m_ctx.TransitionTo(new WorkState(m_ctx));
        }
    }
}