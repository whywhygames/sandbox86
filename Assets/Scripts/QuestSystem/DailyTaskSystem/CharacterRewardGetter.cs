using CoverShooter;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterRewardGetter : MonoBehaviour
{
    [SerializeField] private PlayerMoney _moneyGetter; 
    [SerializeField] private MineSpawner _mineGetter; 
    [SerializeField] private GrenadeInventoryCounter _grenadeGetter; 
    [SerializeField] private Gun _revolverGetter; 
    [SerializeField] private Gun _azotBlasterGetter; 
    [SerializeField] private Gun _sniperGetter;

    public void TakeReward(TaskReward reward)
    {
        
        GiveReward(reward);
    }

    private void GiveReward(TaskReward reward)
    {
        UnityAction<int> GetReward = _moneyGetter.AddMonye;

        switch (reward.RewardType)
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
        }

        StartCoroutine(AddReward(GetReward, reward.Count));
    }

    public void GetMoney(int count)
    {
        StartCoroutine(AddReward(_moneyGetter.AddMonye, count));
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
