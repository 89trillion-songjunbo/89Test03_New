using UnityEngine;
using UnityEngine.UI;

namespace MVC.View
{
    public class TipsPanelView : MonoBehaviour
    {
        [SerializeField] private Text mUiTe;

        public void Init(string contentStr)
        {
            gameObject.SetActive(true);
            mUiTe.text = contentStr;
            isOpen = true;
        }

        private float timer;

        private bool isOpen;

        private void Update()
        {
            if (!isOpen)
            {
                return;
            }

            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                isOpen = false;
                gameObject.SetActive(false);
            }
        }
    }
}