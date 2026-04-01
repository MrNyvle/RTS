using UnityEngine;

namespace _Scripts.AntiCheat
{
    [System.Serializable]
    public class SecureFloat
    {
        private float _obfuscatedValue;
        private float _key;
        private long _checksum;

        public SecureFloat(float value)
        {
            _key = Random.Range(1f, 1000f);
            SetValue(value);
        }

        public float GetValue()
        {
            float realValue = _obfuscatedValue - _key;

            if (_checksum != CalculateChecksum(realValue))
            {
                AntiCheatManager.Instance.FlagCheat("SecureInt tampering detected");
            }

            return realValue;
        }

        public void SetValue(float value)
        {
            _key = Random.Range(1f, 1000f); // clé change à chaque update
            _obfuscatedValue = value + _key;
            _checksum = CalculateChecksum(value);
        }

        private long CalculateChecksum(float value)
        {
            return Mathf.RoundToInt(value * 1000) ^ 0xDEADBEEF;
        }
    }
}