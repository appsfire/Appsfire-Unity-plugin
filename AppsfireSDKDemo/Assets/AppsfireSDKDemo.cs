using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AppsfireSDKDemo : MonoBehaviour
{   
	// 
	private int numberOfNotifications;
	
	/*
	 *	Register / Unregister to Events
	 */
	
	void OnEnable()
	{
		// base sdk events
		AppsfireSDKEvents.afsdkIsInitializing += this.afsdkIsInitializing;
		AppsfireSDKEvents.afsdkIsInitialized += this.afsdkIsInitialized;
		
		// engage sdk events
		AppsfireSDKEvents.afsdkNotificationsNumberChanged += this.afsdkNotificationsNumberChanged;
		AppsfireSDKEvents.afsdkPanelWasPresented += this.afsdkPanelWasPresented;
		AppsfireSDKEvents.afsdkPanelWasDismissed += this.afsdkPanelWasDismissed;
		
		// ad sdk events
		AppsfireSDKEvents.afsdkadModalAdsRefreshedAndAvailable += this.afsdkadModalAdsRefreshedAndAvailable;
		AppsfireSDKEvents.afsdkadModalAdsRefreshedAndNotAvailable += this.afsdkadModalAdsRefreshedAndNotAvailable;
		
		// modal ad events
		AppsfireSDKEvents.afsdkadModalAdRequestDidFailWithErrorCode += this.afsdkadModalAdRequestDidFailWithErrorCode;
		AppsfireSDKEvents.afsdkadModalAdWillAppear += this.afsdkadModalAdWillAppear;
		AppsfireSDKEvents.afsdkadModalAdDidAppear += this.afsdkadModalAdDidAppear;
		AppsfireSDKEvents.afsdkadModalAdWillWisappear += this.afsdkadModalAdWillWisappear;
		AppsfireSDKEvents.afsdkadModalAdDidDisappear += this.afsdkadModalAdDidDisappear;
	}
	
	void OnDisable()
	{
		// base sdk events
		AppsfireSDKEvents.afsdkIsInitializing -= this.afsdkIsInitializing;
		AppsfireSDKEvents.afsdkIsInitialized -= this.afsdkIsInitialized;
		
		// engage sdk events
		AppsfireSDKEvents.afsdkNotificationsNumberChanged -= this.afsdkNotificationsNumberChanged;
		AppsfireSDKEvents.afsdkPanelWasPresented -= this.afsdkPanelWasPresented;
		AppsfireSDKEvents.afsdkPanelWasDismissed -= this.afsdkPanelWasDismissed;
		
		// ad sdk events
		AppsfireSDKEvents.afsdkadModalAdsRefreshedAndAvailable -= this.afsdkadModalAdsRefreshedAndAvailable;
		AppsfireSDKEvents.afsdkadModalAdsRefreshedAndNotAvailable -= this.afsdkadModalAdsRefreshedAndNotAvailable;
		
		// modal ad events
		AppsfireSDKEvents.afsdkadModalAdRequestDidFailWithErrorCode -= this.afsdkadModalAdRequestDidFailWithErrorCode;
		AppsfireSDKEvents.afsdkadModalAdWillAppear -= this.afsdkadModalAdWillAppear;
		AppsfireSDKEvents.afsdkadModalAdDidAppear -= this.afsdkadModalAdDidAppear;
		AppsfireSDKEvents.afsdkadModalAdWillWisappear -= this.afsdkadModalAdWillWisappear;
		AppsfireSDKEvents.afsdkadModalAdDidDisappear -= this.afsdkadModalAdDidDisappear;
	}

    // Use this for initialization
    void Start()
    {
		AF_RGBA backgroundColor, textColor;
		
		//
		numberOfNotifications = 0;
		
		// af ad sdk - enable debug mode to see an ad each time
		// we suggest you to only keep it for debug builds!
		// otherwise, don't forget to comment/remove it before any store submission!!!
		#warning enable it for testing, disable it for store submission!!
		AppsfireAdSDK.SetDebugModeEnabled(true);
								
		// af sdk - connect with your api key
		#error please enter your api key here!
		AppsfireSDK.ConnectWithAPIKey("", AFSDKFeature.AFSDKFeatureEngage | AFSDKFeature.AFSDKFeatureMonetization);
		
		// af engage sdk - handle badge count locally and remotely
		AppsfireEngageSDK.HandleBadgeCountLocallyAndRemotely(true);
		#if UNITY_IPHONE
			NotificationServices.RegisterForRemoteNotificationTypes(RemoteNotificationType.Alert | RemoteNotificationType.Badge | RemoteNotificationType.Sound);
		#endif
		
		// af engage sdk - customize background and text colors
		// these values are default colors, you can customize with the colors of your app
		backgroundColor = new AF_RGBA(66.0/255.0, 67.0/255.0, 69.0/255.0, 1.0);
		textColor = new AF_RGBA(1.0, 1.0, 1.0, 1.0);
		AppsfireEngageSDK.SetBackgroundAndTextColor(backgroundColor, textColor);
    }

	void Update()
	{
		//
	}
		
    void OnGUI ()
    {
		string text;
		float centerX;
		GUIStyle labelStyle, buttonStyle;
		float minY, buttonWidth, buttonHeight, buttonMargin;
		
		//
		centerX = Screen.width / 2;
	
		// button style
	    buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = (Screen.width > 400.0) ? 32 : 16;

		// label style
		labelStyle = new GUIStyle();
	    labelStyle.fontSize = (Screen.width > 400.0) ? 48 : 24;
        labelStyle.normal.textColor = Color.black;
        labelStyle.alignment = TextAnchor.MiddleCenter;
    
		// button size
		buttonWidth = (float)Math.Floor(Screen.width * 0.80);
		buttonHeight = (float)Math.Floor(Screen.height * 0.09);
		buttonMargin = (float)Math.Floor(buttonHeight * 0.25);
						
		// label hello
        GUI.Label(new Rect(centerX - buttonWidth / 2.0f, buttonMargin, buttonWidth, buttonHeight), "Appsfire SDK Demo", labelStyle);
		minY = buttonHeight + buttonMargin * 2.0f;

		// button open notifications
		text = "Open Panel for Notifications";
		if (AppsfireSDK.IsInitialized())
			text += " ("+ numberOfNotifications +")";
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, minY, buttonWidth, buttonHeight), text, buttonStyle))
			AppsfireEngageSDK.PresentPanelForContentAndStyle(AFSDKPanelContent.AFSDKPanelContentDefault, AFSDKPanelStyle.AFSDKPanelStyleFullscreen);
		minY += buttonHeight + buttonMargin;

		// button open feedback
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, minY, buttonWidth, buttonHeight), "Open Panel for Feedback", buttonStyle))
			AppsfireEngageSDK.PresentPanelForContentAndStyle(AFSDKPanelContent.AFSDKPanelContentFeedbackOnly, AFSDKPanelStyle.AFSDKPanelStyleFullscreen);
		minY += buttonHeight + buttonMargin;
		
		// button request modal ad (sushi)
		text = "Request Modal sushi";
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, minY, buttonWidth, buttonHeight), text, buttonStyle))
			AppsfireAdSDK.RequestModalAd(AFAdSDKModalType.AFAdSDKModalTypeSushi);
		minY += buttonHeight + buttonMargin;
			
		// button request modal ad (uramaki)
		text = "Request Modal uramaki";
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, minY, buttonWidth, buttonHeight), text, buttonStyle))
			AppsfireAdSDK.RequestModalAd(AFAdSDKModalType.AFAdSDKModalTypeUraMaki);

    }

	/*
	 *	Events
	 */

	// sdk is initializing
	public void afsdkIsInitializing()
	{
		Debug.Log("Appsfire SDK - Is Initializing");
	}

	// sdk is initialized
	public void afsdkIsInitialized()
	{
		Debug.Log("Appsfire SDK - Is Initialized");
	}
	
	// notifications count was updated
	public void afsdkNotificationsNumberChanged()
	{
		Debug.Log("Appsfire Engage SDK - Number of Notifications was updated");
		numberOfNotifications = AppsfireEngageSDK.NumberOfPendingNotifications();
	}
	
	// panel was presented
	public void afsdkPanelWasPresented()
	{
		Debug.Log("Appsfire Engage SDK - Panel was presented");		
	}
	
	// panel was dismissed
	public void afsdkPanelWasDismissed()
	{
		Debug.Log("Appsfire Engage SDK - Panel was dismissed");
	}

	// modal ads refreshed and available
	public void afsdkadModalAdsRefreshedAndAvailable()
	{
		Debug.Log("Appsfire Ad SDK - Modal Ad Refreshed And Available For Request");
		// you could directly present a modal ad here
		/*
		if (AppsfireAdSDK.IsThereAModalAdAvailable(AFAdSDKModalType.AFAdSDKModalTypeUraMaki) == AFAdSDKAdAvailability.AFAdSDKAdAvailabilityYes) {
			AppsfireAdSDK.RequestModalAd(AFAdSDKModalType.AFAdSDKModalTypeSushi);
		}
		*/
	}

	// modal ads refreshed and none is available
	public void afsdkadModalAdsRefreshedAndNotAvailable()
	{
		Debug.Log("Appsfire Ad SDK - Modal Ad Refreshed And None Is Available For Request");
	}
	
	// modal ad request did fail
	public void afsdkadModalAdRequestDidFailWithErrorCode(AFSDKErrorCode errorCode)
	{
		Debug.Log("Appsfire Ad SDK - Modal Ad Request Did Fail With Error Code");
	}

	// modal ad will appear
	public void afsdkadModalAdWillAppear()
	{
		Debug.Log("Appsfire Ad SDK - Modal Ad Will Appear");
	}
	
	// modal ad did appear
	public void afsdkadModalAdDidAppear()
	{
		Debug.Log("Appsfire Ad SDK - Modal Ad Did Appear");
	}

	// modal ad will disappear
	public void afsdkadModalAdWillWisappear()
	{
		Debug.Log("Appsfire Ad SDK - Modal Ad Will Disappear");
	}
	
	// modal ad did disappear
	public void afsdkadModalAdDidDisappear()
	{
		Debug.Log("Appsfire Ad SDK - Modal Ad Did Disappear");
	}
}
