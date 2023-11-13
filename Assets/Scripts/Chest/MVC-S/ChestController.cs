using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Chest.View;
using Chest.Model;
using Chest.SO;
using Chest.State;
using Chest.Service;

namespace Chest.Controller{
    public class ChestController
    {
        public ChestModel ChestModel { get; }
        public ChestView ChestView { get; }

        private CancellationTokenSource cancellationTokenSource;

        public ChestController(ChestSO chestData){
            ChestModel = new ChestModel(chestData);
            ChestView = GameObject.Instantiate<ChestView>(chestData.chestPrefab);
            ChestView.SetController(this);
            ChestView.UpdateView(ChestModel);
        }

        public void OnChestButtonClicked(){ 
            var currentState = ChestModel.ChestState;
            if(currentState == ChestState.Locked && !ChestService.Instance.IsAnyOtherChestUnlocking){
                _ = StartUnlockingChest(ChestModel.TimeToUnlock);
            }else if(currentState == ChestState.Unlocking){
                ChestService.Instance.ShowOpenChestNowPopup(OpenChestPopupCallback);
            }
            else if(currentState == ChestState.Unlocked){
                CollectChestReward();
            }
        }

        private void OpenChestPopupCallback(bool useGems){
            if(useGems){
                var gemsCost = CalculateGemCost(ChestView.RemainingTime);
                ChestService.Instance.UseGems(gemsCost);
                cancellationTokenSource?.Cancel();
                ChangeChestState(ChestState.Unlocked);
            }
        }

        private async Task StartUnlockingChest(float timeToUnlock){
            ChangeChestState(ChestState.Unlocking);
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            try{
                await Task.Delay(((int)timeToUnlock)*1000, token);
                ChangeChestState(ChestState.Unlocked);
            }catch(TaskCanceledException){
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
            await Task.Delay(((int)timeToUnlock)*1000);
            ChangeChestState(ChestState.Unlocked);
        }

        private void CollectChestReward(){
            ChestService.Instance.CollectChestReward(this);
            ChangeChestState(ChestState.Collected);
        }

        private void ChangeChestState(ChestState chestState){
            ChestModel.UpdateChestState(chestState);
            ChestView.UpdateView(ChestModel);
        }

        public int CalculateGemCost(float remainingTime)
        {
            float minutes = remainingTime / 60;
            return Mathf.CeilToInt(minutes / 10);
        }
    }
}