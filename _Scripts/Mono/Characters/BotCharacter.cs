using Kuhpik;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotCharacter : BaseMovementCharacter {
    public NavMeshAgent NavMeshAgent { get; private set; }

    private void Awake() {
        base.Awake();
        
        NavMeshAgent = GetComponent<NavMeshAgent>();

        GameExtensions.CopyComponentValuesToAnother<NavMeshAgent>(Bootstrap.instance.config.BotsNavMeshAgentForCopy,
            NavMeshAgent);
    }

    public void Idle() {
        base.Idle();
    }

    public void Walk(Vector3 destination) {
        base.Walk();

        NavMeshAgent.SetDestination(destination);
    }

    public void AttackAnimation() {
        animator.SetTrigger(Constants.AnimatorTags.Attack);
    }

    public void InteractionWithCampFireAnimation() {
      //  animator.SetTrigger(Constants.AnimatorTags.InteractionWithCampFire);
    }
    /*
    public override void SetMovementData(MovementData movementData) {
       // base.SetMovementData(movementData);

        NavMeshAgent.speed = movementData.MovementSpeed;
        NavMeshAgent.angularSpeed = movementData.AngularSpeed;
    } */
}