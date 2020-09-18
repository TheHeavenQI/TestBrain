using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;
using System.Linq;

public class Christ001 : ChristBaseLevel
{
    public static Christ001 instance { get; private set; }

    public Christ001KlotskiBlock giftBox;

    private List<Christ001KlotskiBlock> _allKlotskiBlocks;
    private int _step = 0;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        _allKlotskiBlocks = this.GetComponentsInChildren<Christ001KlotskiBlock>().ToList();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public override void Refresh()
    {
        base.Refresh();
        foreach (var kb in _allKlotskiBlocks)
        {
            kb.Return2OriginPos();
            kb.gameObject.SetActive(true);
            kb.isLevelCompletion = false;
        }
        _step = 0;
    }

    public void AddStep()
    {
        ++_step;
    }

    private void RemoveBlock()
    {
        foreach (var kb in _allKlotskiBlocks)
        {
            if (kb.gameObject.activeSelf && kb != giftBox)
            {
                kb.gameObject.SetActive(false);
                return;
            }
        }
    }

    protected override void OnTipsDialogCloseWithTipsUsed()
    {
        base.OnTipsDialogCloseWithTipsUsed();
        RemoveBlock();
    }
}
