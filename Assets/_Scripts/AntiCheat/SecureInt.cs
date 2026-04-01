using UnityEngine;

namespace _Scripts.AntiCheat
{
    [System.Serializable]
    public class SecureInt
    {
        private int _obfuscatedValue;
        private int _key;
        private long _checksum;

        public SecureInt(int value)
        {
            _key = GenerateKey();
            SetValue(value);
        }

        public int GetValue()
        {
            int realValue = _obfuscatedValue ^ _key;

            if (_checksum != CalculateChecksum(realValue))
            {
                AntiCheatManager.Instance.FlagCheat("SecureInt tampering detected");
            }

            return realValue;
        }

        public void SetValue(int value)
        {
            _key = GenerateKey();
            _obfuscatedValue = value ^ _key;
            _checksum = CalculateChecksum(value);
        }

        private int GenerateKey()
        {
            return Random.Range(int.MinValue, int.MaxValue);
        }

        private long CalculateChecksum(int value)
        {
            return value ^ 0xDEADBEEF;
        }
    }
}