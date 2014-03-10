
var AmmoSpeed = 5;

function start() {

}

function Update () {
	transform.Translate(Vector3.forward * Time.deltaTime * AmmoSpeed);
	//Destroy(instance.gameObject, 5);
	//Destroy(clone.gameObject, 2);
}

function OnCollisionEnter(){
		Destroy(gameObject);
}