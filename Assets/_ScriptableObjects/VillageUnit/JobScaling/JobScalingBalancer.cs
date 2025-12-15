using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace _ScriptableObjects.VillageUnit.JobScaling
{
    [Serializable]
    public class JobTime
    {
        public EJobType eJobType;
        [Tooltip("This is how much time needs to pass for a point to be given expressed in seconds")]
        public float taskMinDuration;
    }
    
    
    [CreateAssetMenu(fileName = "VillageUnitCombatStats", menuName = "Scriptable Objects/JobScalingBalancer")]
    public class JobScalingBalancer : ScriptableObject
    {
        public List<JobTime> jobsTime = new List<JobTime>();

        public Dictionary<EJobType, float> GetTimes()
        {
            return jobsTime.ToDictionary(jt => jt.eJobType, jt => jt.taskMinDuration);
        }

        public float GetJobTime(EJobType eJobType)
        {
            GetTimes().TryGetValue(eJobType, out var result);
            return result;
        }
        
    }
}