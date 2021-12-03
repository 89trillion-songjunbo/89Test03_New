using System;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;

namespace MVC.Model
{
    public class UILeaderBoardModel
    {
        public UILeaderBoardModel()
        {
            ReadData();
        }

        public Tuple<PersonInfo, int> GetPersonDataWithRank(string uid = "3716954261")
        {
            return new Tuple<PersonInfo, int>(GetPersonData(uid),
                GetRank(uid));
        }

        private PersonInfo GetPersonData(string uid)
        {
            List<PersonInfo> infoLis = null;

            if (LeaderRankInfo != null)
            {
                infoLis = LeaderRankInfo.PersonInfos.Where(a => a.Uid == uid).ToList();
            }

            return infoLis != null && infoLis.Count > 0 ? infoLis[0] : null;
        }

        private int GetRank(string uid)
        {
            var rank = 0;
            if (LeaderRankInfo != null)
            {
                rank = LeaderRankInfo.PersonInfos.FindIndex(item => item.Uid.Equals(uid));
            }

            return rank + 1;
        }

        public LeaderRankInfo LeaderRankInfo { get; private set; }

        private const string jsonPath = "json/ranklist";

        private void ReadData()
        {
            var json = Resources.Load<TextAsset>(jsonPath);
            LeaderRankInfo = ReadData(json.ToString());
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        private static LeaderRankInfo ReadData(string data)
        {
            var root = JSON.Parse(data);

            var leaderRankInfos = new LeaderRankInfo
            {
                CountDown = root["countDown"]
            };

            for (var i = 0; i < root["list"].Count; i++)
            {
                var personInfo = root["list"][i];

                var info = new PersonInfo(
                    personInfo["uid"],
                    personInfo["nickName"],
                    personInfo["trophy"].AsInt
                );

                leaderRankInfos.PersonInfos.Add(info);
            }

            leaderRankInfos.PersonInfos.Sort((a, b) => b.Trophy - a.Trophy);
            leaderRankInfos.SeasonID = root["seasonID"].AsInt;
            return leaderRankInfos;
        }
    }

    public class LeaderRankInfo
    {
        public int CountDown;
        public readonly List<PersonInfo> PersonInfos = new List<PersonInfo>();
        public int SeasonID;
    }

    public class PersonInfo
    {
        public readonly string Uid;
        public readonly string NickName;
        public readonly int Trophy;

        public PersonInfo(string uid, string nickName, int trophy)
        {
            Uid = uid;
            NickName = nickName;
            Trophy = trophy;
        }
    }
}