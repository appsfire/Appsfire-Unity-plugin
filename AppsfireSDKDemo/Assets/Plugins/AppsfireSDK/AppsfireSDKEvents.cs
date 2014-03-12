using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public enum AFSDKErrorCode
{
    // base sdk
    AFSDKErrorCodeUnknown,                              // unknown
    AFSDKErrorCodeLibraryNotInitialized,                // library isn't initialized yet
    AFSDKErrorCodeInternetNotReachable,                 // internet isn't reachable (and is required)
    AFSDKErrorCodeNeedsApplicationDelegate,             // you need to set the application delegate to proceed
    // advertising sdk / requesting a modal ad
    AFSDKErrorCodeAdvertisingNoAd,                      // no ad available
    AFSDKErrorCodeAdvertisingBadCall,                   // the request call isn't appropriate
    AFSDKErrorCodeAdvertisingAlreadyDisplayed,          // an ad is currently displayed for this format
    AFSDKErrorCodeAdvertisingCanceledByDevelopper,      // the request was canceled by the developer
    // base sdk / presenting panel
    AFSDKErrorCodePanelAlreadyDisplayed,                // the panel is already displayed
    // base sdk / open notification
    AFSDKErrorCodeOpenNotificationNotFound              // the notification wasn't found
}

public class AppsfireSDKEvents : MonoBehaviour {

	/* Interface to native implementation */
	
	[DllImport("__Internal")]
	private static extern void afsdk_iniAndSetCallbackHandler(string handlerName);
	
	/* Base SDK Events */

	// is initializing
	public delegate void AFSDKIsInitializingHandler();
	public static event AFSDKIsInitializingHandler afsdkIsInitializing;

	// is initialized
	public delegate void AFSDKIsInitializedHandler();
	public static event AFSDKIsInitializedHandler afsdkIsInitialized;
	
	// notifications count was updated
	public delegate void AFSDKNotificationsNumberChangedHandler();
	public static event AFSDKNotificationsNumberChangedHandler afsdkNotificationsNumberChanged;
	
	// panel was presented
	public delegate void AFSDKPanelWasPresentedHandler();
	public static event AFSDKPanelWasPresentedHandler afsdkPanelWasPresented;
	
	// panel was dismissed
	public delegate void AFSDKPanelWasDismissedHandler();
	public static event AFSDKPanelWasDismissedHandler afsdkPanelWasDismissed;
	
	/* Advertising SDK Delegate */

	// did initialize
	public delegate void AFSDKAdDidInitializeHandler();
	public static event AFSDKAdDidInitializeHandler afsdkadDidInitialize;
	
	// modal ad is ready for request
	public delegate void AFSDKAdModalAdIsReadyForRequestHandler();
	public static event AFSDKAdModalAdIsReadyForRequestHandler afsdkadModalAdIsReadyForRequest;

	// modal ad request did fail with error code
	public delegate void AFSDKAdModalAdRequestDidFailWithErrorCodeHandler(AFSDKErrorCode errorCode);
	public static event AFSDKAdModalAdRequestDidFailWithErrorCodeHandler afsdkadModalAdRequestDidFailWithErrorCode;
	
	// modal ad will appear
	public delegate void AFSDKAdModalAdWillAppearHandler();
	public static event AFSDKAdModalAdWillAppearHandler afsdkadModalAdWillAppear;

	// modal ad did appear
	public delegate void AFSDKAdModalAdDidAppearHandler();
	public static event AFSDKAdModalAdDidAppearHandler afsdkadModalAdDidAppear;

	// modal ad will disappear
	public delegate void AFSDKAdModalAdWillDisappearHandler();
	public static event AFSDKAdModalAdWillDisappearHandler afsdkadModalAdWillWisappear;

	// modal ad did disappear
	public delegate void AFSDKAdModalAdDidDisappearHandler();
	public static event AFSDKAdModalAdDidDisappearHandler afsdkadModalAdDidDisappear;
			
	/*!
	 *	Awake
	 */
	
	void Awake()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			gameObject.name = this.GetType().ToString();
			afsdk_iniAndSetCallbackHandler(gameObject.name);
		}
		//
		DontDestroyOnLoad(this);
	}  
	
	/*
	 *	Events
	 */
	
	// sdk is initializing
	public void AFSDKIsInitializing(string empty)
	{
		if (afsdkIsInitializing != null)
			afsdkIsInitializing();
	}
	
	// sdk is initialized
	public void AFSDKIsInitialized(string empty)
	{
		if (afsdkIsInitialized != null)
			afsdkIsInitialized();		
	}
	
	// notifications count was updated
	public void AFSDKNotificationsNumberChanged(string empty)
	{
		if (afsdkNotificationsNumberChanged != null)
			afsdkNotificationsNumberChanged();
	}
	
	// panel was presented
	public void AFSDKPanelWasPresented(string empty)
	{
		if (afsdkPanelWasPresented != null)
			afsdkPanelWasPresented();
	}
	
	// panel was dismissed
	public void AFSDKPanelWasDismissed(string empty)
	{
		if (afsdkPanelWasDismissed != null)
			afsdkPanelWasDismissed();
	}
	
	/*
	 *	Events
	 */
	
	// sdk did initialize
	public void AFSDKAdDidInitialize(string empty)
	{
		if (afsdkadDidInitialize != null)
			afsdkadDidInitialize();
	}
	
	// modal ad is ready for request
	public void AFSDKAdModalAdIsReadyForRequest(string empty)
	{
		if (afsdkadModalAdIsReadyForRequest != null)
			afsdkadModalAdIsReadyForRequest();
	}
	
	// modal ad request did fail witht error code
	public void AFSDKAdModalAdRequestDidFailWithErrorCode(string errorCode)
	{
		if (afsdkadModalAdRequestDidFailWithErrorCode != null)
			afsdkadModalAdRequestDidFailWithErrorCode((AFSDKErrorCode)Int32.Parse(errorCode));
	}
	
	// modal ad will appear
	public void AFSDKAdModalAdWillAppear(string empty)
	{
		if (afsdkadModalAdWillAppear != null)
			afsdkadModalAdWillAppear();
	}
	
	// modal ad did appear
	public void AFSDKAdModalAdDidAppear(string empty)
	{
		if (afsdkadModalAdDidAppear != null)
			afsdkadModalAdDidAppear();
	}

	// modal ad will disappear
	public void AFSDKAdModalAdWillDisappear(string empty)
	{
		if (afsdkadModalAdWillWisappear != null)
			afsdkadModalAdWillWisappear();
	}
	
	// modal ad did disappear
	public void AFSDKAdModalAdDidDisappear(string empty)
	{		
		if (afsdkadModalAdDidDisappear != null)
			afsdkadModalAdDidDisappear();
	}
	
}
