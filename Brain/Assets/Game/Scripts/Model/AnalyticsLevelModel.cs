using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsLevelModel {
    public float start_time;
    public float end_time;
    
    public bool click_menu;
    public bool click_levels;
    public bool click_refresh;
    public bool click_skip;
    public bool click_tips;
    
    public bool ad_tips_skip_show;
    public bool ad_tips_skip_finish;
    public bool ad_tips_skip_skip;
    public bool ad_tips_skip_close;
    
    public bool ad_tips_show;
    public bool ad_tips_finish;
    public bool ad_tips_skip;
    public bool ad_tips_close;


    public bool ad_tips_finish_show;
    public bool ad_tips_finish_finish;
    public bool ad_tips_finish_skip;
    public bool ad_tips_finish_close;
        
    public bool ad_use_tips;
    public bool ad_use_skip;

    public Dictionary<string, object> ToDict() {
        var dict = new Dictionary<string, object>();
        dict["duration"] = end_time - start_time;
       
        dict["click_menu"] = click_menu ? 1 : 0;
        dict["click_levels"] = click_levels ? 1 : 0;
        dict["click_refresh"] = click_refresh ? 1 : 0;
        dict["click_skip"] = click_skip ? 1 : 0;
        dict["click_tips"] = click_tips ? 1 : 0;
        
        dict["ad_tips_skip_show"] = ad_tips_skip_show ? 1 : 0;
        dict["ad_tips_skip_finish"] = ad_tips_skip_finish ? 1 : 0;
        dict["ad_tips_skip_skip"] = ad_tips_skip_skip ? 1 : 0;
        dict["ad_tips_skip_close"] = ad_tips_skip_close ? 1 : 0;
        
        dict["ad_tips_show"] = ad_tips_show ? 1 : 0;
        dict["ad_tips_finish"] = ad_tips_finish ? 1 : 0;
        dict["ad_tips_skip"] = ad_tips_skip ? 1 : 0;
        dict["ad_tips_close"] = ad_tips_close ? 1 : 0;
        
        dict["ad_tips_finish_show"] = ad_tips_finish_show ? 1 : 0;
        dict["ad_tips_finish_finish"] = ad_tips_finish_finish ? 1 : 0;
        dict["ad_tips_finish_skip"] = ad_tips_finish_skip ? 1 : 0;
        dict["ad_tips_finish_close"] = ad_tips_finish_close ? 1 : 0;

        dict["ad_use_tips"] = ad_use_tips ? 1 : 0;
        dict["ad_use_skip"] = ad_use_skip ? 1 : 0;
        dict["tipsCount"] = UserModel.Get().keyCount;
        
        return dict;
    }
}
