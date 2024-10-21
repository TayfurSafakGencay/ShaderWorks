using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Shaders.Fire
{
    public class FireParticle : MonoBehaviour, IShowInOrder
    {
        public Light fireLight;
        public ParticleSystem FireParticles;
        public float minIntensity = 0.5f;
        public float maxIntensity = 2f;
        public float minRange = 5f;
        public float maxRange = 10f;
        public float flickerDuration = 1f;
        
        private bool _isStopping;
        private void Start()
        {
            fireLight.intensity = 0;
            
            StartFlickerEffect();
        }

        private void StartFlickerEffect()
        {
            if (_isStopping) return;

            fireLight.DOIntensity(maxIntensity, flickerDuration).SetEase(Ease.InOutSine);
            DOTween.To(() => fireLight.range, x => fireLight.range = x, maxRange, flickerDuration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    if (_isStopping) return;
                    
                    fireLight.DOIntensity(minIntensity, flickerDuration).SetEase(Ease.InOutSine);
                    DOTween.To(() => fireLight.range, x => fireLight.range = x, minRange, flickerDuration)
                        .SetEase(Ease.InOutSine)
                        .OnComplete(StartFlickerEffect);
                });
        }

        public void Stop(float stoppingTime)
        {
            StartCoroutine(StopFire(stoppingTime));
            
            fireLight.DOIntensity(0, stoppingTime).SetEase(Ease.InOutSine);
        }
        
        private IEnumerator StopFire(float stoppingTime)
        {
            _isStopping = true;
            
            FireParticles.Stop();
            
            yield return new WaitForSeconds(stoppingTime);
            
            gameObject.SetActive(false);
        }
    }
}
