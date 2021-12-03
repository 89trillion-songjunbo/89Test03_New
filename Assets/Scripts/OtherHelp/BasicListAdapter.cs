using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using MVC.Controller;
using MVC.Model;

// You should modify the namespace to your own or - if you're sure there won't ever be conflicts - remove it altogether
namespace Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists
{
    // There are 2 important callbacks you need to implement, apart from Start(): CreateViewsHolder() and UpdateViewsHolder()
    // See explanations below
    public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
    {
        private SimpleDataHelper<PersonInfo> Data { get; set; }

        private UILeaderBoardModel _uiLeaderBoardModel;

        #region OSA implementation

        protected override void Awake()
        {
            Data = new SimpleDataHelper<PersonInfo>(this);

            base.Awake();
        }

        public void Init(UILeaderBoardModel model)
        {
            _uiLeaderBoardModel = model;
        }

        protected override void Start()
        {
            base.Start();
            RetrieveDataAndUpdate(_uiLeaderBoardModel.LeaderRankInfo.PersonInfos.Count);
        }


        protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
        {
            var instance = new MyListItemViewsHolder();

            instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

            return instance;
        }

        public Action<string> EventCreateTips;

        protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)
        {
            // In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
            // index of item that should be represented by this views holder. You'll use this index
            // to retrieve the model from your data set
            /*			*/

            PersonInfo model = Data[newOrRecycled.ItemIndex];

            newOrRecycled.mUiSegmentTex.gameObject.SetActive(newOrRecycled.ItemIndex > 2);
            newOrRecycled.mUiFrontImg.gameObject.SetActive(newOrRecycled.ItemIndex <= 2);

            if (newOrRecycled.ItemIndex <= 2)
            {
                newOrRecycled.mUiFrontImg.sprite =
                    Tools.ResourceManager.Instance.GetSprite(Const.FrontRankPath, newOrRecycled.ItemIndex + 1);
                newOrRecycled.mUiFrontImg.SetNativeSize();
                newOrRecycled.mUiBgImg.sprite =
                    Tools.ResourceManager.Instance.GetSprite(Const.RankBgImgPath, newOrRecycled.ItemIndex + 1);
            }
            else
            {
                newOrRecycled.mUiSegmentTex.text = (newOrRecycled.ItemIndex + 1).ToString();
                newOrRecycled.mUiBgImg.sprite = Tools.ResourceManager.Instance.GetSprite(Const.RankNormalPath);
            }

            int index = model.Trophy / 1000 + 1;
            newOrRecycled.mUiTropyhImg.sprite = Tools.ResourceManager.Instance.GetSprite(Const.SegmentImgPath, index);
            newOrRecycled.mUiTropyhImg.SetNativeSize();
            newOrRecycled.mUiTeamName.text = model.NickName;
            newOrRecycled.mUiTropyhNum.text = model.Trophy.ToString();
            newOrRecycled.contentTe =
                $"User: {model.NickName}  Rank: {newOrRecycled.ItemIndex + 1}";

            newOrRecycled.mUiBox.onClick.AddListener(() =>
            {
                if (EventCreateTips != null)
                    EventCreateTips(newOrRecycled.contentTe);
            });
        }

        #endregion

        // These are common data manipulation methods
        // The list containing the models is managed by you. The adapter only manages the items' sizes and the count
        // The adapter needs to be notified of any change that occurs in the data list. Methods for each
        // case are provided: Refresh, ResetItems, InsertItems, RemoveItems

        #region data manipulation

        public void AddItemsAt(int index, IList<PersonInfo> items)
        {
            Data.InsertItems(index, items);
        }

        public void RemoveItemsFrom(int index, int count)
        {
            Data.RemoveItems(index, count);
        }

        public void SetItems(IList<PersonInfo> items)
        {
            Data.ResetItems(items);
        }

        #endregion

        // Here, we're requesting <count> items from the data source
        void RetrieveDataAndUpdate(int count)
        {
            StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate(count));
        }

        // Retrieving <count> models from the data source and calling OnDataRetrieved after.
        // In a real case scenario, you'd query your server, your database or whatever is your data source and call OnDataRetrieved after
        IEnumerator FetchMoreItemsFromDataSourceAndUpdate(int count)
        {
            // Simulating data retrieving delay
            yield return new WaitForSeconds(.5f);

            var newItems = new PersonInfo[count];

            for (int i = 0; i < count; ++i)
            {
                newItems[i] = _uiLeaderBoardModel.LeaderRankInfo.PersonInfos[i];
            }

            OnDataRetrieved(newItems);
        }

        void OnDataRetrieved(PersonInfo[] newItems)
        {
            Data.InsertItemsAtEnd(newItems);
        }
    }

    public class MyListItemViewsHolder : BaseItemViewsHolder
    {
        public Image mUiBgImg;
        public Image mUiFrontImg;
        public Text mUiSegmentTex;
        public Text mUiTeamName;
        public Image mUiTropyhImg;
        public Text mUiTropyhNum;
        public Button mUiBox;

        public string contentTe;

        // Retrieving the views from the item's root GameObject
        public override void CollectViews()
        {
            base.CollectViews();

            root.GetComponentAtPath("segmentTex", out mUiSegmentTex);
            root.GetComponentAtPath("tropyhImg", out mUiTropyhImg);
            root.GetComponentAtPath("name", out mUiTeamName);
            root.GetComponentAtPath("trophyNum", out mUiTropyhNum);
            root.GetComponentAtPath("FrontImg", out mUiFrontImg);
            root.GetComponentAtPath("bg", out mUiBgImg);
            root.GetComponentAtPath("Box", out mUiBox);
        }
    }
}