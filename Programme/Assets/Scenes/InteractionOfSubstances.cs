using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionOfSubstances : MonoBehaviour
{
    public GameObject button;
    public GameObject powder, hygroscopicSolid, liquid;
    private Color brownColor;

    private PatternClass pattern = new PatternClass();

    private bool reactant1, reactant2, mixButton;
    private static bool filled1, filled2;

    private static string reagentName1, reagentName2;

    private List<string> oxidationDegree1 = new List<string>() { "Li", "Na", "K", "Rb", "Ag", "Cs", "Au", "H", "F",
    "Cl", "Br", "I" };
    private List<string> oxidationDegree2 = new List<string>() { "Be", "Mg", "Ca", "Mn", "Cu", "Zn", "Sr", "Pt",
    "Ba", "Hg", "Pb", "B", "O", "S" };
    private List<string> oxidationDegree3 = new List<string>() { "Al", "Cr", "Fe", "N", "P" };
    private List<string> oxidationDegree4 = new List<string>() { "Sn", "C", "Si" };

    private List<string> whiteSolid = new List<string>() { "LiH", "KH", "BeH2", "CaF2", "NaH", "MgH2", "CaH2", "RbF",
    "RbH", "Sr2H", "CsH", "CsF", "FrH", "SnO2", "AlF3", "HgF2", "BeS", "MgS", "Al2S3", "Ag2S", "BeCl2", "MgCl2", "AlCl3",
    "KCl", "CaCl2", "ZnCl2", "RbCl", "BaCl2", "HgCl2", "LiBr", "BeBr2", "NaBr", "RbBr", "AlJ3", "KJ", "CaJ2", "CuJ",
    "ZnJ2", "RbJ", "CsJ", "BaJ2", "BaH2", "RaH2", "Li2O", "Li2C2", "Na2C2", "SrO", "BaO",
    "BeF2", "MgF2", "SrF2", "SnF4", "PbF2", "RaF2", "ZnS", "BaS", "LiCl", "SrCl2", "CsCl", "PbCl2", "MgBr2", "AlBr3",
    "Kbr", "CaBr2", "ZnBr2", "SrBr2", "SnBr4", "CsBr", "HgBr2", "PbBr2", "LiJ", "BeJ2", "MgJ2", "SrJ2", "Pt2B",
    "Pt2C", "PtSi", "NaSi", "NaF", "AgF", "K2S" };
    private List<string> blackSolid = new List<string>() { "MgBr2", "CaB6", "Ca3N2", "SrB6", "BaB6", "CrN", "Mn3C",
    "KSi", "Cu3P", "Ba3P2", "Cr2S3", "FeS", "CrJ3", "PtJ2", "MnB", "Be2C", "CrF4", "Al4C3", "Sr3N2", "PtF4", "Ca3P2", "Sr3P2",
    "FeCl3", "PtCl2", "PtBr2" };
    private List<string> yellowSolid = new List<string>() { "CrB", "Mg3N2", "Ba3N2", "Rb2O", "AuF3", "FeSi", "Mg3P2", "AlP",
    "Rb2P5", "Cs2P5", "Li2S", "CuCl2", "AuCl", "AgBr", "AgJ", "SnJ4", "AuJ", "PbJ2" };
    private List<string> greySolid = new List<string>() { "FeB", "CaC2", "Na3N", "Fe3C", "Mn4N", "PtO", "CaSi2", "Mn3Si", "Cu2Si",
    "RbSi", "MnP", "Fe3P", "Zn3P2", "Ag3P", "SnP", "PtP2", "Cu2S", "SrS", "HgS"};
    private List<string> greenSolid = new List<string>() { "Li3N", "K3N", "K3P", "MnO", "FeF3", "CrBr3", "CuBr2" };
    private List<string> redSolid = new List<string>() { "Cs2O", "HgO", "PbO", "Li3P", "Na3P", "MnS", "Rb2S", "Cs2S", "FeBr3",
    "FeJ2", "HgJ2" };
    private List<string> blueSolid = new List<string>() { "MnF4", "PbS", "Mg2Si" };
    private List<string> pinkSolid = new List<string>() { "MnCl2", "MnBr2", "MnJ2", "Li6Si2", "CrCl3" };
    private List<string> whitePowder = new List<string>() { "SnCl2", "AlN", "BeO", "MgO", "Na2O", "Al2O3", "CaO", "ZnO", "LiF",
    "KF", "CuF2", "ZnF2", "BaF2", "Na2S", "CaS", "NaCl", "AgCl", "NaJ", "BaBr2" };
    private List<string> greyPowder = new List<string>() { "Cr3C2", "K2O" };
    private List<string> greenPowder = new List<string>() { "Cr2O3" };
    private List<string> yellowPowder = new List<string>() { "Be3N2", "HgI2" };
    private List<string> blackPowder = new List<string>() { "FeO", "CuO", "Ag2O", "SnS" };
    private List<string> redPowder = new List<string>() { "Au2O3" };

    public void PressedMetal()
    {
        reactant1 = true;
        reagentName1 = this.button.name;
        if (reagentName1 != null)
            filled1 = true;
    }

    public void PressedNonmetal()
    {
        reactant2 = true;
        reagentName2 = this.button.name;
        if (reagentName2 != null)
            filled2 = true;
    }

    public void PressedMix()
    {
        mixButton = true;
    }

    // Start is called before the first frame update.
    void Start()
    {
        try
        {
            powder.SetActive(false);
            hygroscopicSolid.SetActive(false);
            liquid.SetActive(false);
        }
        catch { }
    }

    // Update is called once per frame.
    void Update()
    {
        pattern.AddingReagent(reagentName1, "ChosenMetal", reactant1);
        pattern.AddingReagent(reagentName2, "ChosenNonMetal", reactant2);
        GettingResult(reagentName1, reagentName2, "Result", ref mixButton);
    }

    private void GettingResult(string elem1, string elem2, string gameObj, ref bool check)
    {
        if (check && filled1 && filled2)
        {
            string res;
            res = CountingResult(elem1, elem2);

            FillingSolidColor(res, whiteSolid, "white", Color.white);
            FillingSolidColor(res, blackSolid, "black", Color.black);
            FillingSolidColor(res, yellowSolid, "yellow", Color.yellow);
            FillingSolidColor(res, greySolid, "grey", Color.grey);
            FillingSolidColor(res, greenSolid, "green", Color.green);
            FillingSolidColor(res, redSolid, "red", Color.red);
            FillingSolidColor(res, blueSolid, "blue", Color.blue);
            FillingSolidColor(res, pinkSolid, "pink", Color.magenta);

            FillingPowderColor(res, whitePowder, "white", Color.white);
            FillingPowderColor(res, blackPowder, "black", Color.black);
            FillingPowderColor(res, yellowPowder, "yellow", Color.yellow);
            FillingPowderColor(res, greyPowder, "grey", Color.grey);
            FillingPowderColor(res, greenPowder, "green", Color.green);
            FillingPowderColor(res, redPowder, "red", Color.red);

            if (res == "SnCl4")
            {
                hygroscopicSolid.SetActive(false);
                powder.SetActive(false);
                liquid.SetActive(true);
                liquid.GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("Definition").GetComponent<Text>().text = "white liquid";
            }

            GameObject.Find(gameObj).GetComponentInChildren<Text>().text = res;
            if (res == "X")
            {
                GameObject.Find("Definition").GetComponent<Text>().text = "doesn't exist";
                powder.SetActive(false);
                hygroscopicSolid.SetActive(false);
                liquid.SetActive(false);
            }
            mixButton = false;
        }
        else
            check = false;
    }

    private string CountingResult(string me, string nonMe)
    {
        switch (nonMe)
        {
            case "H":
                return InteractionOfHydrogen(me);
            case "B":
                return InteractionOfBarium(me);
            case "C":
                return InteractionOfCarbon(me);
            case "N":
                return InteractionOfNitrogen(me);
            case "O":
                return InteractionOfOxygen(me);
            case "F":
                return InteractionOfFluorine(me);
            case "Si":
                return InteractionOfSilicon(me);
            case "P":
                return InteractionOfPhosohorus(me);
            case "S":
                return InteractionOfSulfur(me);
            case "Cl":
            case "Br":
                return InteractionOfChlorineAndBromine(nonMe, me);
            case "I":
                return InteractionOfIodine(me);
            default:
                return "There is no such element";
        }
    }

    private string InteractionOfHydrogen(string metal)
    {
        switch (metal)
        {
            case "Li":
            case "Na":
            case "K":
            case "Rb":
            case "Cs":
                return $"{metal}H";
            case "Be":
            case "Mg":
            case "Ca":
            case "Sr":
            case "Ba":
                return $"{metal}H2";
            default:
                return "X";
        }
    }

    private string InteractionOfBarium(string metal)
    {
        switch (metal)
        {
            case "Cr":
            case "Mn":
            case "Fe":
                return $"{metal}B";
            case "Mg":
                return $"{metal}B2";
            case "Ca":
            case "Sr":
            case "Ba":
                return $"{metal}B6";
            case "Pt":
                return $"{metal}2B";
            default:
                return "X";
        }
    }

    private string InteractionOfCarbon(string metal)
    {
        switch (metal)
        {
            case "Li":
            case "Na":
                return $"{metal}2C2";
            case "Ca":
                return $"{metal}C2";
            case "Be":
            case "Pt":
                return $"{metal}2C";
            case "Al":
                return $"{metal}4C3";
            case "Cr":
                return $"{metal}3C2";
            case "Mn":
            case "Fe":
                return $"{metal}3C";
            default:
                return "X";
        }
    }

    private string InteractionOfNitrogen(string metal)
    {
        switch (metal)
        {
            case "Al":
            case "Cr":
                return $"{metal}N";
            case "Mn":
                return $"{metal}4N";
            case "Be":
            case "Mg":
            case "Ca":
            case "Sr":
            case "Ba":
                return $"{metal}3N2";
            case "Li":
            case "Na":
            case "K":
                return $"{metal}3N";
            default:
                return "X";
        }
    }

    private string InteractionOfOxygen(string metal)
    {
        if (metal == "Au")
            return $"{metal}2O3";
        if (metal == "Fe" || metal == "Hg" || metal == "Pb")
            return $"{metal}O";

        if (oxidationDegree1.Contains(metal))
            return $"{metal}2O";
        if (oxidationDegree2.Contains(metal))
            return $"{metal}O";
        if (oxidationDegree3.Contains(metal))
            return $"{metal}2O3";
        if (oxidationDegree4.Contains(metal))
            return $"{metal}O2";

        return "X";
    }

    private string InteractionOfFluorine(string metal)
    {
        if (metal == "Au")
            return $"{metal}F3";
        if (metal == "Pt" || metal == "Mn" || metal == "Cr")
            return $"{metal}F4";

        if (oxidationDegree1.Contains(metal))
            return $"{metal}F";
        if (oxidationDegree2.Contains(metal))
            return $"{metal}F2";
        if (oxidationDegree3.Contains(metal))
            return $"{metal}F3";
        if (oxidationDegree4.Contains(metal))
            return $"{metal}F4";

        return "X";
    }

    private string InteractionOfSilicon(string metal)
    {
        switch (metal)
        {
            case "Na":
            case "K":
            case "Fe":
            case "Rb":
            case "Pt":
                return $"{metal}Si";
            case "Mg":
            case "Cu":
                return $"{metal}2Si";
            case "Ca":
                return $"{metal}Si2";
            case "Li":
                return $"{metal}6Si2";
            case "Mn":
                return $"{metal}3Si";
            default:
                return "X";
        }
    }

    private string InteractionOfPhosohorus(string metal)
    {
        switch (metal)
        {
            case "Al":
            case "Mn":
            case "Sn":
                return $"{metal}P";
            case "Li":
            case "Na":
            case "K":
            case "Fe":
            case "Cu":
            case "Ag":
                return $"{metal}3P";
            case "Mg":
            case "Ca":
            case "Zn":
            case "Sr":
            case "Ba":
                return $"{metal}3P2";
            case "Rb":
            case "Cs":
                return $"{metal}2P5";
            case "Pt":
                return $"{metal}P2";
            default:
                return "X";
        }
    }

    private string InteractionOfSulfur(string metal)
    {
        if (metal == "Cu")
            return $"{metal}2S";
        if (metal == "Fe" || metal == "Sn" || metal == "Pb" || metal == "Hg")
            return $"{metal}S";
        if (metal == "Pt" || metal == "Au")
            return "X";

        if (oxidationDegree1.Contains(metal))
            return $"{metal}2S";
        if (oxidationDegree2.Contains(metal))
            return $"{metal}S";
        if (oxidationDegree3.Contains(metal))
            return $"{metal}2S3";
        if (oxidationDegree4.Contains(metal))
            return $"{metal}S2";

        return "X";
    }

    private string InteractionOfChlorineAndBromine(string nonmetal, string metal)
    {
        if (oxidationDegree1.Contains(metal))
            return metal + nonmetal;
        if (oxidationDegree2.Contains(metal))
            return metal + nonmetal + 2;
        if (oxidationDegree3.Contains(metal))
            return metal + nonmetal + 3;
        if (oxidationDegree4.Contains(metal))
            return metal + nonmetal + 4;

        return "X";
    }

    private string InteractionOfIodine(string metal)
    {
        if (metal == "Cu")
            return $"{metal}I";
        if (metal == "Fe")
            return $"{metal}I2";

        if (oxidationDegree1.Contains(metal))
            return $"{metal}I";
        if (oxidationDegree2.Contains(metal))
            return $"{metal}I2";
        if (oxidationDegree3.Contains(metal))
            return $"{metal}I3";
        if (oxidationDegree4.Contains(metal))
            return $"{metal}I4";

        return "X";
    }

    private void FillingSolidColor(string substance, List<string> list, string colorName, Color color)
    {
        if (list.Contains(substance))
        {
            powder.SetActive(false);
            liquid.SetActive(false);
            hygroscopicSolid.SetActive(true);
            hygroscopicSolid.GetComponent<Renderer>().material.color = color;
            GameObject.Find("Definition").GetComponent<Text>().text = $"{colorName} solid";
        }
    }

    private void FillingPowderColor(string substance, List<string> list, string colorName, Color color)
    {
        if (list.Contains(substance))
        {
            hygroscopicSolid.SetActive(false);
            liquid.SetActive(false);
            powder.SetActive(true);
            powder.GetComponent<Renderer>().material.color = color;
            GameObject.Find("Definition").GetComponent<Text>().text = $"{colorName} powder";
        }
    }
}


