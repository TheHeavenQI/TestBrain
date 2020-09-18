#
# Be sure to run `pod lib lint MoPub-Chartboost-Adapters.podspec' to ensure this is a
# valid spec before submitting.
#

Pod::Spec.new do |s|
s.name             = 'MoPub-Chartboost-Adapters'
s.version          = '8.1.0.1'
s.summary          = 'Chartboost Adapters for mediating through MoPub.'
s.description      = <<-DESC
Supported ad formats: Banner, Interstitial, Rewarded Video.\n
To download and integrate the Chartboost SDK, please check this tutorial: https://answers.chartboost.com/en-us/child_article/ios \n\n
For inquiries and support, please reach out to https://answers.chartboost.com/en-us/zingtree. \n
DESC
s.homepage         = 'https://github.com/mopub/mopub-ios-mediation'
s.license          = { :type => 'New BSD', :file => 'LICENSE' }
s.author           = { 'MoPub' => 'support@mopub.com' }
s.source           = { :git => 'https://github.com/mopub/mopub-ios-mediation.git', :tag => "chartboost-#{s.version}" }
s.ios.deployment_target = '9.0'
s.static_framework = true
s.subspec 'MoPub' do |ms|
  ms.dependency 'mopub-ios-sdk/Core', '~> 5.6'
end
s.subspec 'Network' do |ns|
  ns.source_files = '*.{h,m}'
  ns.dependency 'ChartboostSDK', '8.1.0'
  ns.dependency 'mopub-ios-sdk/Core', '~> 5.6'
end
end
