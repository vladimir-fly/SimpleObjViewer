using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace SOV
{
    public class MenuScreen : MonoBehaviour
    {
        [SerializeField] private ViewManager _viewManager;
        [SerializeField] private Button _load;
        [SerializeField] private CustomButton _rotateRight;
        [SerializeField] private CustomButton _rotateLeft;
        [SerializeField] private Dropdown _models;
        [SerializeField] private Text _messageView;
        [SerializeField] private Button _quit;

        private void Start()
        {
            CheckBindings();
            
            var files = ObjLoader.GetFiles();
            if (files == null || !files.Any())
            {
                _messageView.text = "No models was found!";
                return;
            }

            _models.options = files.Select(f => new Dropdown.OptionData {text = f}).ToList();
            _load.onClick.AddListener(() =>
                {
                    try
                    {
                        var mesh = ObjLoader.LoadMeshFromFile(_models.options[_models.value].text);
                        _viewManager.ShowMesh(mesh);
                    }
                    catch (Exception e)
                    {
                        _messageView.text = "Mesh load error! Details in log.";
                        Debug.LogError(e.Message);
                    }
                }
            );
            
            _quit.onClick.AddListener(Application.Quit);
        }

        private void CheckBindings()
        {
            Assert.IsNotNull(_viewManager);
            Assert.IsNotNull(_load);
            Assert.IsNotNull(_rotateRight);
            Assert.IsNotNull(_rotateLeft);
            Assert.IsNotNull(_models);
            Assert.IsNotNull(_messageView);
            Assert.IsNotNull(_quit);
        }

        private void Update()
        {
            if (_rotateRight.IsPressed)
                _viewManager.RotateRight();
            
            if (_rotateLeft.IsPressed) 
                _viewManager.RotateLeft();
        }
    }
}