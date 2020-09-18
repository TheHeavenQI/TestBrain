using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level180 : LevelBasePage {

    public CustomEventTrigger numDM;
    private float _speed = 3;
    private Vector2 _lastPoint;

    protected override void Start() {
        base.Start();

        numDM.onBeginDrag += (data) => {
            _lastPoint = data.position;
        };

        numDM.onDrag += (data) => {
            Vector3 angle = numDM.transform.localEulerAngles;
            if (data.position.x > _lastPoint.x || data.position.y > _lastPoint.y) {
                angle.z -= _speed;
            } else if (data.position.x < _lastPoint.x || data.position.y < _lastPoint.y) {
                angle.z += _speed;
            }
            numDM.transform.localEulerAngles = angle;
            _lastPoint = data.position;
        };

        numDM.onEndDrag += (data) => {
            if (Mathf.Abs(Mathf.Abs(numDM.transform.localEulerAngles.z) - 180) < 15) {
                Completion();
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        numDM.transform.localEulerAngles = Vector3.zero;
    }
}
