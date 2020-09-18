using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level302 : LevelBasePage
{
    public Button yesBtn;
    public Button noBtn;
    public GameObject bodyPrefab;
    public CustomEventTrigger headEye;
    private List<GameObject> _bodys = new List<GameObject>();

    protected override void Start()
    {
        base.Start();

        yesBtn.onClick.AddListener(() => {
            if (headEye.isPress || Input.GetKey(KeyCode.Space))
            {
                Completion();
            }
            else
            {
                AddBody();
            }
        });
        noBtn.onClick.AddListener(() => {
            if (headEye.isPress || Input.GetKey(KeyCode.Space))
            {
                Completion();
            }
            else
            {
                AddBody();
            }
        });
    }

    public override void Refresh()
    {
        base.Refresh();

        foreach (GameObject body in _bodys)
        {
            Object.Destroy(body);
        }
        _bodys.Clear();
    }

    private void AddBody()
    {
        GameObject body = Instantiate<GameObject>(bodyPrefab);
        body.transform.SetParent(bodyPrefab.transform.parent, false);
        body.transform.SetAsFirstSibling();
        body.transform.localScale = Vector3.one;
        _bodys.Add(body);
    }
}
