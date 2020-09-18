using System;
using System.Collections.Generic;

public static class ListExtend
{
    public static List<T> RandomList<T>(this List<T> sourceList){
        if (sourceList == null || sourceList.Count <= 1) {
            return sourceList;
        }
        List<T> randomList = new List<T>();
        Random ran = new Random();
        for (int i = 0; i < sourceList.Count; i++) {
            int n = ran.Next(0, i);
            randomList.Insert(n,sourceList[i]);
        }
        return randomList;
    }
   
}



