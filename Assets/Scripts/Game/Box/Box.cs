using System.Collections.Generic;
using Game.Box.BoxStates;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Box
{
    public class Box : MonoBehaviour
    {
        private static readonly int BoxColor = Shader.PropertyToID("_BoxColor");
        [SerializeField] private Transform textParent;
        [SerializeField] private MeshRenderer renderer;

        [field: Space] [SerializeField] private Rigidbody rb;

        [SerializeField] private Collider collider;
        private BoxController _boxController;
        private BoxConfig _config;

        private Material blockMaterial;
        private List<TextMeshPro> textes;


        public int Id { get; private set; }
        public BoxData CurrentData { get; private set; }
        public BoxStateMachine StateMachine { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            var otherBox = collision.gameObject.GetComponent<Box>();
            if (otherBox == null) return;

            if (Id < otherBox.Id)
            {
                var impulse = collision.impulse.magnitude;
                _boxController.BoxCollide(this, otherBox, impulse);
            }
        }

        [Inject]
        public void Construct(BoxConfig config)
        {
            _config = config;
        }

        public void Init(BoxController boxController, Transform boxPositionsStartPosition)
        {
            StateMachine = new BoxStateMachine(this);
            StateMachine.Enter<ShootingState>();

            transform.position = boxPositionsStartPosition.position;
            _config.ApplyScale(gameObject);

            _boxController = boxController;
            blockMaterial = new Material(renderer.material);
            renderer.material = blockMaterial;

            textes = new List<TextMeshPro>();
            for (var i = 0; i < textParent.childCount; i++)
                textes.Add(textParent.GetChild(i).gameObject.GetComponent<TextMeshPro>());

            Id = GetInstanceID();
        }

        public void SetBoxVisual(BoxData boxData)
        {
            SetMaterialColor(boxData.color);
            SetText(boxData.number);

            CurrentData = boxData;
        }

        public void Physics(bool active)
        {
            collider.enabled = active;
            rb.isKinematic = !active;
        }

        public void MergeAnimation()
        {
            _config.ApplyMergeAnimation(rb, EmitParticleSystem.Instance);
        }

        private void SetText(int boxDataNumber)
        {
            foreach (var texte in textes) texte.text = boxDataNumber.ToString();
        }

        private void SetMaterialColor(Color boxDataColor)
        {
            blockMaterial.SetColor(BoxColor, boxDataColor);
        }

        public void ApplyShootForce(Vector3 shootVector)
        {
            StateMachine.Enter<FlyState>();
            rb.AddForce(shootVector);
        }
    }
}