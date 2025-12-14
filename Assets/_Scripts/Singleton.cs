using UnityEngine;

namespace _Scripts
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _shuttingDown = false;
        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                if (_shuttingDown) return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        // Try to find an existing instance in the scene
                        _instance = (T)FindFirstObjectByType(typeof(T));

                        // Auto-create if not found
                        if (_instance == null)
                        {
                            var singletonObject = new GameObject(typeof(T).Name);
                            _instance = singletonObject.AddComponent<T>();
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        protected virtual void OnDestroy()
        {
            _shuttingDown = true;
        }
    }
}