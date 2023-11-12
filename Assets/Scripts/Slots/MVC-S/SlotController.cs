using UnityEngine;

public class SlotController
{
    private bool isEmpty;
    public bool IsEmpty { get => isEmpty; private set => isEmpty = value; }

    private SlotView slotView;

    public SlotController(SlotView slotPrefab, Transform slotPrefabParent){
        IsEmpty = true;
        slotView = GameObject.Instantiate<SlotView>(slotPrefab, slotPrefabParent);
        slotView.SetController(this);
        slotView.UpdateView();
    }

    public void PopulateSlot(ChestView chestView){
        IsEmpty = false;
        slotView.UpdateView(chestView);
    }
}
