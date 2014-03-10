using UnityEngine;
using System.Collections;
using System;

public class TouchDetector : MonoBehaviour 
{
	private AndroidJavaObject library;
	private AndroidJavaObject activity;
	private AndroidJavaObject playerView;

	private SPenHoverListener hoverListener;
	private SPenTouchListener touchListener;
	private SPenDetachmentListener detachListener;
	
	// Use this for initialization
	void Start () 
	{
		#if UNITY_ANDROID
			//Initializing the Android objects
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
			activity = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
			library = activity.Call<AndroidJavaObject> ("getPenEventLib");
			playerView = activity.Call<AndroidJavaObject> ("getPlayerView");

			//Initializing the Android pen listeners
			detachListener = new SPenDetachmentListener (this);
			hoverListener = new SPenHoverListener (this);
			touchListener = new SPenTouchListener (this);

			library.Call<bool>("registerSPenDetachmentListener", activity, detachListener);
			library.Call ("setSPenHoverListener", playerView, hoverListener);
			library.Call ("setSPenTouchListener", playerView, touchListener);
		#endif
	}

	public float getHoverX()
	{
		return hoverListener.getHoverX ();
	}

	public float getHoverY()
	{
		return hoverListener.getHoverY ();
	}
	public float getFingerTouchX()
	{
		return hoverListener.getHoverX();
	}

	public float getFingerTouchY()
	{
		return hoverListener.getHoverY();
	}

	public float getPenTouchX()
	{
		return touchListener.getPenX ();
	}

	public float getPenTouchY()
	{
		return touchListener.getPenY ();
	}

	public bool isFingerPressed()
	{
		return touchListener.isFingerPressed ();
	}

	public bool isPenPressed()
	{
		return touchListener.isPenPressed ();
	}

	public bool isHover()
	{
		return hoverListener.isHover ();
	}

	public bool isPenAttached()
	{
		return detachListener.isAttached ();
	}

	//Display a toast notification on the Android device
	public void Toast(string msg)
	{
		AndroidJavaObject text = new AndroidJavaObject ("java.lang.String", msg);
		activity.Call ("showMsg", text);
	}
}

class SPenTouchListener : AndroidJavaProxy
{
	private TouchDetector detector;
	private float penX;
	private float penY;
	private float fingerX;
	private float fingerY;

	private bool fingerPressed;
	private bool penPressed;
	private readonly int ACTION_DOWN;
	private readonly int ACTION_MOVE;
	
	public SPenTouchListener() : base("com.samsung.spensdk.applistener.SPenTouchListener") {}
	public SPenTouchListener(TouchDetector detector) : base("com.samsung.spensdk.applistener.SPenTouchListener")
	{
		this.detector = detector;
		AndroidJavaClass motionEventClass = new AndroidJavaClass ("android.view.MotionEvent");
		ACTION_DOWN = motionEventClass.GetStatic<int> ("ACTION_DOWN");
		ACTION_MOVE = motionEventClass.GetStatic<int> ("ACTION_MOVE");
	}

	//Side button pressed while pen is pressed onto the screen
	public void onTouchButtonDown(AndroidJavaObject view, AndroidJavaObject motionEvent) {
	}

	//Side button released while pen is pressed onto the screen
	public void onTouchButtonUp(AndroidJavaObject view, AndroidJavaObject motionEvent) {
	}
	
	public bool onTouchFinger(AndroidJavaObject view, AndroidJavaObject motionEvent) {
		fingerX = motionEvent.Call<float> ("getX");
		fingerY = motionEvent.Call<float> ("getY");

		int action = motionEvent.Call<int> ("getAction");
		fingerPressed = action == ACTION_DOWN || action == ACTION_MOVE;
		return true;
	}
	
	public bool onTouchPen(AndroidJavaObject view, AndroidJavaObject motionEvent) {
		penX = motionEvent.Call<float> ("getX");
		penY = motionEvent.Call<float> ("getY");

		int action = motionEvent.Call<int> ("getAction");
		penPressed = action == ACTION_DOWN || action == ACTION_MOVE;
		return true;
	}

	//Samsung Galaxy Note 10.1 does not have pen eraser
	public bool onTouchPenEraser(AndroidJavaObject view, AndroidJavaObject motionEvent) {
		return true;
	}

	public bool isFingerPressed() {
		return fingerPressed;
	}

	public bool isPenPressed() {
		return penPressed;
	}

	public float getPenX() {
		return penX;
	}

	public float getPenY() {
		return penY;
	}

	public float getFingerX() {
		return fingerX;
	}

	public float getFingerY() {
		return fingerY;
	}
}

class SPenHoverListener : AndroidJavaProxy
{
	private TouchDetector detector;
	private float x, y;
	private bool hover;

	private readonly int ACTION_HOVER_ENTER;
	private readonly int ACTION_HOVER_MOVE;
	private readonly int ACTION_DOWN;
	private readonly int ACTION_MOVE;
	
	public SPenHoverListener() : base("com.samsung.spensdk.applistener.SPenHoverListener") {}
	public SPenHoverListener(TouchDetector detector) : base("com.samsung.spensdk.applistener.SPenHoverListener")
	{
		this.detector = detector;

		AndroidJavaClass motionEventClass = new AndroidJavaClass ("android.view.MotionEvent");
		ACTION_HOVER_ENTER = motionEventClass.GetStatic<int> ("ACTION_HOVER_ENTER");
		ACTION_HOVER_MOVE = motionEventClass.GetStatic<int> ("ACTION_HOVER_MOVE");
		ACTION_DOWN = motionEventClass.GetStatic<int> ("ACTION_DOWN");
		ACTION_MOVE = motionEventClass.GetStatic<int> ("ACTION_MOVE");
	}

	public bool onHover(AndroidJavaObject view, AndroidJavaObject motionEvent) {
		x = motionEvent.Call<float> ("getX");
		y = motionEvent.Call<float> ("getY");

		int actionCode = motionEvent.Call<int> ("getAction");
		hover = actionCode == ACTION_HOVER_ENTER || actionCode == ACTION_HOVER_MOVE || actionCode == ACTION_DOWN || actionCode == ACTION_MOVE;
		return true;
	}

	public bool isHover()
	{
		return hover;
	}

	//side button pressed while pen is hovering above the screen
	public void onHoverButtonDown(AndroidJavaObject view, AndroidJavaObject motionEvent) {
	}

	//side button released while pen is hovering above the screen
	public void onHoverButtonUp(AndroidJavaObject view, AndroidJavaObject motionEvent) {
	}

	public float getHoverX() {
		return x;
	}
	
	public float getHoverY() {
		return y;
	}
}

class SPenDetachmentListener : AndroidJavaProxy 
{
	private TouchDetector detector;
	private bool attached;
	
	public SPenDetachmentListener() : base("com.samsung.spensdk.applistener.SPenDetachmentListener") {}
	public SPenDetachmentListener(TouchDetector detector) : base("com.samsung.spensdk.applistener.SPenDetachmentListener")
	{
		this.detector = detector;
	}
	
	public void onSPenDetached(bool detached) 
	{
		attached = !detached;
	}

	public bool isAttached()
	{
		return attached;
	}
}