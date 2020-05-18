using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionOfAcidAndOxides : MonoBehaviour
{
    public GameObject button;
    public GameObject solid, liquid;
    private static Color brownColor, colorless;
    private static string acidName, oxideName;
    private bool acidCheck, oxideCheck, mixButton;
    private static bool filledAcid, filledOxide;

    private PatternClass pattern = new PatternClass();

    private List<string> oxidationDegree1 = new List<string>() { "I", "Br", "Cl", "NO3", "F", "Li", "Na" };
    private List<string> oxidationDegree2 = new List<string>() { "SO4", "SO3", "S", "SiO3", "CO3", "Ba", "Ca",
    "Mg", "Zn", "Mn", "Cu", "Fe" };
    private List<string> oxidationDegree3 = new List<string>() { "PO4", "Al", "Cr" };

    // Start is called before the first frame update
    void Start()
    {
        brownColor = solid.GetComponent<Renderer>().material.color;
        colorless = liquid.GetComponent<Renderer>().material.color;

        pattern.SettingActive(solid, liquid, false);
    }

    // Update is called once per frame
    void Update()
    {
        pattern.AddingReagent(acidName, "FirstComponent", acidCheck);
        pattern.AddingReagent(oxideName, "SecondComponent", oxideCheck);
        GettingResult(acidName, oxideName, "Result", ref mixButton);
    }

    public void PressedAcid()
    {
        acidCheck = true;
        acidName = this.button.name;
        if (acidName != null)
            filledAcid = true;
    }

    public void PressedOxide()
    {
        oxideCheck = true;
        oxideName = this.button.name;
        if (oxideName != null)
            filledOxide = true;
    }

    public void PressedMix()
    {
        mixButton = true;
    }

    private void GettingResult(string acid, string oxide, string gameObj, ref bool check)
    {
        if (check && filledAcid && filledOxide)
        {
            string res;
            res = CountingResult(acid, oxide);
            
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
                FillingSolidColor(res, pattern.brownSolid, "brown", brownColor);
                FillingSolidColor(res, pattern.redSolid, "red", Color.red);

                FillingLiquidColor(res, pattern.whiteLiquid, "white", Color.white);
                FillingLiquidColor(res, pattern.pinkLiquid, "pink", Color.magenta);
                FillingLiquidColor(res, pattern.brownLiquid, "brown", brownColor);
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

    private string CountingResult(string acid, string oxide)
    {
        if (oxide == "Cl2O7")
            return "X";
        else
        {
            string metalIon = pattern.TransFormOxideToMetalIon(oxide);
            string acidIon = pattern.TransformToAcidReduceIon(acid);

            if (oxidationDegree1.Contains(metalIon) && oxidationDegree1.Contains(acidIon) ||
            oxidationDegree2.Contains(metalIon) && oxidationDegree2.Contains(acidIon) ||
            oxidationDegree3.Contains(metalIon) && oxidationDegree3.Contains(acidIon))
                return metalIon + acidIon;

            if (oxidationDegree2.Contains(metalIon) && oxidationDegree1.Contains(acidIon))
            {
                if (acidIon == "I" || acidIon == "Cl" || acidIon == "Br" || acidIon == "F")
                    return metalIon + acidIon + "2";
                else
                    return metalIon + "(" + acidIon + ")2";
            }

            if (oxidationDegree1.Contains(metalIon) && oxidationDegree2.Contains(acidIon))
                return metalIon + "2" + acidIon;

            if (oxidationDegree3.Contains(metalIon) && oxidationDegree1.Contains(acidIon))
            {
                if (acidIon == "I" || acidIon == "Cl" || acidIon == "Br" || acidIon == "F" || acidIon == "S")
                    return metalIon + acidIon + "3";
                else
                    return metalIon + "(" + acidIon + ")3";
            }

            if (oxidationDegree1.Contains(metalIon) && oxidationDegree3.Contains(acidIon))
                return metalIon + "3" + acidIon;

            if (oxidationDegree3.Contains(metalIon) && oxidationDegree2.Contains(acidIon))
            {
                if (acidIon == "I" || acidIon == "Cl" || acidIon == "Br" || acidIon == "F" || acidIon == "S")
                    return metalIon + "2" + acidIon + "3";
                else
                    return metalIon + "2(" + acidIon + ")3";
            }

            if (oxidationDegree2.Contains(metalIon) && oxidationDegree3.Contains(acidIon))
            {
                if (acidIon == "I" || acidIon == "Cl" || acidIon == "Br" || acidIon == "F" || acidIon == "S")
                    return metalIon + "3" + acidIon + "2";
                else
                    return metalIon + "3(" + acidIon + ")2";
            }
            return "X";
        }
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
