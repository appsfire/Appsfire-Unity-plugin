using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public enum AFSDKFeature
{
    AFSDKFeatureEngage          = 1 << 0,
    AFSDKFeatureMonetization    = 1 << 1,
    AFSDKFeatureTrack           = 1 << 2
}

public class AppsfireSDK : MonoBehaviour {
	
	/* Interface to native implementation */
	
	[DllImport ("__Internal")]
	private static extern bool afsdk_connectWithAPIKey(string apikey, AFSDKFeature features);
	
	[DllImport ("__Internal")]
	private static extern bool afsdk_isInitialized();

	[DllImport ("__Internal")]
	private static extern void afsdk_resetCache();
	 
	/*!
	 *  @brief Set up the Appsfire SDK with your API key.
	 *
	 *  @param key Your API key can be found on http://dashboard.appsfire.com/app/manage
	 *
	 *  @return `YES` if no error was detected, `NO` if a problem occured (likely due to the key).
	 */
	public static bool ConnectWithAPIKey(string apikey, AFSDKFeature features)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			return afsdk_connectWithAPIKey(apikey, features);
		return false;
	}
	
	/*!
	 *  @brief Tells you if the SDK is initialized.
	 *
	 *  @note Once the SDK is initialized, you can present the notifications or the feedback.
	 *
	 *  @return `YES` if the sdk is initialized, `NO` if not.
	 */	
	public static bool IsInitialized()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			return afsdk_isInitialized();
		return false;
	}
		
	/*!
	 *  @brief Resets the SDK's cache completely - all user settings will be erased.
	 *
	 *  @note This includes messages that have been read, icon images, assets, etc. DO NOT USE LIGHTLY! If you're having an issue that only this seems to solve, please contact us immediately : app-support@appsfire.com
	 */
	[System.Obsolete]
	public static void ResetCache()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			afsdk_resetCache();
	}
	
}
