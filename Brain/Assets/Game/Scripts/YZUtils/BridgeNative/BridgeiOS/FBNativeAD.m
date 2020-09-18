//
//  FBNativeAD.m
//  ADTest
//
//  Created by yz on 2019/12/17.
//  Copyright © 2019年 yz. All rights reserved.
//

#import "FBNativeAD.h"
#import <FBAudienceNetwork/FBAudienceNetwork.h>

#pragma mark - C
void _initFBNativeADsWithPlacementID(char *placementID){
    NSString *placeid = [NSString stringWithCString:placementID encoding:NSUTF8StringEncoding];
    [FBNativeAD.instance initWithPlacementID:placeid];
}
void _showFBNativeADs(void){
    [FBNativeAD.instance showFBNativeADs];
}
void _hideFBNativeADs(void){
    [FBNativeAD.instance hideFBNativeADs];
}
bool _FBNativeADsIsAdValid(void){
    return [FBNativeAD.instance isAdValid];
}
void _FBNativeADsLoad(void){
    [FBNativeAD.instance loadADs];
}



static char *const UnityCallBackObject = "FBNativeADCallBack";

#pragma mark - OC
@interface FBNativeAD () <FBNativeAdDelegate,FBMediaViewDelegate>
/// window
@property (strong,nonatomic) UIView *rootView;
/// 广告
@property (strong, nonatomic) FBNativeAd *nativeAd;
/// 广告的placementID
@property (nonatomic,copy) NSString *placementID;

/// 广告展示的根视图
@property (strong, nonatomic) FBNativeAdView *adUIView;
/// 广告展示的根视图最大尺寸
@property (nonatomic,assign) CGRect maxRect;
@property (nonatomic,assign) BOOL show;
@property (nonatomic,assign) NSTimeInterval loadADTime;
@end

@implementation FBNativeAD
#pragma mark - init
+ (instancetype)instance{
    static FBNativeAD *_instance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _instance = [[FBNativeAD alloc] init];
    });
    return _instance;
}
- (instancetype)init{
    if (self = [super init]) {
        [self initView];
    }
    return self;
}
- (void)initView{
    //    [FBAdSettings setLogLevel:FBAdLogLevelLog];
    //    [FBAdSettings addTestDevice:[FBAdSettings testDeviceHash]];
    self.loadADTime = 0;
    NSLog(@"testDeviceHash:%@",[FBAdSettings testDeviceHash]);
    _rootView = [UIApplication sharedApplication].keyWindow.rootViewController.view;
}

#pragma mark - bridge
- (void)showFBNativeADs{
    self.show = true;
    self.adUIView.hidden = false;
    NSLog(@"[FBNativeAD]:showFBNativeADs");
}
- (void)hideFBNativeADs{
    [self loadADs];
    self.show = false;
    self.adUIView.hidden = true;
    NSLog(@"[FBNativeAD]:hideFBNativeADs");
}

- (void)initWithPlacementID:(NSString *)placementID{
    [self.adUIView removeFromSuperview];
    self.adUIView = nil;
    self.placementID = placementID;
    FBNativeAd *nativeAd = [[FBNativeAd alloc] initWithPlacementID:placementID];
    nativeAd.delegate = self;
    self.nativeAd = nativeAd;
    [self.nativeAd loadAd];
    NSLog(@"[FBNativeAD]:initWithPlacementID:%@",placementID);
}
- (void)updateMaxRect:(CGRect)rect{
    NSLog(@"[FBNativeAD]:updateFrame:%@",NSStringFromCGRect(self.maxRect));
}
- (bool)isAdValid{
    return self.nativeAd && self.nativeAd.isAdValid;
}
- (void)loadADs{
    long timeStamp = CFAbsoluteTimeGetCurrent();
    if (timeStamp - self.loadADTime > 60){
        self.loadADTime = timeStamp;
        [self initWithPlacementID:self.placementID];
    }
}

