using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Chest.Controller;
using Chest.Model;
using Chest.State;

namespace Chest.View{

    [RequireComponent(typeof(Button))]
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;
        private Button chestButton;

        private float remainingTime;

        [SerializeField] private Image bgImage;
        [SerializeField] private Image chestImage;
        [SerializeField] private Transform locked;
        [SerializeField] private TextMeshProUGUI timeToUnlock;
        [SerializeField] private Transform unlocking;
        [SerializeField] private TextMeshProUGUI pendingTimeToUnlock;
        [SerializeField] private TextMeshProUGUI openNowCost;
        [SerializeField] private Transform unlocked;

        public float RemainingTime { get => remainingTime; private set => remainingTime = value; }

        public void SetController(ChestController chestController){
            this.chestController = chestController;
            RemainingTime = chestController.ChestModel.TimeToUnlock;
        }

        private void Start(){
            if(chestButton == null){
                chestButton = GetComponent<Button>();
            }
            chestButton.onClick.AddListener(OnChestButtonClicked);
        }

        private void OnChestButtonClicked(){
            chestController.OnChestButtonClicked();
        }

        public void UpdateView(ChestModel chestModel){
            SetBG(chestModel.BgColor);
            SetChestImage(chestModel.ChestSprite);
            SetChestStateUI(chestModel);
        }

        private void SetBG(Color bgColor){
            bgImage.color = bgColor;
        }

        private void SetChestImage(Sprite chestSprite){
            chestImage.sprite = chestSprite; 
        }

        private void SetChestStateUI(ChestModel chestModel) 
        {
            locked.gameObject.SetActive(false);
            unlocking.gameObject.SetActive(false);
            unlocked.gameObject.SetActive(false);
        
            switch (chestModel.ChestState)
            {
                case ChestState.Locked:
                    SetLockedState(chestModel.TimeToUnlock);
                    break;
                case ChestState.Unlocking:
                    SetUnlockingState();
                    break;
                case ChestState.Unlocked:
                    unlocked.gameObject.SetActive(true);
                    break;
                case ChestState.Collected:
                    break;
            }
        }

        private void SetLockedState(float timeToUnlock){
            locked.gameObject.SetActive(true);
            
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeToUnlock);
            string formattedTime;

            if (timeSpan.TotalHours >= 1) {
                formattedTime = $"{timeSpan.Hours}h";
            } else if (timeSpan.TotalMinutes >= 1) {
                formattedTime = $"{timeSpan.Minutes}m";
            } else {
                formattedTime = $"{timeSpan.Seconds}s";
            }

            this.timeToUnlock.text = formattedTime;
        }

        private void SetUnlockingState(){
            unlocking.gameObject.SetActive(true);
            StartCoroutine(UpdateTimer());
        }

        private IEnumerator UpdateTimer(){
            while(RemainingTime>0){
                UpdateTimerText(RemainingTime);
                UpdateGemCostText(chestController.CalculateGemCost(RemainingTime));
                yield return new WaitForSeconds(RemainingTime > 3600 ? 60 : 1);
                RemainingTime -= RemainingTime > 3600 ? 60 : 1;
            }
        }

        private void UpdateTimerText(float remainingTime){
            var hours = (int)(remainingTime / 3600);
            var minutes = (int)(remainingTime % 3600) / 60;
            var seconds = (int)(remainingTime % 60);

            if (remainingTime > 3600)
            {
                pendingTimeToUnlock.text = $"{hours}H : {minutes}m";
            }
            else
            {
                pendingTimeToUnlock.text = $"{minutes}m : {seconds}s";
            }
        }

        private void UpdateGemCostText(int gemCost){
            openNowCost.text = $"{gemCost}";
        }
        
    }
}