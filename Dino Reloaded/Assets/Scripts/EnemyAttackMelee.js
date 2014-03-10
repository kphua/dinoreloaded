#pragma strict

// Melee attack of the enemy 

private var HealthCounter:GameObject;
private var healthBarScript:HealthBarScript;

function Start(){
	HealthCounter = GameObject.Find("HealthCounter");
	healthBarScript = HealthCounter.GetComponent(HealthBarScript);

	healthBarScript.healthWidth = 90; 
	
}

function OnCollisionEnter(collision: Collision){
	reduceHealth();
	
	if(collision.gameObject == ("Player")){	
		Debug.Log('Touched enemy');
	}
}

function reduceHealth(){
	if(healthBarScript.healthWidth > 0){
		healthBarScript.healthWidth = healthBarScript.healthWidth - 5;
	}
	
		
}

function increaseHealth(){
	if(healthBarScript.healthWidth <= 90){
		healthBarScript.healthWidth = healthBarScript.healthWidth + 5;
	}
}
