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
	private static extern bool afsdk_connectWithAPIKey(string sdktoken, string secretkey, AFSDKFeature features);
	
	[DllImport ("__Internal")]
	private static extern bool afsdk_isInitialized();
	 
	/*!
	 *  @brief Set up the Appsfire SDK with your API key.
	 *
	 *  @param key Your API key can be found on http://dashboard.appsfire.com/app/manage
	 *
	 *  @return `YES` if no error was detected, `NO` if a problem occured (likely due to the key).
	 */
	public static bool ConnectWithSDKTokenAndSecretKey(string sdktoken, string secretkey, AFSDKFeature features)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			return afsdk_connectWithAPIKey(sdktoken, secretkey, features);
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
	
}
