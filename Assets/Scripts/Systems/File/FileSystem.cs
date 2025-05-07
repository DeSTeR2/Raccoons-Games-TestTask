using System;
using System.IO;
using UnityEngine;

namespace Utils
{
    public static class FileSystem
    {
        /// <summary>
        ///     Saves data to a file
        /// </summary>
        /// <typeparam name="T">Save data type</typeparam>
        /// <param name="fileName">File name</param>
        /// <param name="data">Data to save</param>
        public static void Save<T>(string fileName, T data)
        {
            try
            {
                var path = Path.Combine(Application.persistentDataPath, fileName);
                var json = JsonUtility.ToJson(data, true);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(json);
                    }
                }

                Debug.Log($"Save succesful! Path: {path}\n Data: {json}, {data}");
            }
            catch (Exception e)
            {
                Debug.LogError("Save error!\n" + e.Message);
            }
        }

        /// <summary>
        ///     Load info from a json file
        /// </summary>
        /// <typeparam name="T">Load data type</typeparam>
        /// <param name="fileName">File name</param>
        /// <returns></returns>
        private static T Load<T>(string fileName)
        {
            try
            {
                var path = Path.Combine(Application.persistentDataPath, fileName);
                Debug.Log($"Load path: {path}");

                var json = "";
                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }

                Debug.Log($"Readed JSON: {json}");
                var data = JsonUtility.FromJson<T>(json);

                Debug.Log("Load succesful!");
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError("Load error!\n" + e.Message);
                return default;
            }
        }


        /// <summary>
        ///     Load data and assign it to a class
        /// </summary>
        /// <typeparam name="T">Load data type</typeparam>
        /// <typeparam name="U">Class to assign data to</typeparam>
        /// <param name="fileName">File name</param>
        /// <param name="toAssign">Class to assign</param>
        /// <returns></returns>
        public static bool Load<T>(string fileName, ILoadable toAssign) where T : IData
        {
            var data = Load<T>(fileName);
            toAssign.Assign(data);
            return true;
        }
    }
}