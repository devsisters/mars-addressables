using UnityEditor;

namespace UnityEngine.ResourceManagement.Util
{
    /// <summary>
    /// Creates a singleton.
    /// </summary>
    /// <typeparam name="T">The singleton type.</typeparam>
    [ExecuteInEditMode]
    public abstract class ComponentSingleton<T> : MonoBehaviour where T : ComponentSingleton<T>
    {
        static T s_Instance;

        /// <summary>
        /// Indicates whether or not there is an existing instance of the singleton.
        /// </summary>
        public static bool Exists => s_Instance != null;

        /// <summary>
        /// Stores the instance of the singleton.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType<T>() ?? CreateNewSingleton();
                }
                return s_Instance;
            }
        }

        /// <summary>
        /// Retrieves the name of the object.
        /// </summary>
        /// <returns>Returns the name of the object.</returns>
        protected virtual string GetGameObjectName() => typeof(T).Name;

        static T CreateNewSingleton()
        {
            var go = new GameObject();

            if (Application.isPlaying)
            {
                DontDestroyOnLoad(go);
                go.hideFlags = HideFlags.DontSave;
            }
            else
            {
                go.hideFlags = HideFlags.HideAndDontSave;
            }
            var instance = go.AddComponent<T>();
            go.name = instance.GetGameObjectName();
            return instance;
        }

        private void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
            s_Instance = this as T;
        }

        /// <summary>
        /// Destroys the singleton.
        /// </summary>
        public static void DestroySingleton()
        {
            if (Exists)
            {
                DestroyImmediate(Instance.gameObject);
                s_Instance = null;
            }
        }

#if UNITY_EDITOR
        void OnEnable()
        {
            EditorApplication.playModeStateChanged += PlayModeChanged;
        }

        void OnDisable()
        {
            EditorApplication.playModeStateChanged -= PlayModeChanged;
        }

        void PlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                if (Exists)
                {
                    DestroyImmediate(Instance.gameObject);
                    s_Instance = null;
                }
            }
        }

#endif
    }
}
