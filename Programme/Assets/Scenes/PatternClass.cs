using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternClass
{
    public readonly List<string> whiteSolid = new List<string>() { "BaSO4", "BaSO3", "CaSO3", "MnSO3", "Li3PO4",
    "Ba3(PO4)2", "Zn3(PO4)2", "AlPO4", "Fe3(PO4)2", "CaS", "ZnS", "CaCO3", "FeCO3", "Ca3(PO4)2", "Mg3(PO4)2",
    "Mn3(PO4)2", "CaF2", "MgF2", "Li2SiO3", "BaSiO3", "CaSiO3", "MgSiO3", "ZnSiO3", "BaCO3", "ZnCO3", "H2SiO3", "Mg(OH)2", "Al(OH)3", "BaSO4", "BaSO3",
    "CaSO3", "MnSO3", "Li3PO4", "Ba3(PO4)2", "Zn3(PO4)2", "AlPO4", "Fe3(PO4)2", "CaS", "ZnS", "CaCO3", "FeCO3",
    "Zn(OH)2", "Ca3(PO4)2", "Mg3(PO4)2", "Mn3(PO4)2", "CaF2", "MgF2", "Li2SiO3", "BaSiO3", "CaSiO3", "MgSiO3",
    "ZnSiO3", "BaCO3", "ZnCO3", "BaSO4", "BaSO3", "CaSO3", "MnSO3",
    "CaCO3", "FeCO3", "Li2SiO3", "BaSiO3", "CaSiO3", "MgSiO3", "ZnSiO3", "BaCO3", "ZnCO3", "Ca(PO3)2",
    "Mg(PO3)2", "Zn(PO3)2", "Mn(PO3)2", "Cu(PO3)2", "Al(PO3)3", "Fe(PO3)2", "Fe(PO3)3", "MnCrO4", "Fe2(CrO4)3", "BaSO4", "PbSO4"  };
    public readonly List<string> blackSolid = new List<string>() { "CuS", "FeS", "FeSiO3" };
    public readonly List<string> blueSolid = new List<string>() { "Cu3(PO4)2", "CuCO3", "Cu(OH)2" };
    public readonly List<string> pinkSolid = new List<string>() { "Mn(OH)2", "MnSiO3", "MnCO3" };
    public readonly List<string> greenSolid = new List<string>() { "CuSiO3", "Fe(OH)2", "CuSiO3" };
    public readonly List<string> brownSolid = new List<string>() { "CrPO4", "Fe(OH)3", "MnS" };
    public readonly List<string> redSolid = new List<string>() { "CuSO3" };
    public readonly List<string> yellowSolid = new List<string>() { "FePO4", "BaCrO4" };

    public readonly List<string> whiteLiquid = new List<string>() { "BaI2", "CuI2", "BaBr2", "LiCl", "NaCl", "CaCl2", "ZnCl2", "FeCl2",
        "MgSO4", "MnSO4", "Ca(NO3)2", "Li2SO3", "ZnSO3", "Na3PO4", "LiF", "NaF", "BaF2", "ZnF2", "AlF3", "FeF2", "Na2S", "Na2SiO3", "Na2CO3",
        "MgCO3", "LiI", "NaI", "CaI2", "MgI2", "Na2SO3", "MgSO3", "ZnI2", "AlI3", "LiBr", "NaBr", "CaBr2", "MgBr2", "ZnBr2", "AlBr3", "BaCl2",
        "MgCl2", "Li2SO4", "Na2SO4", "CaSO4", "ZnSO4", "Al2(SO4)3", "FeSO4", "LiNO3", "NaNO3", "Ba(NO3)2", "Mg(NO3)2", "Zn(NO3)2", "Al(NO3)3",
        "BaS", "Li2CO3", "NaOH", "BaI2", "CuI2", "BaBr2", "LiCl", "NaCl", "KCl", "CaCl2", "ZnCl2", "FeCl2", "MgSO4", "MnSO4", "Ca(NO3)2", "K2SO3",
        "Li2SO3", "ZnSO3", "Na3PO4", "LiF", "NaF", "KF", "ZnF2", "AlF3", "FeF2", "Na2S", "Na2SiO3", "K2CO3", "Na2CO3", "MgCO3", "HI", "HBr",
        "HCl", "H2SO4", "HNO3", "H2SO3", "H3PO4", "HF", "H2S", "H2CO3", "LiOH", "KOH", "Ba(OH)2", "Ca(OH)2", "LiI", "NaI", "KI", "CaI2", "MgI2", "ZnI2",
        "AlI3", "LiBr", "NaBr", "KBr", "CaBr2", "MgBr2", "ZnBr2", "AlBr3", "BaCl2", "MgCl2", "Li2SO4", "Na2SO4", "K2SO4", "CaSO4", "ZnSO4", "Al2(SO4)3",
    "FeSO4", "LiNO3", "NaNO3", "KNO3", "Ba(NO3)2", "Mg(NO3)2", "Zn(NO3)2", "Al(NO3)3", "Fe(NO3)3", "Na2SO3", "MgSO3", "K3PO4", "K2S", "BaS", "K2SiO3", "Li2CO3",
    "NaPO3", "KPO3", "MgSO4", "MnSO4", "Ca(NO3)2", "K2SO3", "Li2SO3", "ZnSO3", "K2CO3", "Na2SiO3", "Na2CO3", "MgCO3", "Li2SO4", "Na2SO4", "K2SO4", "Fe(MnO4)3", "CuCrO4",
    "CaSO4", "ZnSO4", "Al2(SO4)3", "FeSO4", "LiNO3", "NaNO3", "KNO3", "Ba(NO3)2", "Mg(NO3)2", "Zn(NO3)2", "Al(NO3)3",
    "Fe(NO3)3", "Li2CO3", "Na2SO3", "MgSO3", "K2SiO3", "NaClO4", "LiClO4", "KClO4", "Ba(ClO4)2", "Ca(ClO4)2", "Mg(ClO4)2",
    "Al(ClO4)3", "Zn(ClO4)2", "Mn(ClO4)2", "Cu(ClO4)2", "Fe(ClO4)3", "Al(MnO4)3", "Zn(MnO4)2", "Cu(MnO4)2", "Fe(MnO4)2", "LiI", "MgI2", "NaI", "CaI2", "ZnI2", "AlI3",
    "BaI2", "LiBr", "MgBr2", "NaBr", "CaBr2", "ZnBr2", "AlBr3", "BaBr2", "LiCl", "MgCl2", "FeCl2", "NaCl", "CaCl2",
    "ZnCl2", "BaCl2", "Na2SO3", "MgSO3", "BaS", "Li2CO3", "LiF", "NaF", "ZnF2", "AlF3", "FeF2", "Na2S",
    "Na2CO3", "MgCO3", "Na3PO4", "Li2SO3", "ZnSO3", "K2CO3", "K2SO3", "KCl", "KI", "KBr", "K3PO4", "K2S", "KF",
    "PbCl2", "PbBr2", "Ca(NO3)2", "MgSO4", "Li2SO4", "Na2SO4", "K2SO4", "ZnSO4", "Al2(SO4)3", "FeSO4", "LiNO3",
    "NaNO3", "KNO3", "Ba(NO3)2", "Cu(NO3)2", "Mg(NO3)2","Zn(NO3)2", "Al(NO3)3", "CaSO4", "Fe(NO3)2", "Pb(NO3)2",
    "CuSO4", "HgSO4", "Hg(NO3)2"};
    public readonly List<string> pinkLiquid = new List<string>() { "MnI2", "MnBr2", "MnCl2", "Cr2(SO4)3", "MnI2", "MnBr2", "MnCl2", "LiMnO4", "NaMnO4", "KMnO4", "Mg(MnO4)2", "Ba(MnO4)2", "Ca(MnO4)2" };
    public readonly List<string> brownLiquid = new List<string>() { "Mn(NO3)2", "MnF2", "FeI2", "FeCl3", "Li2S" };
    public readonly List<string> blackLiquid = new List<string>() { "CuBr2", "Mn(NO3)2" };
    public readonly List<string> yellowLiquid = new List<string>() { "FeBr2", "AlCl3", "AlCl3", "Fe(SO4)3", "Fe2(SO4)3", "Li2CrO4", "K2CrO4", "Na2CrO4", "MgCrO4", "CaCrO4", "ZnCrO4" };
    public readonly List<string> greenLiquid = new List<string>() { "CrBr3", "CuCl2", "Fe(NO3)2", "FeSO3", "CrF3", "Cr(NO3)2", "CuCl2", "Fe(NO3)2", "FeSO3", "FeF3", "FeSO3", "Fe(ClO4)2" };
    public readonly List<string> blueLiquid = new List<string>() { "CuSO4", "Cu(NO3)2", "CuF2", "CrI3", "CrCl3", "CuSO4", "Cu(NO3)2", "CuF2", "CuSO4", "Cu(NO3)2" };
    public readonly List<string> redLiquid = new List<string>() { "FeBr3" };

    public readonly List<string> impossibleRes = new List<string>() { "Al2(SO3)3", "MgS", "Al2S3", "Cr2(SO3)3",
        "Cr2S3", "Al2(SiO3)3", "Al2(CO3)3", "Cr2(SiO3)3", "Cr2(CO3)3", "LiPO3", "Al2(CrO4)3", "FeCrO4",
        "Mn(MnO4)2", "FeI3", "Al2(SO3)3", "Fe2(SO3)3", "Fe2S3", "Al2S3", "Al2(SiO3)3", "Fe2(SiO3)3", "Fe2(CO3)3" };


    public void AddingReagent(string elementName, string gameObj, bool reactant)
    {
        if (reactant)
        {
            GameObject.Find($"{gameObj}").GetComponentInChildren<Text>().text = elementName;
            reactant = false;
        }
    }

    public string TransformHydroxideToMetalIon(string elem)
    {
        if (elem == "KOH")
            return elem.Substring(0, 1);
        else
            return elem.Substring(0, 2);
    }

    public string TransformToAcidReduceIon(string elem)
    {
        string ion = elem;
        int length = ion.Length;
        if (elem == "HI" || elem == "HBr" || elem == "HCl" || elem == "HF" || elem == "HNO3")
            return ion.Remove(0, 1);
        else
            return ion.Remove(0, 2);
    }

    public string TransFormOxideToMetalIon(string elem)
    {
        return elem.Substring(0, 2);
    }

    public void SettingActive(GameObject go1, GameObject go2, bool active)
    {
        go1.SetActive(active);
        go2.SetActive(active);
    }
}
