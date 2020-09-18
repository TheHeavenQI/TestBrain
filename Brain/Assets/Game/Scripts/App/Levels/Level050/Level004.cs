using System.Collections.Generic;

public class Level004 : LevelBasePage {

    public List<DragMoveEventTrigger> allButtons;
    public DragMoveEventTrigger correctButton;

    protected override void Start() {
        base.Start();

        if (correctButton != null && !allButtons.Contains(correctButton)) {
            allButtons.Add(correctButton);
        }

        for (int i = 0; i < allButtons.Count; i++) {
            DragMoveEventTrigger btn = allButtons[i];
            btn.onPointerClick = (d) => {
                if ((correctButton != null && btn == correctButton)) {
                    CompletionWithMousePosition();
                } else {
                    ShowErrorWithMousePosition();
                }
            };
        }
    }

    public override void Refresh() {
        base.Refresh();
        allButtons.ForEach(it => it.Return2OriginPos());
    }
}
