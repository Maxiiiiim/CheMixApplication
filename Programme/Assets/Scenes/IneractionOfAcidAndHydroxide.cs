using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IneractionOfAcidAndHydroxide : MonoBehaviour
{
    public GameObject button;
    public GameObject solidAcid, liquidAcid, solidHydroxide, liquidHydroxide;
    public GameObject solidRes, liquidRes;
    private static Color brownColor, colorless;

    private PatternClass pattern = new PatternClass();

    private bool acidCheck, hydroxideCheck, mixButton;
    private static bool filledAcid, filledHydroxide;
    private static string acidName, hydroxideName;

    private List<string> oxidationDegree1 = new List<string>() { "I", "Br", "Cl", "NO3", "F", "Li", "Na", "K" };
    private List<string> oxidationDegree2 = new List<string>() { "SO4", "SO3", "S", "SiO3", "CO3", "Ba", "Ca",
    "Mg", "Zn", "Mn", "Cu", "Fe" };
    private List<string> oxidationDegree3 = new List<string>() { "PO4", "Fe", "Al" };

    // Start is called before the first frame update
    void Start()
    {
        brownColor = solidRes.GetComponent<Renderer>().material.color;
        colorless = liquidRes.GetComponent<Renderer>().material.color;

        pattern.SettingActive(solidAcid, liquidAcid, false);
        pattern.SettingActive(solidHydroxide, liquidHydroxide, false);
        pattern.SettingActive(solidRes, liquidRes, false);
    }

    // Update is called once per frame
    void Update()
    {
        pattern.AddingReagent(acidName, "FirstComponent", acidCheck);
        DefiningColor(acidName, liquidAcid, solidAcid);
        pattern.AddingReagent(hydroxideName, "SecondComponent", hydroxideCheck);
        DefiningColor(hydroxideName, liquidHydroxide, solidHydroxide);
        GettingResult(acidName, hydroxideName, "Result", ref mixButton);
    }

    public void PressedAcid()
    {
        acidCheck = true;
        acidName = this.button.name;
        if (acidName != null)
            filledAcid = true;
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

    private void GettingResult(string acid, string hydroxide, string gameObj, ref bool check)
    {
        if (check && filledAcid && filledHydroxide)
        {
            string res;
            res = CountingResult(acid, hydroxide);

            if (pattern.impossibleRes.Contains(res) || res == "X")
            {
                GameObject.Find(gameObj).GetComponentInChildren<Text>().text = "Impossible reaction";
                GameObject.Find("Definition").GetComponent<Text>().text = "";
                pattern.SettingActive(solidAcid, liquidAcid, false);
                pattern.SettingActive(solidHydroxide, liquidHydroxide, false);
                pattern.SettingActive(solidRes, liquidRes, false);
            }
            else
            {
                liquidRes.SetActive(true);
                liquidRes.GetComponent<Renderer>().material.color = colorless;
                DefiningColor(res, liquidRes, solidRes);
                GameObject.Find(gameObj).GetComponentInChildren<Text>().text = res + " + H2O";
            }
            check = false;
        }
        else
            check = false;
    }


    private void DefiningColor(string substance, GameObject liquid, GameObject solid)
    {
        FillingSolidColor(liquid, solid, substance, pattern.whiteSolid, "white", Color.white);
        FillingSolidColor(liquid, solid, substance, pattern.pinkSolid, "pink", Color.magenta);
        FillingSolidColor(liquid, solid, substance, pattern.brownSolid, "brown", brownColor);
        FillingSolidColor(liquid, solid, substance, pattern.redSolid, "red", Color.red);
        FillingSolidColor(liquid, solid, substance, pattern.blackSolid, "black", Color.black);
        FillingSolidColor(liquid, solid, substance, pattern.yellowSolid, "yellow", Color.yellow);
        FillingSolidColor(liquid, solid, substance, pattern.greenSolid, "green", Color.green);
        FillingSolidColor(liquid, solid, substance, pattern.blueSolid, "blue", Color.blue);

        FillingLiquidColor(liquid, solid, substance, pattern.whiteLiquid, "white", Color.white);
        FillingLiquidColor(liquid, solid, substance, pattern.pinkLiquid, "pink", Color.magenta);
        FillingLiquidColor(liquid, solid, substance, pattern.brownLiquid, "brown", brownColor);
        FillingLiquidColor(liquid, solid, substance, pattern.redLiquid, "red", Color.red);
        FillingLiquidColor(liquid, solid, substance, pattern.blackLiquid, "black", Color.black);
        FillingLiquidColor(liquid, solid, substance, pattern.yellowLiquid, "yellow", Color.yellow);
        FillingLiquidColor(liquid, solid, substance, pattern.greenLiquid, "green", Color.green);
        FillingLiquidColor(liquid, solid, substance, pattern.blueLiquid, "blue", Color.blue);
    }

    private void FillingLiquidColor(GameObject liquid, GameObject solid, string substance, List<string> list, string strColor, Color color)
    {
        if (list.Contains(substance))
        {
            if (!liquid.activeSelf)
                liquid.SetActive(true);
            solid.SetActive(false);
            liquid.GetComponent<Renderer>().material.color = color;
            if (liquid == liquidRes)
                GameObject.Find("Definition").GetComponent<Text>().text = $"{strColor} liquid";
        }
    }

    private void FillingSolidColor(GameObject liquid, GameObject solid, string substance, List<string> list, string strColor, Color color)
    {
    if (list.Contains(substance))
        {
            solid.SetActive(true);
            solid.GetComponent<Renderer>().material.color = color;
            
            if (liquid != liquidRes)
                liquid.SetActive(false);
            else
            GameObject.Find("Definition").GetComponent<Text>().text = $"{strColor} sediment";
        }
    }

    private string CountingResult(string acid, string hydroxide)
    {
        string metalIon, acidReduceIon;
        if (hydroxide.Contains("Fe"))
        {
            if (hydroxide == "Fe(OH)2")
            {
                metalIon = "Fe";
                acidReduceIon = pattern.TransformToAcidReduceIon(acid);
                if (oxidationDegree1.Contains(acidReduceIon))
                {
                    if (acidReduceIon == "I" || acidReduceIon == "Cl" || acidReduceIon == "Br" || acidReduceIon == "F")
                        return metalIon + acidReduceIon + "2";
                    else
                        return metalIon + "(" + acidReduceIon + ")2";
                }
                if (oxidationDegree2.Contains(acidReduceIon))
                    return metalIon + acidReduceIon;
                if (oxidationDegree3.Contains(acidReduceIon))
                {
                    if (acidReduceIon == "I" || acidReduceIon == "Cl" || acidReduceIon == "Br" || acidReduceIon == "F" || acidReduceIon == "S")
                        return metalIon + "3" + acidReduceIon + "2";
                    else
                        return metalIon + "3(" + acidReduceIon + ")2";
                }

            }

            if (hydroxide == "Fe(OH)3")
            {
                metalIon = "Fe";
                acidReduceIon = pattern.TransformToAcidReduceIon(acid);
                if (oxidationDegree1.Contains(acidReduceIon))
                {
                    if (acidReduceIon == "I" || acidReduceIon == "Cl" || acidReduceIon == "Br" || acidReduceIon == "F")
                        return metalIon + acidReduceIon + "3";
                    else
                        return metalIon + "(" + acidReduceIon + ")3";
                }
                if (oxidationDegree2.Contains(acidReduceIon))
                {
                    if (acidReduceIon == "I" || acidReduceIon == "Cl" || acidReduceIon == "Br" || acidReduceIon == "F" || acidReduceIon == "S")
                        return metalIon + "2" + acidReduceIon + "3";
                    else
                        return metalIon + "2(" + acidReduceIon + ")3";
                }
                if (oxidationDegree3.Contains(acidReduceIon))
                    return metalIon + acidReduceIon;
            }
            return "X";
        }
        else
        {
            metalIon = pattern.TransformHydroxideToMetalIon(hydroxide);
            acidReduceIon = pattern.TransformToAcidReduceIon(acid);
            return GettingSubstance(metalIon, acidReduceIon);
        }
    }

    private string GettingSubstance(string metalIon, string acidIon)
    {
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
