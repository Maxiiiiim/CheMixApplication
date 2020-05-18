using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionOfOxideAndHydroxide : MonoBehaviour
{
    public GameObject button;
    public GameObject solid, liquid;
    private static Color colorless;
    private static string oxideName, hydroxideName;
    private bool oxideCheck, hydroxideCheck, mixButton;
    private static bool filledOxide, filledHydroxide;

    private PatternClass pattern = new PatternClass();

    private List<string> oxidationDegree1 = new List<string>() { "PO3", "ClO4", "NO3", "MnO4", "Li", "Na", "K" };
    private List<string> oxidationDegree2 = new List<string>() { "SO4", "SO3", "CrO4", "SiO3", "CO3", "Ba", "Ca",
    "Mg", "Zn", "Mn", "Cu", "Fe" };
    private List<string> oxidationDegree3 = new List<string>() { "Fe", "Al" };

    // Start is called before the first frame update
    void Start()
    {
        colorless = liquid.GetComponent<Renderer>().material.color;
        pattern.SettingActive(solid, liquid, false);
    }

    // Update is called once per frame
    void Update()
    {
        pattern.AddingReagent(hydroxideName, "FirstComponent", hydroxideCheck);
        pattern.AddingReagent(oxideName, "SecondComponent", oxideCheck);
        GettingResult(hydroxideName, oxideName, "Result", ref mixButton);
    }

    public void PressedOxide()
    {
        oxideCheck = true;
        oxideName = this.button.name;
        if (oxideName != null)
            filledOxide = true;
    }

    public void PressedHydroxide()
    {
        hydroxideCheck = true;
        hydroxideName = this.button.name;
        if (hydroxideName != null)
            filledHydroxide = true;
    }

    public void PressedMix()
    {
        mixButton = true;
    }

    private void GettingResult(string hydroxide, string oxide, string gameObj, ref bool check)
    {
        if (check && filledHydroxide && filledOxide)
        {
            string res;
            res = CountingResult(hydroxide, oxide);

            if (pattern.impossibleRes.Contains(res) || res == "X")
            {
                GameObject.Find(gameObj).GetComponentInChildren<Text>().text = "Impossible reaction";
                GameObject.Find("Definition").GetComponent<Text>().text = "";
                pattern.SettingActive(solid, liquid, false);
            }
            else
            {
                liquid.SetActive(true);
                liquid.GetComponent<Renderer>().material.color = colorless;

                FillingSolidColor(res, pattern.whiteSolid, "white", Color.white);
                FillingSolidColor(res, pattern.blackSolid, "black", Color.black);
                FillingSolidColor(res, pattern.blueSolid, "blue", Color.blue);
                FillingSolidColor(res, pattern.greenSolid, "green", Color.green);
                FillingSolidColor(res, pattern.pinkSolid, "pink", Color.magenta);
                FillingSolidColor(res, pattern.yellowSolid, "yellow", Color.yellow);
                FillingSolidColor(res, pattern.redSolid, "red", Color.red);

                FillingLiquidColor(res, pattern.whiteLiquid, "white", Color.white);
                FillingLiquidColor(res, pattern.pinkLiquid, "purple", Color.magenta);
                FillingLiquidColor(res, pattern.blackLiquid, "black", Color.black);
                FillingLiquidColor(res, pattern.yellowLiquid, "yellow", Color.yellow);
                FillingLiquidColor(res, pattern.greenLiquid, "green", Color.green);
                FillingLiquidColor(res, pattern.blueLiquid, "blue", Color.blue);

                GameObject.Find(gameObj).GetComponentInChildren<Text>().text = res + " + H2O";
            }
            check = false;
        }
        else
            check = false;
    }

    private string CountingResult(string hydroxide, string oxide)
    {
        string acidIon, metalIon;
        if (oxide == "CaO" || hydroxide == "Fe(OH)2" || hydroxide == "Fe(OH)3")
        {
            if (oxide == "CaO")
                return "X";
            if (hydroxide == "Fe(OH)2")
            {
                metalIon = "Fe";
                acidIon = TransformToAcidReduceIon(oxide);
                if (oxidationDegree1.Contains(acidIon))
                    return metalIon + "(" + acidIon + ")2";
                if (oxidationDegree2.Contains(acidIon))
                    return metalIon + acidIon;
                if (oxidationDegree3.Contains(acidIon))
                    return metalIon + "3(" + acidIon + ")2";
            }

            if (hydroxide == "Fe(OH)3")
            {
                metalIon = "Fe";
                acidIon = TransformToAcidReduceIon(oxide);
                if (oxidationDegree1.Contains(acidIon))
                    return metalIon + "(" + acidIon + ")3";
                if (oxidationDegree2.Contains(acidIon))
                    return metalIon + "2(" + acidIon + ")3";
                if (oxidationDegree3.Contains(acidIon))
                    return metalIon + acidIon;
            }
            return "X";
        }
        else
        {
            metalIon = pattern.TransformHydroxideToMetalIon(hydroxide);
            acidIon = TransformToAcidReduceIon(oxide);

            if (oxidationDegree1.Contains(metalIon) && oxidationDegree1.Contains(acidIon) ||
            oxidationDegree2.Contains(metalIon) && oxidationDegree2.Contains(acidIon))
                return metalIon + acidIon;

            if (oxidationDegree2.Contains(metalIon) && oxidationDegree1.Contains(acidIon))
                    return metalIon + "(" + acidIon + ")2";

            if (oxidationDegree1.Contains(metalIon) && oxidationDegree2.Contains(acidIon))
                return metalIon + "2" + acidIon;

            if (oxidationDegree3.Contains(metalIon) && oxidationDegree1.Contains(acidIon))
                    return metalIon + "(" + acidIon + ")3";

            if (oxidationDegree3.Contains(metalIon) && oxidationDegree2.Contains(acidIon))
                    return metalIon + "2(" + acidIon + ")3";
            return "X";
        }
    }

    private string TransformToAcidReduceIon(string elem)
    {
        if (elem == "P2O5")
            return "PO3";
        if (elem == "SiO2")
            return "SiO3";
        if (elem == "CO2")
            return "CO3";
        if (elem == "SO3")
            return "SO4";
        if (elem == "SO2")
            return "SO3";
        if (elem == "Cl2O7")
            return "ClO4";
        if (elem == "CrO3")
            return "CrO4";
        if (elem == "Mn2O7")
            return "MnO4";
        if (elem == "N2O5")
            return "NO3";

        return "X";
    }

    private void FillingSolidColor(string substance, List<string> list, string strColor, Color color)
    {
        if (list.Contains(substance))
        {
            solid.SetActive(true);
            solid.GetComponent<Renderer>().material.color = color;
            GameObject.Find("Definition").GetComponent<Text>().text = $"{strColor} sediment";
        }
    }

    private void FillingLiquidColor(string substance, List<string> list, string strColor, Color color)
    {
        if (list.Contains(substance))
        {
            solid.SetActive(false);
            liquid.GetComponent<Renderer>().material.color = color;
            GameObject.Find("Definition").GetComponent<Text>().text = $"{strColor} liquid";
        }
    }
}
