using Sample;
using UniRx;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ObjectDetection _objectDetection;
    
    private void Start()
    {
        _objectDetection.IsPersonDetected.Subscribe(isPersonDetected =>
        {
            if (isPersonDetected)
            {
                _particleSystem.Play();
            }
            else
            {
                _particleSystem.Stop();
            }
        });
    }
}
