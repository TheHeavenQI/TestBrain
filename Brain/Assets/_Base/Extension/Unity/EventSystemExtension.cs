using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace BaseFramework {
    public static class EventSystemExtension {
        // ReSharper disable once InconsistentNaming
        public static bool IsPointerOverUIObject(this EventSystem self, Vector3 inputPosition, GameObject uiObject = null) {
            PointerEventData currentPositionEventData = new PointerEventData(self) {
                // 不使用 Input.mousePosition，Input.mousePosition在移动设备
                // 多点触控的时候取的是多点的平均值
                position = inputPosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            self.RaycastAll(currentPositionEventData, results);
            if (uiObject == null) {
                return results.Any();
            }
            foreach (RaycastResult ret in results) {
                if (ret.gameObject == uiObject) {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPointerOverUIObject() {
            //判断是否点击UI
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
                //移动端
                if (Application.platform == RuntimePlatform.Android ||
                            Application.platform == RuntimePlatform.IPhonePlayer) {
                    int fingerId = Input.GetTouch(0).fingerId;
                    if (EventSystem.current.IsPointerOverGameObject(fingerId)) {
                        return true;
                    }
                }
                //其它平台
                else {
                    if (EventSystem.current.IsPointerOverGameObject()) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
