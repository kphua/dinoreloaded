#pragma strict


var backgroundTexture: Texture;
var foregroundTexture: Texture;

var healthWidth: int=90;
var healthHeight: int=18;

var healthMarginLeft: int=7;
var healthMarginTop: int=34;


function OnGUI () {
	GUI.DrawTexture(Rect(healthMarginLeft,healthMarginTop,healthMarginLeft+ healthWidth,healthHeight),backgroundTexture,ScaleMode.ScaleToFit,true,0);
	GUI.DrawTexture(Rect(healthMarginLeft,healthMarginTop,healthMarginLeft+ healthWidth, healthHeight),foregroundTexture,ScaleMode.ScaleAndCrop,true,0);
}

