/**
 * Created by jameskim on 2020/04/01
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.AddressableAssets.Settings
{
    public class AddressableAssetRegisterRuleSet : ScriptableObject
    {
        [Serializable]
        public struct Rule
        {
            public string PathPrefix;
            public AddressableAssetGroup AssetGroup;
            public bool SimplifyAddress;
        }

        [SerializeField]
        List<Rule> m_Rules;


        public bool TryResolveRule(string path, out Rule found)
        {
            path = path.Replace('\\', '/');

            foreach (var rule in m_Rules)
            {
                if (!path.StartsWith(rule.PathPrefix))
                    continue;
                found = rule;
                return true;
            }

            found = default;
            return false;
        }
    }
}