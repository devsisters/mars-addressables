/**
 * Created by jameskim on 2020-09-01
 */

using UnityEngine;

namespace UnityEditor.AddressableAssets.Settings
{
    public abstract class AddressablesHook : ScriptableObject
    {
        public abstract bool BeforeSetEntryOnInspectorGUI(string assetPath, out AddressableAssetGroup assetGroup, out string address);
    }
}