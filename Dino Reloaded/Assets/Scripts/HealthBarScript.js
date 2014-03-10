#pragma strict


var backgroundTexture: Texture;
var foregroundTexture: Texture;

var healthWidth: int=90;
var healthHeight: int=18;

var healthMarginLeft: int=7;
var healthMarginTop: int=34;

var bgWidth: int=90;
var bgHeight: int=18;

var bgMarginLeft: int=7;
var bgMarginTop: int=34;

function OnGUI () {
	GUI.DrawTexture(Rect(bgMarginLeft,bgMarginTop,bgMarginLeft+ bgWidth,bgHeight),backgroundTexture,ScaleMode.ScaleAndCrop,true,0);
	GUI.DrawTexture(Rect(healthMarginLeft,healthMarginTop,healthMarginLeft+ healthWidth, healthHeight),foregroundTexture,ScaleMode.ScaleAndCrop,true,0);
}

