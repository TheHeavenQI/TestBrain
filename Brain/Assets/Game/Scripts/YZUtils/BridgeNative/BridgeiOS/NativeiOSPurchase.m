//
//  NativePurchase.m
//  Unity-iPhone
//
//  Created by yz on 2020/1/14.
//

#import "NativeiOSPurchase.h"
#import <RMStore/RMStore.h>
static char *const UnityCallBackObject = "NativeiOSPurchase";
static NSString * SepString = @"<__>";

void SendMessageToUnity(const char *func,const char *msg){
    if (msg == NULL) {
        msg = "";
    }
    NSLog(@"[NativeiOS]:[NativeiOSPurchase]:%s %s",func,msg);
    dispatch_async(dispatch_get_main_queue(), ^{
        UnitySendMessage(UnityCallBackObject, func, msg);
    });
}

void NativeiOSPurchase_initProducts(char *ids){
    [NativeiOSPurchase initProducts:[NSString stringWithCString:ids encoding:NSUTF8StringEncoding]];
}
void NativeiOSPurchase_purchase(char *productID){
    [NativeiOSPurchase purchase:[NSString stringWithCString:productID encoding:NSUTF8StringEncoding]];
}
void NativeiOSPurchase_restore(void){
    [NativeiOSPurchase restore];
}

@implementation NativeiOSPurchase
+ (void)initProducts:(NSString *)ids{
    NSArray *array = [ids componentsSeparatedByString:@","];
    NSSet *products = [NSSet setWithArray:array];
    [[RMStore defaultStore] requestProducts:products success:^(NSArray<SKProduct *> *products, NSArray *invalidProductIdentifiers) {
        if (products.count > 0) {
            NSMutableArray *arrayM = [NSMutableArray array];
            for (SKProduct *product in products) {
                [arrayM addObject:product.productIdentifier];
            }
            SendMessageToUnity("OnInitialize", [arrayM componentsJoinedByString:@","].UTF8String);
        }else{
            dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(5 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
                [NativeiOSPurchase initProducts:ids];
            });
            SendMessageToUnity("OnInitializeFailed", "No products available for purchase!");
        }
    } failure:^(NSError *error) {
        SendMessageToUnity("OnInitializeFailed", error.localizedDescription.UTF8String ?: "");
        dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(5 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
            [NativeiOSPurchase initProducts:ids];
        });
    }];
}
+ (void)purchase:(NSString *)productID{
    [[RMStore defaultStore] addPayment:productID success:^(SKPaymentTransaction *transaction) {
        if (transaction.transactionState == SKPaymentTransactionStatePurchased
            ||transaction.transactionState == SKPaymentTransactionStateRestored) {
            SendMessageToUnity("OnPurchased", productID.UTF8String);
        }else{
            NSString *state = @"Unkown";
            switch (transaction.transactionState) {
                case SKPaymentTransactionStatePurchasing:
                    state = @"SKPaymentTransactionStatePurchasing";
                    break;
                case SKPaymentTransactionStatePurchased:
                    state = @"SKPaymentTransactionStatePurchased";
                    break;
                case SKPaymentTransactionStateFailed:
                    state = @"SKPaymentTransactionStateFailed";
                    break;
                case SKPaymentTransactionStateRestored:
                    state = @"SKPaymentTransactionStateRestored";
                    break;
                case SKPaymentTransactionStateDeferred:
                    state = @"SKPaymentTransactionStateDeferred";
                    break;
            }
            const char *msg = [@[productID,state] componentsJoinedByString:SepString].UTF8String;
            SendMessageToUnity("OnPurchasedFailed", msg);
        }
    } failure:^(SKPaymentTransaction *transaction, NSError *error) {
        const char *msg = [@[productID,error.localizedDescription ?: @""] componentsJoinedByString:SepString].UTF8String;
        SendMessageToUnity("OnPurchasedFailed", msg);
    }];
}
+ (void)restore{
    [[RMStore defaultStore] restoreTransactionsOnSuccess:^(NSArray<SKPaymentTransaction *> *transactions) {
        if(transactions.count > 0){
            NSMutableArray *arrayM = [NSMutableArray array];
            for (SKPaymentTransaction *transaction in transactions) {
                if (transaction.transactionState == SKPaymentTransactionStatePurchased
                    ||transaction.transactionState == SKPaymentTransactionStateRestored) {
                    [arrayM addObject:transaction.payment.productIdentifier];
                }else{
                    NSLog(@"[NativeiOS]:[NativeiOSPurchase]:restore:%@,%ld",transaction.payment.productIdentifier,(long)transaction.transactionState);
                }
            }
            if(arrayM.count > 0){
                SendMessageToUnity("OnRestored", [arrayM componentsJoinedByString:@","].UTF8String);
            }else{
                SendMessageToUnity("OnRestoreFailed", "No products available for restore!!!!!");
            }
        }else{
            SendMessageToUnity("OnRestoreFailed", "No products available for restore!");
        }
    } failure:^(NSError *error) {
        SendMessageToUnity("OnRestoreFailed", error.localizedDescription.UTF8String);
    }];
}
@end
