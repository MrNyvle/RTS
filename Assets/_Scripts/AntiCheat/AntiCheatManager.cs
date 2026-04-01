using System;
using System.Collections.Generic;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.AntiCheat
{
    public class AntiCheatManager : Singleton<AntiCheatManager>
    {
        private int cheatFlags = 0;

        private List<ICheckable> checkables = new List<ICheckable>();

        private void Update()
        {
            foreach (var checkable in checkables)
            {
                checkable.PerformCheck();
            }

            if (GetFlagCount() >= 1)
            {
                UiManager.Instance.ShowCheatScreen();
            }
            
        }

        public void Register(ICheckable obj)
        {
            if (!checkables.Contains(obj))
                checkables.Add(obj);
        }

        public void FlagCheat(string reason)
        {
            cheatFlags++;
            Debug.LogWarning($"Cheat detected: {reason} | Total flags: {cheatFlags}");
        }

        public int GetFlagCount()
        {
            return cheatFlags;
        }
    }
}