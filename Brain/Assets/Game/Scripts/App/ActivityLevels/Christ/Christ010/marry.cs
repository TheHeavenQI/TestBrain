using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class marry : MonoBehaviour
{
    public GameObject mBox;
    readonly string firsAniName = "marry1";
    readonly string secondAniName = "marry2";
    //readonly List<string> mDiaolog = new List<string>()
    //{
    //    "May all your wishes come true!",
    //    "2020 is going to be one of the best years!",
    //    "May joy and health be with you always!",
    //    "A cheery New Year hold lots of happiness for you!",
    //    "May you have the best New Year ever!",
    //    "May ever day be brilliant for you in the New Year!",
    //    "Wish you a brand new beginning at the New Year!",
    //    "Good luck in the year ahead!",
    //    "May the season’s joy fill you all the year around!",
    //    "Good luck and great success in the coming year!"
    //};
    public Animation mAni;
    private void Start()
    {
        Canvas ca = GetComponent<Canvas>();
        ca.sortingLayerName = "PopUp";
        mAni.Play(firsAniName);
        StartCoroutine("playLoopAni");
    }
    private IEnumerator playLoopAni()
    {
        while (mAni.isPlaying)
        {
            yield return null;
        }
        mBox.SetActive(true);
        mAni.Play(secondAniName);
    }
    public void ClickGetBtn()
    {
        mBox.SetActive(false);
    }
    public void ClickCLose()
    {
        Destroy(gameObject);
    }
}
