#pragma strict

var score : int = 0;

function Start () {
	
}

function Update () {
	guiText.text = "Score: " + score;
}

function IncreaseScore () {
	score += 100;
}

function DecreaseScore () {
	score -= 100;
}