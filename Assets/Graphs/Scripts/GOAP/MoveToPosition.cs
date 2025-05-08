
// Exemplo de meta atômica: Mover para uma posição
using UnityEngine;
namespace GOAP
{
    public class MoveToPosition : AtomicGoal
    {
        private Vector3 target;
        private float speed = 3f;
        private float reachThreshold = 0.1f;

        public MoveToPosition(GameObject agent, Vector3 target) : base(agent)
        {
            this.target = target;
        }

        public override GoalStatus Process()
        {
            ActivateIfInactive();

            float distance = Vector3.Distance(agent.transform.position, target);

            if (distance <= reachThreshold)
            {
                Status = GoalStatus.Completed;
                Debug.Log("Reached target position!");
                return Status;
            }

            // Movimento simples
            agent.transform.position = Vector3.MoveTowards(
                agent.transform.position,
                target,
                speed * Time.deltaTime);

            Debug.Log($"Moving to position: {target}. Current distance: {distance}");
            return GoalStatus.Active;
        }
    }

    // Exemplo de meta composta: Coletar um item
    public class CollectItem : CompositeGoal
    {
        private Vector3 itemPosition;
        private Vector3 storagePosition;

        public CollectItem(GameObject agent, Vector3 itemPos, Vector3 storagePos) : base(agent)
        {
            this.itemPosition = itemPos;
            this.storagePosition = storagePos;
        }

        public override void Activate()
        {
            base.Activate();

            // Adiciona submetas na ordem inversa (LIFO)
            AddSubgoal(new StoreItem(agent, storagePosition));
            AddSubgoal(new PickUpItem(agent/*, itemPosition*/));
            AddSubgoal(new MoveToPosition(agent, itemPosition));
        }
    }

    // Meta atômica: Pegar item
    public class PickUpItem : AtomicGoal
    {
        public PickUpItem(GameObject agent) : base(agent) { }

        public override GoalStatus Process()
        {
            ActivateIfInactive();

            Debug.Log("Picking up item...");
            // Simula tempo para pegar o item
            Status = GoalStatus.Completed;
            return Status;
        }
    }

    // Meta atômica: Armazenar item
    public class StoreItem : AtomicGoal
    {
        private Vector3 storagePosition;

        public StoreItem(GameObject agent, Vector3 storagePos) : base(agent)
        {
            this.storagePosition = storagePos;
        }

        public override void Activate()
        {
            base.Activate();
            Debug.Log("Storing item...");
        }

        public override GoalStatus Process()
        {
            ActivateIfInactive();

            // Simula armazenamento
            Status = GoalStatus.Completed;
            return Status;
        }
    }
}