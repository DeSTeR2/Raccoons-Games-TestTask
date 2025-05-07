using System.Collections.Generic;
using UnityEngine;

namespace Game.Box
{
    [CreateAssetMenu(fileName = "Box data collection", menuName = "Box/DataCollection")]
    public class BoxDataCollection : ScriptableObject
    {
        public List<BoxData> Datas;

        public BoxData GetNextData(BoxData data)
        {
            var number = Datas.FindIndex(dat => dat == data);
            number++;
            if (Datas.Count == number) AppendNewData();

            return Datas[number];
        }

        public BoxData GetDataWithPercent(int rng)
        {
            var sumPercent = 0;
            for (var i = 0; i < Datas.Count; i++)
            {
                var data = Datas[i];
                sumPercent += data.spawnPercent;

                if (sumPercent >= rng)
                    return data;
            }

            return Datas[^1];
        }

        private void AppendNewData()
        {
            var data = Datas[^1];
            var newData = new BoxData(data);
        }
    }
}