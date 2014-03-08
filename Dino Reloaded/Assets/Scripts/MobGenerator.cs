using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {
				
	public enum State {						// the states in the FSM
		Idle,
		Initialize,
		Setup,
		SpawnMob
	}

	public GameObject[] mobPrefabs;			// array to hold the mobs to be spawned
	public GameObject[] spawnPoints;		// array to hold the spawn points

	public State state;						// current state
	
	// function which sets things up before all the scripts run
	void Awake() {							
		state = MobGenerator.State.Initialize;
	}

	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			switch(state) {
			case State.Initialize:
				Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMob:
				SpawnMob();
				break;
			}
		
			yield return 0;					// stop every frame while the rest of the game runs
		}
	}

	private void Initialize() {
		Debug.Log ("in Initialize function");
		
		mobPrefabs = GameObject.FindGameObjectsWithTag("Mob");
		spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

		if (!hasMobPrefabs () || !hasSpawnPoints ())
			return;

		state = MobGenerator.State.Setup;
	}

	private void Setup() {
		Debug.Log ("in Setup function");

		state = MobGenerator.State.SpawnMob;
	}
	
	// instantiate a mob randomly
	// sets its position to the same as the spawn point
	// sets its parent to the the spawn point
	private void SpawnMob() {
		Debug.Log ("in SpawnMob function");
		
		GameObject[] spawnPoints = AvailableSpawnPoints();
		
		for(int i=0; i<spawnPoints.Length; i++) {
			GameObject mob = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)], 
			                            spawnPoints[i].transform.position,
										Quaternion.identity) as GameObject;
			Debug.Log ("mob respawned");
		
			mob.transform.parent = spawnPoints[i].transform;
		}
		
		state = MobGenerator.State.Idle;
	}

	private bool hasMobPrefabs() {
		if (mobPrefabs.Length > 0)
			return true;
		else
			return false;
	}

	private bool hasSpawnPoints() {
		if (spawnPoints.Length > 0)
			return true;
		else
			return false;
	}
	
	// generate a list of spawn points that do not have any mobs childed to it
	private GameObject[] AvailableSpawnPoints() {
		List<GameObject> gameObj = new List<GameObject>();
		
		for(int i=0; i<spawnPoints.Length; i++) {
			if(spawnPoints[i].transform.childCount == 0) {
				Debug.Log("spawn point available");
				gameObj.Add (spawnPoints[i]);
			}
		}
		
		return gameObj.ToArray();
	}

}