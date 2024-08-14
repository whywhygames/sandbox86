using CoverShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterRewardGetter : MonoBehaviour
{
    [SerializeField] private List<GunDogMovement> _gunDogMovementList = new List<GunDogMovement>();
    [SerializeField] private PlayerMoney _moneyGetter; 
    [SerializeField] private MineSpawner _mineGetter; 
    [SerializeField] private GrenadeInventoryCounter _grenadeGetter; 
    [SerializeField] private Gun _revolverGetter; 
    [SerializeField] private Gun _azotBlasterGetter; 
    [SerializeField] private Gun _sniperGetter;

    public void TakeReward(TaskReward reward)
    {
        GiveReward(reward.RewardType, reward.Count);
    }

    public void TakeReward(List<TaskReward> rewards)
    {
        foreach (TaskReward reward in rewards)
        {
            GiveReward(reward.RewardType, reward.Count);
        }
    }

    public void GiveReward(RewardType rewardType, int count)
    {
        UnityAction<int> GetReward = _moneyGetter.AddMonye;

        switch (rewardType)
        {
            case RewardType.Money:
                GetReward = _moneyGetter.AddMonye;
                break;

            case RewardType.RevolverBullet:
                GetReward = _revolverGetter.FillBulletInventory;
                break;

            case RewardType.AzotBlasterBullet:
                GetReward = _azotBlasterGetter.FillBulletInventory;
                break;

            case RewardType.SniperBullet:
                GetReward = _sniperGetter.FillBulletInventory;
                break;

            case RewardType.Grenade:
                GetReward = _grenadeGetter.AddMine;
                break;

            case RewardType.Mine:
                GetReward = _mineGetter.AddMine;
                break;

            case RewardType.GunDog:
                GetGunDog();
                GetReward = null;
                break;
        }

        if (GetReward != null)
            StartCoroutine(AddReward(GetReward, count));
    }

    public void GetMoney(int count)
    {
        StartCoroutine(AddReward(_moneyGetter.AddMonye, count));
    }

    private void GetGunDog()
    {
        if (_gunDogMovementList.Count > 0)
        {
            _gunDogMovementList[_gunDogMovementList.Count - 1].gameObject.SetActive(true);
            _gunDogMovementList.RemoveAt(_gunDogMovementList.Count - 1);
        }
    }

    private IEnumerator AddReward(UnityAction<int> GetReward, int count)
    {
        while (count > 0)
        {
            GetReward(1);
            count--;
            yield return new WaitForSeconds(0.005f);
        }
    }
}
