using Chest.View;
using Slot.View;
using UnityEngine;

namespace Slot.Controller{
    public class SlotController
    {
        private bool isEmpty;
        public bool IsEmpty { get => isEmpty; private set => isEmpty = value; }

        private SlotView slotView;

        public ChestView ChestView { get; private set; }

        public SlotController(SlotView slotPrefab, Transform slotPrefabParent){
            IsEmpty = true;
            slotView = GameObject.Instantiate<SlotView>(slotPrefab, slotPrefabParent);
            slotView.SetController(this);
            slotView.UpdateView();
        }

        public void PopulateSlot(ChestView chestView){
            IsEmpty = false;
            this.ChestView = chestView;
            slotView.UpdateView(chestView);
        }

        public void VacateSlot(){
            isEmpty = true;
            ChestView = null;
            slotView.UpdateView();
        }
    }
}