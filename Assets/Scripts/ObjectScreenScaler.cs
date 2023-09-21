using System;
using UnityEngine;

public class ObjectScreenScaler : MonoBehaviour
{
    public static event Action OnScreenResolutionChanged;

    [SerializeField] private Vector2 min, max;
    [SerializeField] private Camera scaleCamera;
    [SerializeField] private bool setScale = true, setPosition = true;

    private static int s_screenWidth, s_screenHeight;

    private void OnValidate()
    {
        min.x = Mathf.Clamp(min.x, 0, max.x);
        min.y = Mathf.Clamp(min.y, 0, max.y);
        max.x = Mathf.Clamp(max.x, min.x, 1);
        max.y = Mathf.Clamp(max.y, min.y, 1);

        if (scaleCamera == null)
            scaleCamera = Camera.main;
        ScaleObjectToScreen();
    }

    private void OnEnable()
    {
        OnScreenResolutionChanged += ScaleObjectToScreen;
        ScaleObjectToScreen();
    }

    private void Update()
    {
        if (s_screenWidth == Screen.width && s_screenHeight == Screen.height)
            return;

        s_screenWidth = Screen.width;
        s_screenHeight = Screen.height;
        OnScreenResolutionChanged?.Invoke();
    }

    private void OnDisable()
    {
        OnScreenResolutionChanged -= ScaleObjectToScreen;
    }

    [ContextMenu(nameof(ScaleObjectToScreen))]
    private void ScaleObjectToScreen()
    {
        if (scaleCamera == null)
            return;

        Vector2 worldMin = scaleCamera.ViewportToWorldPoint(min, Camera.MonoOrStereoscopicEye.Mono);
        Vector2 worldMax = scaleCamera.ViewportToWorldPoint(max, Camera.MonoOrStereoscopicEye.Mono);

        if (setScale)
        {
            transform.localScale = worldMax - worldMin;
        }

        if (setPosition)
        {
            transform.position = worldMin + (worldMax - worldMin) / 2;
        }
    }
}
