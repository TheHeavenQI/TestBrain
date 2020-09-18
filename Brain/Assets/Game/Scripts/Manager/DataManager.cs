
using UnityEngine;

public class DataManager {
    private static DataManager _instance;
    public static DataManager Instance() {
        if (_instance == null) {
            _instance = new DataManager();
        }
        return _instance;
    }
    
//    public UserModel userModel;
    private DataManager() {
//        userModel = UserModel.Get();
    }
    
}
