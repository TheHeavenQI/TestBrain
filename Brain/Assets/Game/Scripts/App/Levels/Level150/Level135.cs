
public class Level135 : InputNumLevel {
    public EventCallBack nain1;
    public EventCallBack nain2;
    protected override void Start() {
        base.Start();
        nain1.gameObject.SetActive(true);
        nain2.gameObject.SetActive(false);
        nain1.onClick = () => {
            if (nain2.gameObject.activeSelf) {
                nain2.gameObject.SetActive(false);
            }
            else {
                nain2.gameObject.SetActive(true);
            }
        };
//        nain1.onSwipe = (value) => {
//            if (value == SwipeDirection.Left) {
//                nain2.gameObject.SetActive(true);
//            }
//        };
//        nain2.onSwipe = (value) => {
//            if (value == SwipeDirection.Right) {
//                nain2.gameObject.SetActive(false);
//            }
//        };
    }

    public override void Refresh() {
        base.Refresh();
        nain1.gameObject.SetActive(true);
        nain2.gameObject.SetActive(false);
    }
}
