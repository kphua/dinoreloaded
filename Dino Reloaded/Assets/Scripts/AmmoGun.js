#pragma strict
var AmmoSpawn : Transform;
var Ammo : GameObject;

function Start () {
}
function Update () {

	if(Input.GetButtonDown("Fire1")){
		Instantiate(Ammo, AmmoSpawn.transform.position, AmmoSpawn.transform.rotation);
	}
	
}
