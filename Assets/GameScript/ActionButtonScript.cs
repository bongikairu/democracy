using UnityEngine;
using System.Collections;

public class ActionButtonScript : MonoBehaviour
{

    private static bool loaded = false;

    private static Texture TextureActionCampaign;
    private static Texture TextureActionCommand;
    private static Texture TextureActionCorrupt;

    public enum BtnType
    {
        None,
        Campaign,
        Command,
        Corrupt
    };

    public BtnType Type;
    public string abbrev;

    private static void Load()
    {
        if (loaded) return;

        TextureActionCampaign = Resources.Load<Texture>("action_campaign");
        TextureActionCommand = Resources.Load<Texture>("action_command");
        TextureActionCorrupt = Resources.Load<Texture>("action_corrupt");

    }

    // Use this for initialization
    void Start()
    {
        Load();

        UITexture uit = transform.FindChild("Back").GetComponent<UITexture>();
        uit.gameObject.AddComponent<ButtonPassthrough>();

    }

    // Update is called once per frame
    void Update()
    {
        UITexture uit = transform.FindChild("BaseImg").GetComponent<UITexture>();
        switch (Type)
        {
            case BtnType.Campaign:
                uit.mainTexture = TextureActionCampaign;
                break;
            case BtnType.Command:
                uit.mainTexture = TextureActionCommand;
                break;
            case BtnType.Corrupt:
                uit.mainTexture = TextureActionCorrupt;
                break;
        }
        if (Type != BtnType.None) uit.enabled = true;
        else uit.enabled = false;

        UILabel uil = transform.FindChild("Label").GetComponent<UILabel>();
        uil.text = abbrev;
        if (Type != BtnType.None) uil.enabled = true;
        else uil.enabled = false;

    }

    internal void OnClick()
    {
        if (Type == BtnType.None) return;

        Type = BtnType.None;
        TooltipScript.me.Hide();
    }

    internal void OnHover(bool isOver)
    {
        if (Type == BtnType.None) return;

        if (isOver)
        {
            // populate data

            TooltipScript.me.Show();
        }
        else
        {
            TooltipScript.me.Hide();
        }
    }

}
