using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Chest.Controller;
using Chest.SO;
using Slot.Service;
using UI.Service;
using Player.Service;
using System.Linq;

namespace Chest.Service{
    public class ChestService : GenericMonoSingleton<ChestService>
    {
        private List<ChestController> chestControllers;
        [SerializeField] private ChestSO[] chestsSO;
        [SerializeField] private Button generateChestsButton;

        private void Start(){
            chestControllers = new List<ChestController>();
            generateChestsButton.onClick.AddListener(OnGenerateChestButtonClicked);
        }

        private void OnGenerateChestButtonClicked(){
            if(SlotsService.Instance.IsAnySlotEmpty){
                int randomSelection = UnityEngine.Random.Range(0, chestsSO.Length);
                var chestController = new ChestController(chestsSO[randomSelection]);
                chestControllers.Add(chestController);
                SlotsService.Instance.PopulateSlot(chestController.ChestView);
            }else{
                Debug.Log("Chest slots are full, cannot create more");
            }
        }

        public void ShowOpenChestNowPopup(Action<bool> callback){
            UIService.Instance.ShowOpenChestNowPopup(callback);
        }

        public void UseGems(int gemsUsed){
            PlayerService.Instance.UseGems(gemsUsed);
        }

        public void CollectChestReward(ChestController chestController){
            PlayerService.Instance.CollectChestReward(chestController.ChestModel.ChestReward);
            SlotsService.Instance.RemoveChestFromSlot(chestController.ChestView);
            chestControllers.Remove(chestController);
            chestControllers[0].OnChestButtonClicked();
        }

        public bool IsAnyOtherChestUnlocking => chestControllers.Any(x => x.ChestModel.ChestState == State.ChestState.Unlocking);
    }
}