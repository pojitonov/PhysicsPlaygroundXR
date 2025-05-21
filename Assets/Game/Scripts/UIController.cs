using System;
using Cysharp.Threading.Tasks;
using Oculus.Interaction;
using PrimeTween;
using UnityEngine;
using Tween = PrimeTween.Tween;

public class UIController : MonoBehaviour
{
    [SerializeField] private bool showSideBar;
    [SerializeField] private float targetWidth = 800f;
    [SerializeField] private float targetHeight = 600f;
    [SerializeField] private GameObject experimentFrame;
    [SerializeField] private GameObject experimentTitle;
    [SerializeField] private GameObject experimentControl;
    [SerializeField] private GameObject experiment;

    private GameObject _UIPanel;
    private GameObject _leftNavigation;
    private bool isActive;
    private bool menuShown;

    private void Awake()
    {
        FindGameObjectByName("UIPanel", ref _UIPanel);
        FindGameObjectByName("LeftNavSize", ref _leftNavigation);
        _UIPanel.SetActive(false);
        experimentFrame.SetActive(false);
        experimentControl.SetActive(false);
        experimentTitle.SetActive(false);
        experiment.SetActive(false);
        experimentTitle.transform.localScale = new Vector3(0, 0, 0);
    }

    void Start()
    {
        _leftNavigation.SetActive(showSideBar);
        SetSize().Forget();
    }

    public void PushButton()
    {
        if (!menuShown)
        {
            _UIPanel.SetActive(true);
            menuShown = true;
        }
        else
        {
            experimentFrame.SetActive(false);
            experimentTitle.SetActive(false);
            experiment.SetActive(false);
            experimentControl.SetActive(false);
            experimentTitle.transform.localScale = new Vector3(0, 0, 0);
            isActive = false;
        }
    }

    private void FindGameObjectByName(string name, ref GameObject target, Transform root = null)
    {
        Transform found = (root ?? transform).FindChildRecursive(name);
        if (found)
            target = found.gameObject;
    }

    private async UniTaskVoid SetSize()
    {
        await UniTask.Delay(10);

        float scaleX = targetWidth / 1024f;
        float scaleY = targetHeight / 688f;

        _UIPanel.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    public void LoadExperiment(int index)
    {
        if (isActive) return;
        if (index == 1)
        {
            LoadExperiment1().Forget();
            isActive = true;
        }
    }

    private async UniTaskVoid LoadExperiment1()
    {
        experimentFrame.SetActive(true);
        experimentTitle.SetActive(true);

        var Frame = experimentFrame.GetComponent<RoundedBoxProperties>();
        await Sequence.Create()
            .Group(Tween.Custom(0f, 0.6f, duration: 1f, onValueChange: newVal => Frame.Height = newVal))
            .Group(Tween.Custom(0f, 0.6f, duration: 1f, onValueChange: newVal => Frame.Width = newVal));
        await Sequence.Create()
            .Group(Tween.Scale(experimentTitle.transform, endValue: 0.00061f, duration: 0.5f));
        await Tween.Delay(0.5f);
        
        experiment.SetActive(true);
        experimentControl.SetActive(true);
    }
}