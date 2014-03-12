#import "AppsfireSDKWrapper.h"
#import "UnityAppController.h"

#pragma mark - Unity Controller

void UnitySendDeviceToken(NSData* deviceToken);

@implementation UnityAppController (Appsfire)

- (void)application:(UIApplication*)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData*)deviceToken {
    
    [AppsfireSDK registerPushToken:deviceToken];
    
    UnitySendDeviceToken(deviceToken);
    
}

@end

#pragma mark - Appsfire Wrapper

@interface AppsfireSDKWrapper()
- (void)_bindNotificationToEvent:(NSString *)messageName;
@end

@implementation AppsfireSDKWrapper

+ (id)sharedInstance {
    
    static dispatch_once_t pred;
    static AppsfireSDKWrapper *shared = nil;
    
    dispatch_once(&pred, ^{
        shared = [[AppsfireSDKWrapper alloc] init];
    });
    return shared;
    
}

- (id)init {
    
    if ((self = [super init]) != nil) {
      
		// notifications
	    [ [ NSNotificationCenter defaultCenter ] addObserver:self selector:@selector(_bindNotificationToEvent:) name:kAFSDKIsInitializing object:nil ];
	    [ [ NSNotificationCenter defaultCenter ] addObserver:self selector:@selector(_bindNotificationToEvent:) name:kAFSDKIsInitialized object:nil ];
	    [ [ NSNotificationCenter defaultCenter ] addObserver:self selector:@selector(_bindNotificationToEvent:) name:kAFSDKNotificationsNumberChanged object:nil ];
	    [ [ NSNotificationCenter defaultCenter ] addObserver:self selector:@selector(_bindNotificationToEvent:) name:kAFSDKPanelWasPresented object:nil ];
	    [ [ NSNotificationCenter defaultCenter ] addObserver:self selector:@selector(_bindNotificationToEvent:) name:kAFSDKPanelWasDismissed object:nil ];
        
	}
    return self;
    
}

#pragma mark - Notifications

- (void)_bindNotificationToEvent:(NSNotification *)notification {
	
	NSString *messageName;
	
	// 
	messageName = nil;
	if ([notification.name isEqualToString:kAFSDKIsInitializing])
		messageName = @"AFSDKIsInitializing";
	else if ([notification.name isEqualToString:kAFSDKIsInitialized])
		messageName = @"AFSDKIsInitialized";
	else if ([notification.name isEqualToString:kAFSDKNotificationsNumberChanged])
		messageName = @"AFSDKNotificationsNumberChanged";
	else if ([notification.name isEqualToString:kAFSDKPanelWasPresented])
		messageName = @"AFSDKPanelWasPresented";
	else if ([notification.name isEqualToString:kAFSDKPanelWasDismissed])
		messageName = @"AFSDKPanelWasDismissed";
	else
		return;
		
	//
	UnitySendMessage([_callbackHandlerName UTF8String], [messageName UTF8String], "");
	
}

#pragma mark - Ad Delegate

- (void)adUnitDidInitialize {
	
	UnitySendMessage([_callbackHandlerName UTF8String], "AFSDKAdDidInitialize", "");
	
}

- (void)modalAdIsReadyForRequest {
	
	UnitySendMessage([_callbackHandlerName UTF8String], "AFSDKAdModalAdIsReadyForRequest", "");
	
}

- (void)modalAdRequestDidFailWithError:(NSError *)error {
	
	UnitySendMessage([_callbackHandlerName UTF8String], "AFSDKAdModalAdRequestDidFailWithErrorCode", [[NSString stringWithFormat:@"%d", error.code] UTF8String]);
	
}

- (void)modalAdWillAppear {
	
	UnitySendMessage([_callbackHandlerName UTF8String], "AFSDKAdModalAdWillAppear", "");
	
}

- (void)modalAdDidAppear {
	
	UnitySendMessage([_callbackHandlerName UTF8String], "AFSDKAdModalAdDidAppear", "");
	
}

- (void)modalAdWillDisappear {

	UnitySendMessage([_callbackHandlerName UTF8String], "AFSDKAdModalAdWillDisappear", "");	
	
}

- (void)modalAdDidDisappear {
	
	UnitySendMessage([_callbackHandlerName UTF8String], "AFSDKAdModalAdDidDisappear", "");	
	
}

@end

#pragma mark - Extern Methods

void afsdk_iniAndSetCallbackHandler(const char* handlerName) {
	
	[ [ AppsfireSDKWrapper sharedInstance ] setCallbackHandlerName:[NSString stringWithUTF8String:handlerName] ];
	[ AppsfireAdSDK setDelegate:[ AppsfireSDKWrapper sharedInstance ] ];
	
}