#pragma mark - FBNativeAdDelegate
- (void)nativeAdDidLoad:(FBNativeAd *)nativeAd
{
    self.nativeAd = nativeAd;
    [self showNativeAd];
    NSLog(@"[FBNativeAD]:nativeAdDidLoad:%@",nativeAd);
    UnitySendMessage(UnityCallBackObject, "_nativeAdDidLoad", self.nativeAd.placementID.UTF8String);
}

- (void)showNativeAd
{
    if ([self isAdValid]) {
        FBNativeAdView *adView = [FBNativeAdView nativeAdViewWithNativeAd:self.nativeAd
                                                                 withType:FBNativeAdViewTypeGenericHeight300];
        [self.rootView addSubview:adView];
        adView.hidden = !self.show;
        [self.adUIView removeFromSuperview];
        self.adUIView = adView;
        [self.rootView bringSubviewToFront:self.adUIView];
        CGSize size = self.rootView.bounds.size;
        CGFloat W = 320/375.0f * size.width;
        CGFloat xOffset = size.width / 2 - W / 2.0f;
        adView.frame = CGRectMake(xOffset, size.height - 300 - 10, W, 300);
    }
}

- (void)nativeAdDidDownloadMedia:(FBNativeAd *)nativeAd{
    UnitySendMessage(UnityCallBackObject, "_nativeAdDidDownloadMedia", "");
    NSLog(@"[FBNativeAD]:nativeAdDidDownloadMedia:%@",nativeAd);
}
- (void)nativeAd:(FBNativeAd *)nativeAd didFailWithError:(NSError *)error{
    const char *errorString = error != nil ? error.description.UTF8String :"";
    UnitySendMessage(UnityCallBackObject, "_nativeAdDidFailWithError", errorString);
    NSLog(@"[FBNativeAD]:didFailWithError:%@",error);
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(5 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
        [self loadADs];
    });
}
- (void)nativeAdWillLogImpression:(FBNativeAd *)nativeAd{
    NSLog(@"[FBNativeAD]:nativeAdWillLogImpression:%@",nativeAd.extraHint);
    UnitySendMessage(UnityCallBackObject, "_nativeAdWillLogImpression", "");
}

- (void)nativeAdDidClick:(FBNativeAd *)nativeAd
{
    UnitySendMessage(UnityCallBackObject, "_nativeAdDidClick", "");
    NSLog(@"[FBNativeAD]:Native ad was clicked.");
}

- (void)nativeAdDidFinishHandlingClick:(FBNativeAd *)nativeAd
{
    UnitySendMessage(UnityCallBackObject, "_nativeAdDidFinishHandlingClick", "");
    NSLog(@"[FBNativeAD]:Native ad did finish click handling.");
}

#pragma mark - FBMediaViewDelegate
- (void)mediaViewDidLoad:(FBMediaView *)mediaView
{
    NSLog(@"[FBNativeAD]:[MediaViewDelegate]:actual aspect:%f",mediaView.aspectRatio);
}

- (void)mediaViewWillEnterFullscreen:(FBMediaView *)mediaView{
    NSLog(@"[FBNativeAD]:[MediaViewDelegate]:mediaViewWillEnterFullscreen");
}

- (void)mediaViewDidExitFullscreen:(FBMediaView *)mediaView{
    NSLog(@"[FBNativeAD]:[MediaViewDelegate]:mediaViewDidExitFullscreen");
}

- (void)mediaView:(FBMediaView *)mediaView videoVolumeDidChange:(float)volume{
    NSLog(@"[FBNativeAD]:[MediaViewDelegate]:videoVolumeDidChange");
}

- (void)mediaViewVideoDidPause:(FBMediaView *)mediaView{
    NSLog(@"[FBNativeAD]:[MediaViewDelegate]:mediaViewVideoDidPause");
}

- (void)mediaViewVideoDidPlay:(FBMediaView *)mediaView{
    NSLog(@"[FBNativeAD]:[MediaViewDelegate]:mediaViewVideoDidPlay");
}

- (void)mediaViewVideoDidComplete:(FBMediaView *)mediaView{
    NSLog(@"[FBNativeAD]:[MediaViewDelegate]:mediaViewVideoDidComplete");
}


@end
