//
//  NativeiOS.m
//  Unity-iPhone
//
//  Created by yz on 2019/12/31.
//

#import "NativeiOS.h"
#import <UIKit/UIKit.h>
#import "AppsFlyerTracker.h"
//#import <RMStore/RMStore.h>
#import <AdSupport/AdSupport.h>
NSDictionary *dictionaryWithJsonString(NSString *jsonString)
{
    if (jsonString == nil) {
        return nil;
    }
    
    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err;
    NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData
                                                        options:NSJSONReadingMutableContainers
                                                          error:&err];
    if(err)
    {
        NSLog(@"json解析失败：%@",err);
        return nil;
    }
    return dic;
}

char * getLocaleCountryCode(void){
    NSString *CountryCode = [[NSLocale currentLocale] objectForKey:NSLocaleCountryCode];
    const char * code = [CountryCode UTF8String];
    char *copy = malloc(strlen(code) + 1);
    strcpy(copy, code);
    return copy;
}

bool getiOSDebug(void){
#if DEBUG
    return true;
#endif
    return false;
}

float getBrightness(void){
    return [UIScreen mainScreen].brightness;
}
void setBrightness(float value){
    [[UIScreen mainScreen] setBrightness:value];
}

void uploadReceiptData(char *urlString, char *value){
    
    NSMutableDictionary *params = [NSMutableDictionary dictionary];
    NSURL *receiptUrl = [[NSBundle mainBundle] appStoreReceiptURL];
    NSData *receiptData = [NSData dataWithContentsOfURL:receiptUrl];
    NSString *receiptString = [receiptData base64EncodedStringWithOptions:0];
    params[@"af_id"] = [[AppsFlyerTracker sharedTracker] getAppsFlyerUID];
    params[@"idfa"] = [[[ASIdentifierManager sharedManager] advertisingIdentifier] UUIDString];
    params[@"bundle_id"] = [[NSBundle mainBundle] bundleIdentifier];
    params[@"timestamp"] = @((long)[[NSDate date] timeIntervalSince1970]);
    params[@"subscribe_id"] = receiptString;
    
    if(value != NULL && strlen(value) > 0){
        NSString *str = [NSString stringWithCString:value encoding:NSUTF8StringEncoding];
        NSDictionary *dict = dictionaryWithJsonString(str);
        for (NSString *key in dict) {
            id value = dict[key];
            params[key] = value;
        }
    }
    NSLog(@"[NativeiOS]:[uploadReceiptData]:%s : %@",urlString,params);
    NSURL *url = [NSURL URLWithString:[NSString stringWithCString:urlString encoding:NSUTF8StringEncoding]];
    NSMutableURLRequest *requestM = [NSMutableURLRequest requestWithURL:url];
    
    requestM.HTTPBody = [NSJSONSerialization dataWithJSONObject:params options:(NSJSONReadingAllowFragments) error:nil];
    requestM.HTTPMethod = @"POST";
    [requestM setValue:@"application/json" forHTTPHeaderField:@"Content-Type"];
    
    //    req
    [[[NSURLSession sharedSession] dataTaskWithRequest:requestM completionHandler:^(NSData * _Nullable data, NSURLResponse * _Nullable response, NSError * _Nullable error) {
        NSHTTPURLResponse *httpResponse = (NSHTTPURLResponse *)response;
        NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:nil];
        NSLog(@"[NativeiOS]:[uploadReceiptData]:verifyReceipt:%ld %@ %@",(long)httpResponse.statusCode,dic,error);
    }] resume];
}


//void appStoreReceipt(){
//    NSURL *receiptUrl = [[NSBundle mainBundle] appStoreReceiptURL];
//    NSData *receiptData = [NSData dataWithContentsOfURL:receiptUrl];
//    NSString *receiptString = [receiptData base64EncodedStringWithOptions:0];
//    NSString *bodyString = [NSString stringWithFormat:@"{\"receipt-data\" : \"%@\",\"password\" :\"2fdf7793816a450980a8e247be58bf37\"}", receiptString];
//    NSData *bodyData = [bodyString dataUsingEncoding:NSUTF8StringEncoding];
//    // 创建请求到苹果官方进行购买验证
//    NSURL *url = [NSURL URLWithString:@"https://sandbox.itunes.apple.com/verifyReceipt"];
//    NSMutableURLRequest *requestM = [NSMutableURLRequest requestWithURL:url];
//    requestM.HTTPBody = bodyData;
//    requestM.HTTPMethod = @"POST";
//    //    req
//    [[[NSURLSession sharedSession] dataTaskWithRequest:requestM completionHandler:^(NSData * _Nullable data, NSURLResponse * _Nullable response, NSError * _Nullable error) {
//
//        if (error) {
//            NSLog(@"[uploadReceiptData]:验证购买过程中发生错误，错误信息：%@",error.localizedDescription);
//            return;
//        }
//
//        NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:nil];
//        NSLog(@"[uploadReceiptData]:verifyReceipt:%@",dic);
//    }] resume];
//}

