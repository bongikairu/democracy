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

    public ActionScript.Action Action;
    public BtnType Type;
    public string Abbrev;

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

        Type = ActionScript.GetType(Action);
        Abbrev = ActionScript.GetAbbrev(Action);

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
        uil.text = Abbrev;
        if (Type != BtnType.None) uil.enabled = true;
        else uil.enabled = false;

    }

    internal void OnClick()
    {
        if (Type == BtnType.None)
        {
            GameObject.Find("MenuSound2").GetComponent<AudioSource>().time = 0f;
            GameObject.Find("MenuSound2").GetComponent<AudioSource>().Play();
            return;
        }

        

        if (GameRunner.me.DoAction(Action))
        {
            Action = ActionScript.Action.None;
            TooltipScript.me.Hide();

            GameObject.Find("MenuSound").GetComponent<AudioSource>().time = 0f;
            GameObject.Find("MenuSound").GetComponent<AudioSource>().Play();

        }
        else
        {
            GameObject.Find("MenuSound2").GetComponent<AudioSource>().time = 0f;
            GameObject.Find("MenuSound2").GetComponent<AudioSource>().Play();
        }
        
    }

    internal void OnHover(bool isOver)
    {
        if (Type == BtnType.None) return;

        if (isOver)
        {
            // populate data
            TooltipScript.me.transform.FindChild("Name").GetComponent<UILabel>().text = ActionScript.GetName(Action);
            TooltipScript.me.transform.FindChild("Details").GetComponent<UILabel>().text = ActionScript.GetDetail(Action);
            TooltipScript.me.transform.FindChild("Desc").GetComponent<UILabel>().text = ActionScript.GetDesc(Action);
            TooltipScript.me.transform.FindChild("Effects").GetComponent<UILabel>().text = ActionScript.GetEffects(Action);
            TooltipScript.me.transform.FindChild("Pro").GetComponent<UILabel>().text = ActionScript.GetPro(Action);

            TooltipScript.me.Show();
        }
        else
        {
            TooltipScript.me.Hide();
        }
    }

}
