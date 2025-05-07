using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Systems.File
{
    public class SaveLoadClassSystem : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> saveScriptableObjects;
        private List<IFile> _filesList;

        private void OnDestroy()
        {
            Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        public void FindAndLoad()
        {
            FindObjects();
            Load();
        }

        private void FindObjects()
        {
            _filesList = new List<IFile>();
            _filesList.AddRange(saveScriptableObjects);
        }

        public void Save()
        {
            foreach (var file in _filesList) file.Save();
        }

        private void Load()
        {
            foreach (var file in _filesList)
            {
                var result = file.Load();
                if (result == false) Debug.Log("Loading is failed");
            }
        }
    }
}