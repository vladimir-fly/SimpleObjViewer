using UnityEngine;
using UnityEngine.Assertions;

namespace SOV
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] private GameObject _rootAnchor;
        [SerializeField] private GameObject _model;
        private void Start()
        {
            Assert.IsNotNull(_rootAnchor);
        }

        public void ShowMesh(Mesh mesh)
        {
           ClearLoadedMesh();
            
            _model = new GameObject(mesh.name);

            var meshRenderer = _model.AddComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Standard"));

            var filter = _model.AddComponent<MeshFilter>();
            filter.mesh = mesh;
            
            _model.transform.SetParent(_rootAnchor.transform);
        }

        private void ClearLoadedMesh()
        {   
            if (_model != null) Destroy(_model);
        }
        
        public void RotateRight()
        {
            _rootAnchor.transform.Rotate(0,-1,0);
        }

        public void RotateLeft()
        {
            _rootAnchor.transform.Rotate(0,1,0);
        }
    }
}