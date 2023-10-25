using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] protected UIDocument Document;
    [SerializeField] protected StyleSheet StyleSheet;
    
    protected VisualElement Root;

    private void Awake()
    {
        PreGenerate();
    }

    private void Start()
    {
        Generate();
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        Regenerate();
    }

    private void PreGenerate()
    {
        if (Document == null)
            Document = GetComponent<UIDocument>();

        Root = Document.rootVisualElement;
        Root.Clear();
        Root.styleSheets.Add(StyleSheet);
    }

    /// <summary>
    /// Regenerate the entire document
    /// </summary>
    [ContextMenu(nameof(Regenerate))]
    public void Regenerate()
    {
        PreGenerate();
        Generate();
    }

    protected virtual void Generate() => StartCoroutine(C_Generate());
    protected virtual IEnumerator C_Generate()
    {
        yield return null;
    }

    /// <summary>
    /// Quickly create a <see cref="VisualElement"/> with class names
    /// </summary>
    /// <param name="classNames">Set class names</param>
    protected VisualElement Create(params string[] classNames) => Create<VisualElement>(classNames);
    /// <summary>
    /// Quickly create any type of <see cref="VisualElement"/> with class names
    /// </summary>
    /// <typeparam name="T">A child class of <see cref="VisualElement"/></typeparam>
    /// <param name="classNames">Set class names</param>
    protected T Create<T>(params string[] classNames) where T : VisualElement, new()
    {
        T element = new();
        foreach (string className in classNames)
        {
            element.AddToClassList(className);
        }
        return element;
    }
}
