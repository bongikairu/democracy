using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ActionScript : MonoBehaviour
{

    public enum Action
    {
        None,
        SinatraCare,
        HospitalStock,
        OTOP,
        RaiseMinWage,
        DrugWars,
        KillSam,
        OCPC,
        SarkhanWeekly,
        MyFirstCar,
        GoldenLandAirport,
        PauseLoan,
        RicePriceGuarantee,
        FamilyGeneral,
        FamilyPolice,
        Privatization,
        FreeUtils,
        WealthVilleLand,
        ThailandTelecom,
        TelecomTax,
        ForeignTelecom
    };

    public static string GetName(Action act)
    {
        switch (act)
        {
            case Action.SinatraCare:        return "Sinatra Care";
            case Action.HospitalStock:      return "That hospital look profitable";
            case Action.OTOP:               return "One District One Product";
            case Action.RaiseMinWage:       return "Raise Minimum Wage";
            case Action.DrugWars:           return "War against Drugs";
            case Action.KillSam:            return "Kill that Sam Neil";
            case Action.OCPC:               return "One Computer per Child";
            case Action.SarkhanWeekly:      return "Someone stop that Sarkhan Weekly";
            case Action.MyFirstCar:         return "My First Car";
            case Action.GoldenLandAirport:  return "Golden Land Airport";
            case Action.PauseLoan:          return "Hold the farmer loan";
            case Action.RicePriceGuarantee: return "Rice Price is 15 K§, guaranteed!";
            case Action.FamilyGeneral:      return "That guy is my relative";
            case Action.FamilyPolice:       return "That guy is also my relative";
            case Action.Privatization:      return "Gov Enterprise should be private";
            case Action.FreeUtils:          return "Free water! Free electricity! Hooray!";
            case Action.WealthVilleLand:    return "Make sure I win the auction";
            case Action.ThailandTelecom:    return "My Neighbor Thailando";
            case Action.TelecomTax:         return "Stright to the point, lower the tax";
            case Action.ForeignTelecom:     return "And then sell it to foreigner";
            default: return "None";
        }
    }

    public static string GetAbbrev(Action act)
    {
        switch (act)
        {
            case Action.SinatraCare:        return "SC";
            case Action.HospitalStock:      return "HPT";
            case Action.OTOP:               return "ODOP";
            case Action.RaiseMinWage:       return "300";
            case Action.DrugWars:           return "DRUG";
            case Action.KillSam:            return "SAM";
            case Action.OCPC:               return "OCPC";
            case Action.SarkhanWeekly:      return "SW";
            case Action.MyFirstCar:         return "CAR";
            case Action.GoldenLandAirport:  return "AIR";
            case Action.PauseLoan:          return "LOAN";
            case Action.RicePriceGuarantee: return "15K";
            case Action.FamilyGeneral:      return "AGEN";
            case Action.FamilyPolice:       return "PGEN";
            case Action.Privatization:      return "PRI";
            case Action.FreeUtils:          return "FREE";
            case Action.WealthVilleLand:    return "AUC";
            case Action.ThailandTelecom:    return "THD";
            case Action.TelecomTax:         return "TAX";
            case Action.ForeignTelecom:     return "SELL";
            default: return "Unknown";
        }
    }

    public static ActionButtonScript.BtnType GetType(Action act)
    {
        switch (act)
        {
            case Action.SinatraCare: return ActionButtonScript.BtnType.Campaign;
            case Action.HospitalStock: return ActionButtonScript.BtnType.Corrupt;
            case Action.OTOP: return ActionButtonScript.BtnType.Campaign;
            case Action.RaiseMinWage: return ActionButtonScript.BtnType.Campaign;
            case Action.DrugWars: return ActionButtonScript.BtnType.Campaign;
            case Action.KillSam: return ActionButtonScript.BtnType.Command;
            case Action.OCPC: return ActionButtonScript.BtnType.Campaign;
            case Action.SarkhanWeekly: return ActionButtonScript.BtnType.Command;
            case Action.MyFirstCar: return ActionButtonScript.BtnType.Campaign;
            case Action.GoldenLandAirport: return ActionButtonScript.BtnType.Campaign;
            case Action.PauseLoan: return ActionButtonScript.BtnType.Campaign;
            case Action.RicePriceGuarantee: return ActionButtonScript.BtnType.Campaign;
            case Action.FamilyGeneral: return ActionButtonScript.BtnType.Command;
            case Action.FamilyPolice: return ActionButtonScript.BtnType.Command;
            case Action.Privatization: return ActionButtonScript.BtnType.Command;
            case Action.FreeUtils: return ActionButtonScript.BtnType.Campaign;
            case Action.WealthVilleLand: return ActionButtonScript.BtnType.Corrupt;
            case Action.ThailandTelecom: return ActionButtonScript.BtnType.Corrupt;
            case Action.TelecomTax: return ActionButtonScript.BtnType.Corrupt;
            case Action.ForeignTelecom: return ActionButtonScript.BtnType.Corrupt;
            default: return ActionButtonScript.BtnType.None;
        }
    }

    public static int GetPrice(Action act)
    {
        switch (act)
        {
            case Action.SinatraCare: return 35000;
            case Action.HospitalStock: return 0;
            case Action.OTOP: return 6000;
            case Action.RaiseMinWage: return 0;
            case Action.DrugWars: return 5000;
            case Action.KillSam: return 0;
            case Action.OCPC: return 15000;
            case Action.SarkhanWeekly: return 0;
            case Action.MyFirstCar: return 20000;
            case Action.GoldenLandAirport: return 70000;
            case Action.PauseLoan: return 9000;
            case Action.RicePriceGuarantee: return 15000;
            case Action.FamilyGeneral: return 0;
            case Action.FamilyPolice: return 0;
            case Action.Privatization: return 25000;
            case Action.FreeUtils: return 5000;
            case Action.WealthVilleLand: return 0;
            case Action.ThailandTelecom: return 0;
            case Action.TelecomTax: return 0;
            case Action.ForeignTelecom: return 0;
            default: return 0;
        }
    }

    public static int GetDays(Action act)
    {
        switch (act)
        {
            case Action.SinatraCare: return 120;
            case Action.HospitalStock: return 30;
            case Action.OTOP: return 30;
            case Action.RaiseMinWage: return 30;
            case Action.DrugWars: return 90;
            case Action.KillSam: return 30;
            case Action.OCPC: return 90;
            case Action.SarkhanWeekly: return 30;
            case Action.MyFirstCar: return 90;
            case Action.GoldenLandAirport: return 180;
            case Action.PauseLoan: return 60;
            case Action.RicePriceGuarantee: return 60;
            case Action.FamilyGeneral: return 30;
            case Action.FamilyPolice: return 30;
            case Action.Privatization: return 60;
            case Action.FreeUtils: return 30;
            case Action.WealthVilleLand: return 30;
            case Action.ThailandTelecom: return 60;
            case Action.TelecomTax: return 30;
            case Action.ForeignTelecom: return 30;
            default: return 30;
        }
    }

    public static string GetDetail(Action act)
    {
        string money = (GetPrice(act)>0)?GameRunner.FormatMoney(GetPrice(act)):"Free";
        switch (GetType(act))
        {
            case ActionButtonScript.BtnType.Campaign:
                return "Campaign, " + money + ", " + GetDays(act) + " days";
            case ActionButtonScript.BtnType.Command:
                return "Command, " + money + ", " + GetDays(act) + " days";
            case ActionButtonScript.BtnType.Corrupt:
                return "Corruption, " + money + ", " + GetDays(act) + " days";
            default:
                return "Unknown";
        }
    }

    public static string GetDesc(Action act)
    {
        switch (act)
        {
            case Action.SinatraCare: 
                return  "With only 30 §, you can visit doctor for any\n"+
                        "possible problem you have. Cancer? We might\n"+
                        "have a cure for that.";
            case Action.HospitalStock:
                return  "With the latest bill, Middle Class prefer\n" +
                        "private hospital over public one. You should\n" +
                        "buy stock and get that profit too.";
            case Action.OTOP:
                return  "Business is hard when you don't have money to\n" +
                        "start up. Gov will give each district some\n" +
                        "cash so they can start their own business.";
            case Action.RaiseMinWage:
                return  "Many people only make minimum wage per day.\n" +
                        "Raising it will surely gain their support.\n" +
                        "Business cost? Who the hell cares?";
            case Action.DrugWars:
                return  "Drugs are bad, m'kay?\n" +
                        "Wiping it out is considered the good things,\n" +
                        "if nothing goes wrong..";
            case Action.KillSam:
                return  "Violent actions against innocent civilian\n" +
                        "got you into trouble. It's mostly brought to\n" +
                        "light by Attorney Sam Neil. Only if he is gone..";
            case Action.OCPC:
                return  "No one will argue that computer isn't important\n" +
                        "anymore. You may made some money from this, but\n" +
                        "be aware that they will be more intelligent.";
            case Action.SarkhanWeekly:
                return  "There’s currently a show that try to exposed your\n" +
                        "tricks. Pulling it down sure help lower Lower Class\n" +
                        "insight, but will cause uprising from Middle Class";
            case Action.MyFirstCar: 
                return  "Statistically speaking, there are currently more\n"+
                        "car than the road can handle is Sarkhan. So?\n"+
                        "Just give them car tax exempted. Happy for all.";
            case Action.GoldenLandAirport: 
                return  "Sarkhan’s airport have run at max capacity \n"+
                        "for over 20 years. New airport is a great idea.\n"+
                        "You will gain a significant amount of money from it.";
            case Action.PauseLoan: 
                return "When farmer don’t have to pay the loan back\n" +
                        "now. They will save it for later, or will they?\n" +
                        "Anyway, just do it!";
            case Action.RicePriceGuarantee:
                return  "Why guarantee the price of 15 K§ when it’s\n" +
                        "generally sold at 8 K§. No reason at all!,\n" +
                        "except you will gain more Lower Class supports.";
            case Action.FamilyGeneral: // อันนี้ทหาร - action แต่งตั้งทหารมาก่อนตำรวจ
                return "A commander-in-chief sure is a man of power.\n" +
                        "But who will command him?";
            case Action.FamilyPolice: // แต่งตั้งพรรคพวกเข้าไปเป็นหัวหน้าตำรวจหน่ะ
                return "Want a decent chief of police? You get it!\n" +
                        "How can I know? He’s my relative!";
            case Action.Privatization: // แปรรูป ปตท
                return "Some enterprise is making money while many\n" +
                        "isn’t. Grab its profit slices and make them private.\n" +
                        "So they can grow and make more profit.";
            case Action.FreeUtils: // ไฟฟรี น้ำฟรี
                return "Paying so much for so little units of utilities?\n" +
                        "Get it for free! No Charge! Don’t worry about it.\n" +
                        "We already gain enough from others.";
            case Action.WealthVilleLand: // ที่ดินรัชดา
                return "Honey, I want that land so much\n" +
                        "Make sure I got that, or you will feel my wrath";
            case Action.ThailandTelecom: // ขายระบบโทรศัพท์ให้พม่า (thailando) โดยเงินไม่ออกนอกประเทศ
                return "Our neighbor Thailando is getting a new telephone\n" +
                        "system. I will have them buy mine. But make sure\n" +
                        "that money never leave Sarkhan, or its value will drops.";
            case Action.TelecomTax: // กล่าวถึงเรื่องลดภาษีโทรคมนาคม
                return "Communication - the human connection - \n" +
                        "is the key to personal and career success.\n" +
                        "I’m making it personal career success.";
            case Action.ForeignTelecom: // กล่าวถึงเรื่องขาย Shin Corp ให้ต่างชาติ
                return "They said telephone is a nation’s property.\n" +
                        "But mommmmm, I founded that company.\n" +
                        "I should be able to sell this to anyone.";
            default: return "Unknown";
        }
    }

    public static string GetEffects(Action act)
    {
        switch (act)
        {
            case Action.SinatraCare: return "+1 M§/m, Nation revenue -25%";
            case Action.HospitalStock: return "5% of nation revenue credit to you";
            case Action.OTOP: return "+1 M§/m, Nation revenue -200 M§/m";
            case Action.RaiseMinWage: return "Nation revenue -25%";
            case Action.DrugWars: return "Influence will decrease over time until fixed";
            case Action.KillSam: return "Stop influence from dropping over time";
            case Action.OCPC: return "+1 M§/m, Corruption exposed make LC angrier";
            case Action.SarkhanWeekly: return "Corruption exposed affect lower on LC but more on MC";
            case Action.MyFirstCar: return "Nation revenue -300 M§/m";
            case Action.GoldenLandAirport: return "You gain 360 M§ over 6 months";
            case Action.PauseLoan: return "Nation revenue -400 M§/m";
            case Action.RicePriceGuarantee: return "Nation revenue -25%";
            case Action.FamilyGeneral: return "Armed Force support falls slower";
            case Action.FamilyPolice: return "Corruption exposed don't decrease your income anymore";
            case Action.Privatization: return "Nation revenue -25%, 5% of revenue credit to you";
            case Action.FreeUtils: return "+1 M§/m, Nation revenue -200 M§/m";
            case Action.WealthVilleLand: return "-";
            case Action.ThailandTelecom: return "You gain 200 M§ upfront";
            case Action.TelecomTax: return "+1 M§/m, Nation revenue -100 M§/m";
            case Action.ForeignTelecom: return "You gain 300 M§ upfront";
            default: return "-";
        }
    }

    public static string GetPro(Action act)
    {

        string input = "";

        switch (act)
        {
            case Action.SinatraCare: input = "LC++"; break;
            case Action.HospitalStock: input = "F+"; break;
            case Action.OTOP: input = "LC++"; break;
            case Action.RaiseMinWage: input = "LC++ MC- HC--"; break;
            case Action.DrugWars: input = "LC+ MC++ HC+"; break;
            case Action.KillSam: input = "MC--"; break;
            case Action.OCPC: input = "LC++ MC+"; break;
            case Action.SarkhanWeekly: input = "MC--"; break;
            case Action.MyFirstCar: input = "LC+ MC++ HC+"; break;
            case Action.GoldenLandAirport: input = "MC++ HC++"; break;
            case Action.PauseLoan: input = "LC++"; break;
            case Action.RicePriceGuarantee: input = "LC++"; break;
            case Action.FamilyGeneral: input = "F+ A++"; break;
            case Action.FamilyPolice: input = "F+ A+ MC-"; break;
            case Action.Privatization: input = "MC- HC-"; break;
            case Action.FreeUtils: input = "LC++"; break;
            case Action.WealthVilleLand: input = "F++ MC-"; break;
            case Action.ThailandTelecom: input = "F+ MC-"; break;
            case Action.TelecomTax: input = "F+"; break;
            case Action.ForeignTelecom: input = "F+ MC-"; break;
            default: input = ""; break;
        }

        string pattern = "([LCMHAF]+\\++)";
        string replacement = "[77FF77]$1[FFFFFF]";
        Regex rgx = new Regex(pattern);
        input = rgx.Replace(input, replacement);

        pattern = "([LCMHAF]+\\-+)";
        replacement = "[FF7777]$1[FFFFFF]";
        Regex rgx2 = new Regex(pattern);
        input = rgx2.Replace(input, replacement);

        return input;

    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
