#pragma strict

var Counter : int = 0;

function Start () {
	
}

function Update () {
	guiText.text = "Egg Count: "+Counter;
	//Debug.Log("update " + Counter);
}

function ApplyDamage () {
	Counter++;
	//Debug.Log("called applyDamage " + Counter);
}