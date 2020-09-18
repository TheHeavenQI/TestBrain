using UnityEngine;

public static class RectTransformExtensions
{
    public static float ScreenWidth() {
        return 750;
    }

    public static float ScreenHeight() {
        var h = Screen.height / (float)Screen.width * 750;
        return h;
    }
    public static bool Overlaps(this RectTransform a, RectTransform b)
    {
        return a.WorldRect().Overlaps(b.WorldRect());
    }

    public static Rect WorldRect(this RectTransform rectTransform)
    {
        UnityEngine.Vector2 sizeDelta = rectTransform.sizeDelta;
        float rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
        float rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;

        UnityEngine.Vector3 position = rectTransform.position;
        return new Rect(position.x + rectTransformWidth * rectTransform.pivot.x, position.y - rectTransformHeight * rectTransform.pivot.y, rectTransformWidth, rectTransformHeight);
    }
    /// <summary>
    /// 仅适用于同一ugui父级下相同对齐方式
    /// </summary>
    /// <param name="rect1"></param>
    /// <param name="rect2"></param>
    /// <returns></returns>
    public static bool IsRectTransformOverlap(this RectTransform rect1, RectTransform rect2)
    {
        return IsRectTransformOverlap(rect1.localPosition, rect1.sizeDelta,rect2.localPosition,rect2.sizeDelta);
    }
    public static bool IsRectTransformOverlap(UnityEngine.Vector2 rect1RefPos, UnityEngine.Vector2 rect1Size, UnityEngine.Vector2 rect2RefPos, UnityEngine.Vector2 rect2Size)
    {
        float rect1MinX = rect1RefPos.x - rect1Size.x / 2;
        float rect1MaxX = rect1RefPos.x + rect1Size.x / 2;
        float rect1MinY = rect1RefPos.y - rect1Size.y / 2;
        float rect1MaxY = rect1RefPos.y + rect1Size.y / 2;

        float rect2MinX = rect2RefPos.x - rect2Size.x / 2;
        float rect2MaxX = rect2RefPos.x + rect2Size.x / 2;
        float rect2MinY = rect2RefPos.y - rect2Size.y / 2;
        float rect2MaxY = rect2RefPos.y + rect2Size.y / 2;

        bool xNotOverlap = rect1MaxX <= rect2MinX || rect2MaxX <= rect1MinX;
        bool yNotOverlap = rect1MaxY <= rect2MinY || rect2MaxY <= rect1MinY;

        bool notOverlap = xNotOverlap || yNotOverlap;

        return !notOverlap;
    }
}
