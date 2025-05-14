using UnityEditor;
using UnityEngine;

namespace GondrLib.ObjectPool.Editor
{
    public class PollingItemSO : EditorWindow
    {
        public string poolingName;
        public GameObject Prefab;
        public int InitCount;
    }
}