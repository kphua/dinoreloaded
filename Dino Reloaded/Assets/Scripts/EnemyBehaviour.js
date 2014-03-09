#pragma strict

private var Health:GameObject;
private var healthBarScript:HealthBarScript;

function Start(){
	Health = GameObject.Find("Health");
	healthBarScript = Health.GetComponent("HealthBarScript");

	healthBarScript.healthWidth = 90; 
	
}

function OnCollisionEnter(collision: Collision){
	reduceHealth();
	
	if(collision.gameObject == ("Player")){	
		Debug.Log('Touched enemy');
	}
}

function reduceHealth(){
	if(healthBarScript.healthWidth > 2){
		healthBarScript.healthWidth = healthBarScript.healthWidth - 5;
	}
	
		
}

function increaseHealth(){
	if(healthBarScript.healthWidth <= 90){
		healthBarScript.healthWidth = healthBarScript.healthWidth + 5;
	}
}
