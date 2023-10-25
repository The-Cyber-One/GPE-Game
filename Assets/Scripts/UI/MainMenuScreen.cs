using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : UIScreen
{
    [Header("Menu References")]
    [SerializeField] private Sprite titleSprite;
    [SerializeField] private Sprite leftButtonSprite, centerButtonSprite, rightButtonSprite;
    [SerializeField] private Sprite leftBottomButtonSprite, centerBottomButtonSprite, rightBottomButtonSprite;

    protected override void Generate()
    {
        var veTitleContainer = Create("title-container");
        var imgTitle = Create<Image>("title");
        imgTitle.sprite = titleSprite;
        veTitleContainer.Add(imgTitle);
        Root.Add(veTitleContainer);

        Root.Add(GenerateButton("Play"));
        Root.Add(GenerateButton("How To Play"));
        Root.Add(GenerateButton("Highscore"));
    }

    private VisualElement GenerateButton(string label)
    {
        var menuButton = Create("menu-button");
        var buttonBackground = Create();
        var top = Create("menu-button-section");
        top.Add(new Image() { sprite = leftButtonSprite });
        top.Add(new Image() { sprite = centerButtonSprite });
        top.Add(new Image() { sprite = rightButtonSprite });
        top.Add(new Label() { text = label });
        buttonBackground.Add(top);

        var bottom = Create("menu-button-section");
        bottom.Add(new Image() { sprite = leftBottomButtonSprite });
        bottom.Add(new Image() { sprite = centerBottomButtonSprite });
        bottom.Add(new Image() { sprite = rightBottomButtonSprite });
        buttonBackground.Add(bottom);
        menuButton.Add(buttonBackground);

        menuButton.Add(Create<Button>());

        return menuButton;
    }
}
