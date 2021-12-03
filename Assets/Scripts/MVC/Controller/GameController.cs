using System.Collections;
using MVC.Model;
using MVC.View;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists;

namespace MVC.Controller
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private RankItemPanelView mUiRankItemPanelView;

        private UILeaderBoardModel uiLeaderBoardModel;

        private UILeaderBoardModel UILeaderBoardModel =>
            uiLeaderBoardModel ?? (uiLeaderBoardModel = new UILeaderBoardModel());

        [SerializeField] private Text mUiCountDownTe;
        [SerializeField] private GameObject mUiMainObj;
        [SerializeField] private Button mUiStartBtn;
        [SerializeField] private Transform mUiTipsTran;
        [SerializeField] private BasicListAdapter mUiListAda;

        private void Start()
        {
            mUiStartBtn.gameObject.SetActive(true);
            mUiStartBtn.onClick.AddListener(() =>
            {
                mUiMainObj.SetActive(true);
                StartCoroutine(CountDownFunc(UILeaderBoardModel.LeaderRankInfo.CountDown));
                mUiStartBtn.gameObject.SetActive(false);
            });

            SetData();
            seasonId = UILeaderBoardModel.LeaderRankInfo.SeasonID;
            mUiListAda.EventCreateTips += ShowToast;
            mUiListAda.Init(UILeaderBoardModel);
        }

        private int seasonId;

        private void SetData()
        {
            mUiRankItemPanelView.SetData(UILeaderBoardModel.GetPersonDataWithRank().Item1,
                UILeaderBoardModel.GetPersonDataWithRank().Item2);
        }


        private IEnumerator CountDownFunc(long endTime)
        {
            while (endTime > 0)
            {
                yield return new WaitForSeconds(1f);
                SetData(endTime);
                endTime--;
            }
        }

        private void SetData(long timer)
        {
            var day = Mathf.FloorToInt(timer / (3600 * 24) % 30);
            var hour = Mathf.FloorToInt(timer / 3600 % 24);
            var minute = Mathf.FloorToInt(timer / 60 % 60);
            var second = Mathf.FloorToInt(timer % 60);
            mUiCountDownTe.text = $"Season {seasonId} Ranking \n Ends: {day}d {hour}h {minute}m {second}s";
        }

        public TipsPanelView tipPanelView;

        // ReSharper disable Unity.PerformanceAnalysis
        private void ShowToast(string content)
        {
            if (tipPanelView.Equals(null) && tipPanelView.gameObject.activeSelf)
            {
                return;
            }

            tipPanelView.Init(content);
        }
    }

    /// <summary>
    /// 常量是文件路径及名字 下划线是资源中的名字 不属于脚本命名！！
    /// </summary>
    public static class Const
    {
        public const string FrontRankPath = "UI/rank_";
        public const string SegmentImgPath = "UI/Segment/arenaBadge_";
        public const string RankBgImgPath = "UI/rank list_";
        public const string RankNormalPath = "UI/rank list_normal";
        public const string TipsPath = "Prefabs/Toast";
    }
}