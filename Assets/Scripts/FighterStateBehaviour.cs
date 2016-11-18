using UnityEngine;
using System.Collections;

public class FighterStateBehaviour : StateMachineBehaviour {

    protected Fighter fighter;
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    FighterState state;
    [SerializeField]
    float dx;
    [SerializeField]
    float dy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(fighter == null)
            fighter = animator.gameObject.GetComponent<Fighter>();

        if(clip != null)
            fighter.PlaySound(clip);

        //sets the fighter state to be the current state executing
        fighter.state = state;
        fighter.RigidBody.AddRelativeForce(Vector3.up * dy *20);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        fighter.RigidBody.AddRelativeForce(Vector3.forward * dx * 1.5f);
	}
}
