using UnityEngine;
using System.Collections;

public class SpecAttackBehaviour : FighterStateBehaviour {

    public bool isProjectile = true;
    public string powerPath;
    public float yRot,zRot;
    public float yPos = 3.75f;

     //OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator,stateInfo,layerIndex);

        float fighterX = fighter.transform.position.z;

        GameObject instance = Instantiate(
            Resources.Load(powerPath),
            new Vector3(-10, yPos, fighterX),
            Quaternion.Euler(0, yRot, zRot)
            ) as GameObject;
        ProjetilePower power = instance.GetComponent<ProjetilePower>();
        power.caster = fighter;
        if (!isProjectile)
            power.transform.SetParent(fighter.transform);
            }

}
