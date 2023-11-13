using Chest.View;
using Slot.Controller;
using UnityEngine;

namespace Slot.View{
    public class SlotView : MonoBehaviour
    {
        private SlotController slotController;
        [SerializeField] private Transform emptySlot;
        [SerializeField] private Transform chestPlaceholder;

        public void SetController(SlotController slotController){
            this.slotController = slotController;
        }

        public void UpdateView(ChestView chestView = null){
            if(chestView == null){
                chestPlaceholder.gameObject.SetActive(false);
                emptySlot.gameObject.SetActive(true);
            }else{
                chestPlaceholder.gameObject.SetActive(true);
                chestView.transform.SetParent(chestPlaceholder);
                ResetChestViewTransform(chestView);
                emptySlot.gameObject.SetActive(false);
            }
        }

        private void ResetChestViewTransform(ChestView chestView){
            chestView.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            chestView.transform.localScale = Vector3.one;
        }
    }
}