bool afsdk_connectWithAPIKey(const char *apikey) {
	
	NSString *apikeyString;
	
	//
	apikeyString = (apikey != NULL) ? [NSString stringWithUTF8String:apikey] : nil;
	
	//
    return [AppsfireSDK connectWithAPIKey:apikeyString];

}

bool afsdk_isInitialized() {
	
	return [AppsfireSDK isInitialized];
	
}

void afsdk_pause() {
	
	[AppsfireSDK pause];
	
}

void afsdk_resume() {
	
	[AppsfireSDK resume];
	
}

void afsdk_setFeatures(AFSDKFeature features) {
	
	[AppsfireSDK setFeatures:features];
	
}

void afsdk_handleBadgeCountLocally(bool handleLocally) {
	
	[AppsfireSDK handleBadgeCountLocally:handleLocally];
	
}

void afsdk_handleBadgeCountLocallyAndRemotely(bool handleLocallyAndRemotely) {
	
	[AppsfireSDK handleBadgeCountLocallyAndRemotely:handleLocallyAndRemotely];
	
}
	
bool afsdk_presentPanelForContentAndStyle(AFSDKPanelContent content, AFSDKPanelStyle style) {
	
	return ([AppsfireSDK presentPanelForContent:content withStyle:style] == nil);
	
}

void afsdk_dismissPanel() {
	
	[AppsfireSDK dismissPanel];
	
}

bool afsdk_isDisplayed() {
	
	return [AppsfireSDK isDisplayed];
	
}

void afsdk_openSDKNotificationID(int notificationID) {
	
	[AppsfireSDK openSDKNotificationID:notificationID];
	
}

void afsdk_setBackgroundAndTextColor (struct AF_RGBA backgroundColor, struct AF_RGBA textColor) {

	[AppsfireSDK setBackgroundColor:[UIColor colorWithRed:backgroundColor.red green:backgroundColor.green blue:backgroundColor.blue alpha:backgroundColor.alpha] 
				textColor:[UIColor colorWithRed:textColor.red green:textColor.green blue:textColor.blue alpha:textColor.alpha]];
	
}

void afsdk_setCustomKeysValues(const char*  attributes) {
	
    NSString *attris;
    NSArray *attributesArray;
    NSMutableDictionary *oAttributes;
    NSRange range;
    
    //
    if (attributes == NULL)
        return;
    
    //
    attris = [NSString stringWithUTF8String:attributes];
    attributesArray = [attris componentsSeparatedByString:@"\n"];
    
    //
    oAttributes = [NSMutableDictionary dictionary];
    for (NSString *keyValuePair in attributesArray) {
        range = [keyValuePair rangeOfString:@"=/="];
        if (range.location != NSNotFound) {
            NSString *key = [keyValuePair substringToIndex:range.location];
            NSString *value = [keyValuePair substringFromIndex:range.location+3];
            [oAttributes setObject:value forKey:key];
        }
    }
    
    //
    [AppsfireSDK setCustomKeysValues:oAttributes];
	
}

bool afsdk_setUserEmailAndModifiable(const char* email, bool modifiable) {
	
	NSString *emailString;
	
	//
	emailString = (email != NULL) ? [NSString stringWithUTF8String:email] : nil;
	
	//
	return [AppsfireSDK setUserEmail:emailString isModifiable:modifiable];
	
}

void afsdk_showFeedbackButton(bool showButton) {
	
	[AppsfireSDK setShowFeedbackButton:showButton];
	
}

int afsdk_numberOfPendingNotifications() {
	
	return [AppsfireSDK numberOfPendingNotifications];
	
}

void afsdk_resetCache() {
	
	[AppsfireSDK resetCache];
	
}

void afsdkad_prepare() {
	
	[AppsfireAdSDK prepare];
	
}

bool afsdkad_isInitialized() {
	
	return [AppsfireAdSDK isInitialized];
	
}

void afsdkad_setUseInAppDownloadWhenPossible(bool use) {
	
	[AppsfireAdSDK setUseInAppDownloadWhenPossible:use];
	
}

void afsdkad_setDebugModeEnabled(bool use) {
	
	[AppsfireAdSDK setDebugModeEnabled:use];
	
}

void afsdkad_requestModalAd(AFAdSDKModalType modalType) {
	
    [AppsfireAdSDK requestModalAd:modalType withController:[UIApplication sharedApplication].keyWindow.rootViewController];
	
}

AFAdSDKAdAvailability afsdkad_isThereAModalAdAvailable(AFAdSDKModalType modalType) {
	
	return [AppsfireAdSDK isThereAModalAdAvailableForType:modalType];
	
}

bool afsdkad_cancelPendingAdModalRequest() {
	
	return [AppsfireAdSDK cancelPendingAdModalRequest];
	
}

bool afsdkad_isModalAdDisplayed() {
	
	return [AppsfireAdSDK isModalAdDisplayed];
	
}
