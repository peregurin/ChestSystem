using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Utility;
using Slot.View;
using Slot.Controller;
using Chest.View;

namespace Slot.Service{
    public class SlotsService : GenericMonoSingleton<SlotsService>
    {
        private const int NUMBER_OF_SLOTS = 8;
        [SerializeField] private SlotView slotPrefab;
        [SerializeField] private Transform slotPrefabParent;
        private List<SlotController> slotControllers;

        private void Start(){
            slotControllers = new List<SlotController>();
            for(int i=0;i<NUMBER_OF_SLOTS;i++){
                GenerateSlots();
            }
        }

        private void GenerateSlots(){
            var slotController = new SlotController(slotPrefab, slotPrefabParent);
            slotControllers.Add(slotController);
        }

        public void PopulateSlot(ChestView chestView){
            foreach(var slot in slotControllers){
                if(slot.IsEmpty){
                    slot.PopulateSlot(chestView);
                    break;
                }
            }
        }

        public void RemoveChestFromSlot(ChestView chestView){
            foreach(var slot in slotControllers){
                if(slot.ChestView == chestView){
                    slot.VacateSlot();
                    break;
                }
            }
        }

        public bool IsAnySlotEmpty => slotControllers.Any(x => x.IsEmpty);
    }
}
