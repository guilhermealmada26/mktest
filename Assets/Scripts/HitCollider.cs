using UnityEngine;
using System.Collections;

public class HitCollider : MonoBehaviour {

    [SerializeField]
    string hitName;
    [SerializeField]
    float damage;
    [SerializeField]
    Fighter fighter;
    Fighter enemy;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Fighter>())
            enemy = col.gameObject.GetComponent<Fighter>();
        else
            return;

        if(enemy != fighter && !enemy.IsInvulnerable() &&fighter.IsAttacking())
             enemy.TakeDamage(damage);
    }
}
