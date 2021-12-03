using UnityEngine;

namespace Tools
{
    public class ResourceManager
    {
        private static ResourceManager instance;
        public static ResourceManager Instance => instance ?? (instance = new ResourceManager());

        public Sprite GetSprite(string mPath, int index = -1)
        {
            string path = mPath;
            if (index != -1)
            {
                path = mPath + index;
            }

            Sprite sp = Resources.Load<Sprite>(path);
            return sp;
        }
    }
}