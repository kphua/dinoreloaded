using UnityEngine;
using System.Collections;

//  movement for enemies who specifically target the player
public class EnemyTargetMovement : MonoBehaviour {
	
	private Transform target;
	private int moveSpeed = 5;
	private int rotationSpeed = 2;
	private int maxDistance = 3;
	
	private Transform enemyTransform;
	
	void Awake() {
		enemyTransform = transform;		
	}
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (target.transform.position, enemyTransform.position, Color.yellow);
		
		// look at target
		enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, Quaternion.LookRotation(target.position - enemyTransform.position), rotationSpeed*Time.deltaTime);
		
		// Move towards target
		if(Vector3.Distance (target.position, enemyTransform.position) > maxDistance)
			enemyTransform.position += enemyTransform.forward * moveSpeed * Time.deltaTime;
		
	}
}
