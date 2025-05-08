using System;
using System.Collections.Generic;
using UnityEngine;
namespace GOAP
{
    public enum GoalStatus { Inactive, Active, Completed, Failed }

    // Interface base para todas as ações e metas
    public interface IGoapElement
    {
        GoalStatus Status { get; }
        void Activate();
        GoalStatus Process();
        void Terminate();
    }

    // Meta atômica (ação indivisível)
    public abstract class AtomicGoal : IGoapElement
    {
        public GoalStatus Status { get; protected set; } = GoalStatus.Inactive;
        protected GameObject agent;

        public AtomicGoal(GameObject agent)
        {
            this.agent = agent;
        }

        public virtual void Activate()
        {
            Status = GoalStatus.Active;
            Debug.Log($"{GetType().Name} activated.");
        }

        public abstract GoalStatus Process();

        public virtual void Terminate()
        {
            Status = GoalStatus.Inactive;
            Debug.Log($"{GetType().Name} terminated.");
        }

        protected void ActivateIfInactive()
        {
            if (Status == GoalStatus.Inactive)
            {
                Activate();
            }
        }
    }

    // Meta composta (contém submetas)
    public abstract class CompositeGoal : IGoapElement
    {
        public GoalStatus Status { get; protected set; } = GoalStatus.Inactive;
        protected GameObject agent;
        protected Stack<IGoapElement> subgoals = new Stack<IGoapElement>();

        public CompositeGoal(GameObject agent)
        {
            this.agent = agent;
        }

        public virtual void Activate()
        {
            Status = GoalStatus.Active;
            Debug.Log($"{GetType().Name} activated.");
        }

        public virtual GoalStatus Process()
        {
            // Remove metas concluídas ou falhas
            while (subgoals.Count > 0 &&
                  (subgoals.Peek().Status == GoalStatus.Completed ||
                   subgoals.Peek().Status == GoalStatus.Failed))
            {
                subgoals.Pop().Terminate();
            }

            // Processa a submeta atual
            if (subgoals.Count > 0)
            {
                var status = subgoals.Peek().Process();

                // Se completou mas ainda há submetas, continua ativo
                if (status == GoalStatus.Completed && subgoals.Count > 1)
                {
                    return GoalStatus.Active;
                }
                return status;
            }

            return GoalStatus.Completed;
        }

        public virtual void Terminate()
        {
            while (subgoals.Count > 0)
            {
                subgoals.Pop().Terminate();
            }
            Status = GoalStatus.Inactive;
            Debug.Log($"{GetType().Name} terminated.");
        }

        public void AddSubgoal(IGoapElement goal)
        {
            subgoals.Push(goal);
            Debug.Log($"Added {goal.GetType().Name} to {GetType().Name}");
        }
    }
}
