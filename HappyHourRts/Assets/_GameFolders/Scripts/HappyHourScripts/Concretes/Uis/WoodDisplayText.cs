using System;
using HappyHourRts.Helpers;
using HappyHourRts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace HappyHourRts.Uis
{
    public class WoodDisplayText : MonoBehaviour
    {
        [SerializeField] PlayerWallet _playerWallet;
        [SerializeField] TMP_Text _woodText;

        void OnValidate()
        {
            this.GetReference(ref _woodText);
        }

        void OnEnable()
        {
            _playerWallet.OnWoodIncreased += HandleOnWoodIncreased;
        }

        void OnDisable()
        {
            _playerWallet.OnWoodIncreased -= HandleOnWoodIncreased;
        }

        void HandleOnWoodIncreased(int value)
        {
            _woodText.SetText(value.ToString());
        }
    }    
}

