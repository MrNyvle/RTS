using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Unit
{
    public class UnitVillagerJob
    {
        EJobType _currentJob;
        UnitStats _stats;

        Dictionary<EJobType, float> _jobsTime = new ();
        
        private float _startJobTime;

        public UnitVillagerJob(UnitStats stats)
        {
            _stats = stats;
        }
        
        public void StartJob(EJobType eJobType)
        {
            _startJobTime = Time.time;
            _currentJob = eJobType;
        }

        public void EndJob()
        {
            float taskDuration = Time.time - _startJobTime;
            _jobsTime[_currentJob] = _jobsTime.GetValueOrDefault(_currentJob) + taskDuration;

            int points = (int)(_jobsTime[_currentJob] / GameManager.Instance.jobScalingBalancers.GetJobTime(_currentJob));
            
            _stats.UpdateJobPoints(_currentJob, Mathf.Clamp(points, 0, 10));
        }
        
    }
}