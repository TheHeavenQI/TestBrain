//
//  NativePurchase.h
//  Unity-iPhone
//
//  Created by yz on 2020/1/14.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

void NativeiOSPurchase_initProducts(char *ids);
void NativeiOSPurchase_purchase(char *productID);
void NativeiOSPurchase_restore(void);

@interface NativeiOSPurchase : NSObject
+ (void)initProducts:(NSString *)ids;
+ (void)purchase:(NSString *)productID;
+ (void)restore;
@end

NS_ASSUME_NONNULL_END
