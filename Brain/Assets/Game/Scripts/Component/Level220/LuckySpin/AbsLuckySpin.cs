using UnityEngine;
using BaseFramework;

/// <summary>
/// 大转盘
/// </summary>
public abstract class AbsLuckySpin : MonoBehaviour {

    protected enum State {
        Run,
        Wait2Cut,
        ReadyCut,
        Cuting,
        Stop
    }

    public float startSpeed = 300;
    public float delayToStartCut = 0.5f;

    public bool isAutoRun = false;
    public bool isAutoStop = false;

    protected State state = State.Run;

    protected float targetAngle = 0;
    protected Quaternion taregtQua;

    protected float delayToWaitFinishCall = 0.1f;

    /// <summary>
    /// 摇奖
    /// </summary>
    public void TurnToStop() {
        if (state == State.Run) {
            state = State.Wait2Cut;
            //Invoke("WaitToSpeedCut", delayToStartCut);
            this.Delay(delayToStartCut).Do(WaitToSpeedCut).Execute();
        }
    }

    public void StartTurn() {
        state = State.Run;
    }

    public void StartTurn(float delayToStop) {
        if(state == State.Run)
        {
            return;
        }
        state = State.Run;
        //Invoke("TurnToStop", delayToStopTurnToStop
        this.Delay(delayToStop).Do(TurnToStop).Execute();
    }

    protected virtual void OnEnable() {
        if (isAutoRun) {
            state = State.Run;
            if (isAutoStop) {
                TurnToStop();
            }
        } else {
            state = State.Stop;
        }
    }

    private void Update() {
        switch (state) {
            case State.Run:
            case State.Wait2Cut:
                transform.Rotate(Vector3.forward * startSpeed * Time.deltaTime);
                break;
            case State.ReadyCut:
                transform.Rotate(Vector3.forward * startSpeed * Time.deltaTime);
                CheckReadyCut();
                break;
            case State.Cuting:
                Quaternion qua = transform.rotation;
                transform.rotation = Quaternion.Slerp(qua, taregtQua, Time.deltaTime * startSpeed / 200f);
                if (IsTurnTarget()) {
                    //Invoke("WaitOnFinishCall", delayToWaitFinishCall);
                    this.Delay(delayToWaitFinishCall).Do(WaitOnFinishCall).Execute();
                    state = State.Stop;
                }
                break;
            case State.Stop:
            default:
                break;
        }
    }


    /// <summary>
    /// 强制停止，将不会有结束回调
    /// </summary>
    public void ForceStop()
    {
        state = State.Stop;
    }

    public bool IsStoped()
    {
        return state == State.Stop;
    }

    protected abstract float GetTargetAngle();

    protected virtual void WaitOnFinishCall() {

    }

    private void WaitToSpeedCut() {
        targetAngle = GetTargetAngle();
        taregtQua = Quaternion.Euler(0, 0, targetAngle);
        state = State.ReadyCut;
    }

    private bool IsTurnTarget() {
        if (GetAngleDiff() < 1 || Mathf.Abs(GetAngleDiff() - 360) < 1) {
            return true;
        }
        return false;
    }

    private void CheckReadyCut() {
        float diff = GetAngleDiff();
        if (diff >= 168 && diff <= 178) {
            state = State.Cuting;
        }
    }

    private float GetAngleDiff() {
        float angleZ = transform.localEulerAngles.z;
        float result = 0;
        result = targetAngle + 360 - angleZ;
        result = ClampAngle(result);
        return result;
    }

    public float ClampAngle(float angle) {
        while (angle > 360) {
            angle -= 360;
        }
        while (angle < 0) {
            angle += 360;
        }
        return angle;
    }
}
