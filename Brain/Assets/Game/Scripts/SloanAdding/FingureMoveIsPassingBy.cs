using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FingureMoveIsPassingBy : MonoBehaviour {

    public Action<GameObject> fingureMovePassByCallBack;
    private Vector2 touchPosition;
    private Vector2? lastPosition;

    private void Update() {

        if (Input.GetMouseButton(0) || Input.touchCount > 0) {

            touchPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (lastPosition == null) {
                lastPosition = touchPosition;
            }

            Ray2D ray = new Ray2D(lastPosition.Value, touchPosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, (touchPosition - lastPosition.Value).magnitude);

            if (hit.collider != null) {
                fingureMovePassByCallBack(hit.collider.gameObject);
            }
        }
    }
}
