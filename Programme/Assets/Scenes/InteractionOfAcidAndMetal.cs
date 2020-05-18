using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionOfAcidAndMetal : MonoBehaviour
{
    public GameObject button;
    public GameObject solid, liquid;
    private static Color brownColor;
    private static string acidName, metalName;
    private bool acidCheck, metalCheck, mixButton;
    private static bool filledAcid, filledMetal;

    private PatternClass pattern = new PatternClass();

    private List<string> oxidationDegree1 = new List<string>() { "I", "Br", "Cl", "NO3", "F", "Li", "Na", "K" };
    private List<string> oxidationDegree2 = new List<string>() { "SO4", "SO3", "S", "SiO3", "CO3", "Ba", "Ca",
    "Mg", "Zn", "Cu", "Fe", "Pb", "Hg" };
    private List<string> oxidationDegree3 = new List<string>() { "PO4", "Al" };

    private readonly List<string> impossibleSolid = new List<string>() { "FeS", "BaSO3", "CaSO3", "Li3PO4", "Ba3(PO4)2",
    "Zn3(PO4)2", "AlPO4", "Fe3(PO4)2", "CaS", "ZnS","CaCO3", "FeCO3", "Ca3(PO4)2", "Mg3(PO4)2", "CaF2","MgF2", "PbI2",
    "PbF2", "Pb3(PO4)2", "PbSO3", "PbCO3", "PbS", "HgI2", "CuI2", "HgBr2",
    "CuBr2", "HgCl2", "CuCl2", "HgSO3", "CuSO3", "Hg3(PO4)2", "Cu3(PO4)2", "CuF2", "HgF2", "HgS", "Cus", "HgCO3", "CuCO3",
    "HgF2", "CuF2"};


    // Start is called before the first frame update
    void Start()
    {
        brownColor = solid.GetComponent<Renderer>().material.color;
        pattern.SettingActive(solid, liquid, false);
    }

    // Update is called once per frame
    void Update()
    {
        pattern.AddingReagent(acidName, "FirstComponent", acidCheck);
        pattern.AddingReagent(metalName, "SecondComponent", metalCheck);
        GettingResult(acidName, metalName, "Result", ref mixButton);
    }

    public void PressedAcid()
    {
        acidCheck = true;
        acidName = this.button.name;
        if (acidName != null)
            filledAcid = true;
    }

    public void PressedMetal()
    {
        metalCheck = true;
        metalName = this.button.name;
        if (metalName != null)
            filledMetal = true;
    }

    public void PressedMix()
    {
        mixButton = true;
    }

    private void GettingResult(string acid, string metal, string gameObj, ref bool check)
    {
        if (check && filledAcid && filledMetal)
        {
            string res;
            res = CountingResult(acid, metal);

            if (pattern.impossibleRes.Contains(res) || impossibleSolid.Contains(res) || res == "X")
            {
                GameObject.Find(gameObj).GetComponentInChildren<Text>().text = "Impossible reaction";
                GameObject.Find("Definition").GetComponent<Text>().text = "";
                pattern.SettingActive(solid, liquid, false);
            }
            else
            {
                FillingSolidColor(res, pattern.whiteSolid, "white", Color.white);
                FillingLiquidColor(res, pattern.whiteLiquid, "white", Color.white);
                FillingLiquidColor(res, pattern.brownLiquid, "brown", brownColor);
                FillingLiquidColor(res, pattern.yellowLiquid, "yellow", Color.yellow);
                FillingLiquidColor(res, pattern.greenLiquid, "green", Color.green);
                
                if (acid == "H2SO4" || acid == "HNO3")
                GameObject.Find(gameObj).GetComponentInChildren<Text>().text = CheckingConcentratedAcid(res, metal);
                else
                GameObject.Find(gameObj).GetComponentInChildren<Text>().text = res + " + H2";
            }
            check = false;
        }
        else
            check = false;
    }

    private string CountingResult(string acid, string metal)
    {
        string acidIon = pattern.TransformToAcidReduceIon(acid);

        if (acid == "H2SiO3")
            return "X";
        else
        {
            if (oxidationDegree1.Contains(metal) && oxidationDegree1.Contains(acidIon) ||
                oxidationDegree2.Contains(metal) && oxidationDegree2.Contains(acidIon) ||
                oxidationDegree3.Contains(metal) && oxidationDegree3.Contains(acidIon))
                return metal + acidIon;

            if (oxidationDegree2.Contains(metal) && oxidationDegree1.Contains(acidIon))
            {
                if (acidIon == "I" || acidIon == "Cl" || acidIon == "Br" || acidIon == "F")
                    return metal + acidIon + "2";
                else
                    return metal + "(" + acidIon + ")2";
            }

            if (oxidationDegree1.Contains(metal) && oxidationDegree2.Contains(acidIon))
                return metal + "2" + acidIon;

            if (oxidationDegree3.Contains(metal) && oxidationDegree1.Contains(acidIon))
            {
                if (acidIon == "I" || acidIon == "Cl" || acidIon == "Br" || acidIon == "F" || acidIon == "S")
                    return metal + acidIon + "3";
                else
                    return metal + "(" + acidIon + ")3";
            }

            if (oxidationDegree1.Contains(metal) && oxidationDegree3.Contains(acidIon))
                return metal + "3" + acidIon;

            if (oxidationDegree3.Contains(metal) && oxidationDegree2.Contains(acidIon))
            {
                if (acidIon == "I" || acidIon == "Cl" || acidIon == "Br" || acidIon == "F" || acidIon == "S")
                    return metal + "2" + acidIon + "3";
                else
                    return metal + "2(" + acidIon + ")3";
            }

            if (oxidationDegree2.Contains(metal) && oxidationDegree3.Contains(acidIon))
            {
                if (acidIon == "I" || acidIon == "Cl" || acidIon == "Br" || acidIon == "F" || acidIon == "S")
                    return metal + "3" + acidIon + "2";
                else
                    return metal + "3(" + acidIon + ")2";
            }
            return "X";
        }
    }

    private string CheckingConcentratedAcid(string res, string metal)
    {
        if (res.Contains("SO4"))
        {
            switch (metal)
            {
                case "Fe":
                    return $"{res} + H2O";
                case "Pb":
                    return $"{res} + S + H2O";
                case "Al":
                    return $"{res} + H2";
                case "Cu":
                case "Hg":
                    return $"{res} + SO2 + H2O";
                default:
                    return $"{res} + H2S + H2O";
            }
        }
            else
        {
            switch (metal)
            {
                case "Fe":
                    return $"{res} + N2 + H2O";
                case "Cu":
                    case "Hg":
                    return $"{res} + NO2 + H2O";
                default:
                    return $"{res} + N2O + H2O";
            }
        }
    }

    private void FillingSolidColor(string substance, List<string> list, string strColor, Color color)
    {
        if (list.Contains(substance))
        {
            solid.SetActive(true);
            liquid.SetActive(false);
            solid.GetComponent<Renderer>().material.color = color;
            GameObject.Find("Definition").GetComponent<Text>().text = $"{strColor} sediment";
        }
    }

    private void FillingLiquidColor(string substance, List<string> list, string strColor, Color color)
    {
        if (list.Contains(substance))
        {
            solid.SetActive(false);
            liquid.SetActive(true);
            liquid.GetComponent<Renderer>().material.color = color;
            GameObject.Find("Definition").GetComponent<Text>().text = $"{strColor} liquid";
        }
    }
}
