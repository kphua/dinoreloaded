using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {

	public float RotationSpeed = 90.0f;
	public int Score = 0;
	//private float ReferenceSpeed;
	//private float ActualRotationSpeed;
	
	void Awake () {
		transform.localEulerAngles = new Vector3 ( 15 , 0 , 0 );
	}

	void Update () {
		//if ( RotationSpeed != ReferenceSpeed ) {
		//UpdateActualRotationSpeed ();
		//}
		float rot = Time.deltaTime * RotationSpeed;
		transform.localEulerAngles += new Vector3 ( 0 , rot , 0 );
	}
	
	void OnTriggerEnter ( Collider Other ) {
		gameObject.SetActive ( false );
		Debug.Log("collected");

		GameObject Counter = GameObject.Find ("EggCounter");
		Counter.SendMessage ("ApplyDamage");

		GameObject Score = GameObject.Find ("Score");
		Score.SendMessage ("IncreaseScore");
	}
	
}