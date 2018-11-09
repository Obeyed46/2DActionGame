using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour {

    Transform player; //Target
    public float aggroDistance;
    float delayBetweenAttacks, delaybtwAttChase;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (Vector2.Distance(animator.transform.position, player.transform.position) < aggroDistance && Vector2.Distance(animator.transform.position, player.transform.position) > 0.5f && delaybtwAttChase == 0)
        {
            animator.SetBool("Running", true);
        }
        else if(Vector2.Distance(animator.transform.position, player.transform.position) <= 0.5f && delayBetweenAttacks == 0)
        {
            int randInt;
            randInt = Random.Range(1, 3);
            if (randInt == 1)
            {
                animator.SetTrigger("Attack1");
            }
            else if(randInt == 2)
            {
                animator.SetTrigger("Attack2");
            }
            delayBetweenAttacks = 0.7f;
            delaybtwAttChase = 0.5f;
        }

        if(delayBetweenAttacks > 0)
        {
            delayBetweenAttacks -= Time.deltaTime;
        }
        else if(delayBetweenAttacks < 0)
        {
            delayBetweenAttacks = 0;
        }

        if (delaybtwAttChase > 0)
        {
            delaybtwAttChase -= Time.deltaTime;
        }
        else if (delaybtwAttChase < 0)
        {
            delaybtwAttChase = 0;
        }
    }


	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
