using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class MyBuildPostprocessor {
    [PostProcessBuildAttribute(999)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path) {
        if(buildTarget == BuildTarget.iOS)
        {
            /// 去掉bitcode
            if (!string.IsNullOrEmpty(AppSetting.BUGLY_APP_ID_IOS)) {
                string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

                PBXProject pbxProject = new PBXProject();
                pbxProject.ReadFromFile(projectPath);

                string target = pbxProject.TargetGuidByName("Unity-iPhone");            
                pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

                pbxProject.WriteToFile (projectPath);
            }
            
           
            // Get plist
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            // Get root
            PlistElementDict rootDict = plist.root;
            /// 添加GADApplicationIdentifier 到info.plist
            if (!string.IsNullOrEmpty(AppSetting.GADApplicationIdentifier_IOS)) {
                var buildKey = "GADApplicationIdentifier";
                rootDict.SetString(buildKey,AppSetting.GADApplicationIdentifier_IOS);
            }
            //见bug https://groups.google.com/forum/#!category-topic/google-admob-ads-sdk/ios/I4EEWrPPbSc
            rootDict.SetString("gad_preferred_webview","wkwebview");
            PlistElementDict security = (PlistElementDict)rootDict["NSAppTransportSecurity"];
            PlistElementDict exceptionDomains = (PlistElementDict)security.CreateDict("NSExceptionDomains");
            PlistElementDict domains = exceptionDomains.CreateDict("shapekeeper.net");
            domains.SetBoolean("NSIncludesSubdomains",true);
            domains.SetBoolean("NSExceptionAllowsInsecureHTTPLoads",true);
            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());

        }
        
       
        
    }
}