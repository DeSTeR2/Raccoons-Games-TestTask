using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Box
{
    [CreateAssetMenu(fileName = "BoxConfig", menuName = "Game/Box Config")]
    public class BoxConfig : ScriptableObject
    {
        [Header("Default values")] public Vector3 startScale;

        [Header("Merge animation")] public float mergeAnimationDuration;

        public Vector3 targetScale;
        public Ease scaleEase;
        public int loopNumber = 1;
        public LoopType loopType = LoopType.Yoyo;

        [Header("Fly up")] public Range upForce;

        public Range sideForce;

        [Header("Shooting data")] public float delayBetweenSpawn = 0.1f;

        public float shootingForce;
        public Vector3 shootDirection;


        public void ApplyMergeAnimation(Rigidbody rb, EmitParticleSystem particleSystem)
        {
            var go = rb.gameObject;
            go.transform.DOScale(targetScale, mergeAnimationDuration).SetEase(scaleEase).SetLoops(loopNumber, loopType);
            particleSystem.Play(ParticleType.BlockMerge, go.transform.position);

            var moveForce = MoveForce();
            var rotation = RotationForce();

            rb.AddForce(moveForce, ForceMode.Impulse);
            rb.AddTorque(rotation, ForceMode.Impulse);
        }

        public void ApplyScale(GameObject go)
        {
            go.transform.localScale = startScale;
        }

        private Vector3 MoveForce()
        {
            return new Vector3(sideForce.RandomNumber, upForce.RandomNumber, 0);
        }

        private Vector3 RotationForce()
        {
            return new Vector3(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );
        }
    }

    [Serializable]
    public class Range
    {
        public float min;
        public float max;

        public float RandomNumber => Random.Range(min, max);
    }
}