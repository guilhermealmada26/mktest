using UnityEngine;
using System.Collections;

public class ProjetilePower : MonoBehaviour {

    public Fighter caster;
    public string powerName;
    public float damage;
    Rigidbody body;
    private float creationTIme;
    public float destrTime;
    public float movementForce;
    public bool destroyOnHit;
    public string triggerName;
    int direction;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        direction = transform.rotation.y > 180 ? 1 : -1;
        body.AddRelativeForce(new Vector3(direction*movementForce, 0, 0));
        creationTIme = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - creationTIme > destrTime)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider col)
    {
        Fighter enemy = null;
        if (col.gameObject.GetComponent<Fighter>())
            enemy = col.gameObject.GetComponent<Fighter>();

        if (enemy != caster && enemy!=null)
        {
            enemy.TakeDamage(damage);
            enemy.animator.SetTrigger(triggerName);
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
