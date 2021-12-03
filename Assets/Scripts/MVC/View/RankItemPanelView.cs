using MVC.Controller;
using MVC.Model;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.View
{
    public class RankItemPanelView : MonoBehaviour
    {
        [SerializeField] private Image mUiRankImg;
        [SerializeField] private Text mUiRankTe;
        [SerializeField] private Text mUiRankName;
        [SerializeField] private Image mUiTrophyImg;
        [SerializeField] private Text mUiTrophyNum;

        public void SetData(PersonInfo personData, int rankIndex)
        {
            mUiRankTe.gameObject.SetActive(rankIndex > 2);
            mUiRankImg.gameObject.SetActive(rankIndex <= 2);

            mUiRankImg.sprite = rankIndex <= 2
                ? Tools.ResourceManager.Instance.GetSprite(Const.FrontRankPath + rankIndex)
                : Tools.ResourceManager.Instance.GetSprite(Const.RankNormalPath);

            mUiRankTe.text = rankIndex.ToString();

            var index = personData.Trophy / 1000 + 1;
            mUiTrophyImg.sprite = Tools.ResourceManager.Instance.GetSprite(Const.SegmentImgPath, index);
            mUiTrophyImg.SetNativeSize();

            mUiTrophyNum.text = personData.Trophy.ToString();
            mUiRankName.text = personData.NickName;
        }
    }
}