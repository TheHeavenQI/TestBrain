//
//  FBNativeAD.h
//  ADTest
//
//  Created by yz on 2019/12/17.
//  Copyright © 2019年 yz. All rights reserved.
//

#import <UIKit/UIKit.h>

#pragma mark - C
void _initFBNativeADsWithPlacementID(char *placementID);
void _showFBNativeADs(void);
void _hideFBNativeADs(void);
bool _FBNativeADsIsAdValid(void);
void _FBNativeADsLoad(void);

#pragma mark - OC

NS_ASSUME_NONNULL_BEGIN

@interface FBNativeAD : NSObject
+ (instancetype)instance;
- (void)initWithPlacementID:(NSString *)placementID;
- (void)showFBNativeADs;
- (void)hideFBNativeADs;
- (void)updateMaxRect:(CGRect)rect;
- (bool)isAdValid;
- (void)loadADs;
@end

NS_ASSUME_NONNULL_END
