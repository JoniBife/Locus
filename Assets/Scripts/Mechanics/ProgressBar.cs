using Assets.Scripts.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mechanics
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private float _startRadius;
        [SerializeField]
        [Range(0.01f, 10f)]
        private float _fillSpeed;

        public Transform CharacterToDetect;
        public Image ProgressCircle;

        public GameEvent OnProgressBarFilled;

        private float _fillProgress = 0f;

        private void Start()
        {
            ProgressCircle.fillAmount = _fillProgress;
        }

        // Update is called once per frame
        void Update()
        {
            // Is character within progress radius
            if (_fillProgress < 1f)
            {
                float distance = (CharacterToDetect.position - transform.position).magnitude;

                if (distance < _startRadius)
                {
                    _fillProgress = Mathf.Clamp(_fillProgress + _fillSpeed * Time.deltaTime, 0f, 1f);

                    if (_fillProgress == 1f)
                    {

                        Color successColor = Color.white;
                        ColorUtility.TryParseHtmlString(GlobalParameters.SuccessColor, out successColor);
                        ProgressCircle.color = successColor;
                        OnProgressBarFilled.Raise();
                    }

                    ProgressCircle.fillAmount = _fillProgress;
                }
            }
        }

        public void Reset()
        {
            ProgressCircle.fillAmount = 0f;
            _fillProgress = 0f;
            Color successColor = Color.white;
            ColorUtility.TryParseHtmlString(GlobalParameters.DefaultColor, out successColor);
            ProgressCircle.color = successColor;
        }
    }
}