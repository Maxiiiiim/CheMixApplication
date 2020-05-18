using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0618 // Тип или член устарел
public class DefiningFlameColor : MonoBehaviour
{
    public GameObject sparks, fire, fireDark;
    public GameObject Na, Ba, Cs, K, Cu, Pb, B, Sr, Ca, Se, Fe, Li, Rb, Zn;
    private static Color startFireDarkColor, startSparkColor, startFireColor;
    private bool MouseDOWN = false;
    static int check = 0;
    private float elemX, elemY, elemZ;
    private float NaX, NaY, NaZ, BaX, BaY, BaZ, CsX, CsY, CsZ, KX, KY, KZ, CuX, CuY, CuZ, PbX, PbY, PbZ,
        BX, BY, BZ, SrX, SrY, SrZ, CaX, CaY, CaZ, SeX, SeY, SeZ, FeX, FeY, FeZ, LiX, LiY, LiZ,
        RbX, RbY, RbZ, ZnX, ZnY, ZnZ;

    // Start is called before the first frame update
    void Start()
    {
        StartSettings();
    }

    void OnMouseDown()
    {
        MouseDOWN = true;

        if (check == 0)
        {
            elemX = this.transform.position.x;
            elemY = this.transform.position.y;
            elemZ = this.transform.position.z;
            check = 1;
        }
        else
            check = 0;
    }

    void OnMouseUp()
    {
        MouseDOWN = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Cursor = Input.mousePosition;
        Cursor = Camera.main.ScreenToWorldPoint(Cursor);
        if (MouseDOWN && check == 1)
        {
            this.transform.position = new Vector3((float)1.824, (float)2.638, (float)-7.555);
            EstablishingColor();
        }

        if (MouseDOWN && check == 0)
        {
            TurningElementsBack();
        }
    }

    private void StartSettings()
    {
        SettingLocation(Na, ref NaX, ref NaY, ref NaZ);
        SettingLocation(Ba, ref BaX, ref BaY, ref BaZ);
        SettingLocation(Cs, ref CsX, ref CsY, ref CsZ);
        SettingLocation(K, ref KX, ref KY, ref KZ);
        SettingLocation(Cu, ref CuX, ref CuY, ref CuZ);
        SettingLocation(Pb, ref PbX, ref PbY, ref PbZ);
        SettingLocation(B, ref BX, ref BY, ref BZ);
        SettingLocation(Sr, ref SrX, ref SrY, ref SrZ);
        SettingLocation(Ca, ref CaX, ref CaY, ref CaZ);
        SettingLocation(Se, ref SeX, ref SeY, ref SeZ);
        SettingLocation(Fe, ref FeX, ref FeY, ref FeZ);
        SettingLocation(Li, ref LiX, ref LiY, ref LiZ);
        SettingLocation(Rb, ref RbX, ref RbY, ref RbZ);
        SettingLocation(Zn, ref ZnX, ref ZnY, ref ZnZ);

        startFireDarkColor = GameObject.Find("FireDark").GetComponentInChildren<ParticleSystem>().startColor;
        startFireColor = GameObject.Find("Fire").GetComponentInChildren<ParticleSystem>().startColor;
        startSparkColor = GameObject.Find("Sparks").GetComponentInChildren<ParticleSystem>().startColor;
    }

    private void TurningElementsBack()
    {
        SettingBackLocation(Na, NaX, NaY, NaZ);
        SettingBackLocation(Ba, BaX, BaY, BaZ);
        SettingBackLocation(Cs, CsX, CsY, CsZ);
        SettingBackLocation(K, KX, KY, KZ);
        SettingBackLocation(Cu, CuX, CuY, CuZ);
        SettingBackLocation(Pb, PbX, PbY, PbZ);
        SettingBackLocation(B, BX, BY, BZ);
        SettingBackLocation(Sr, SrX, SrY, SrZ);
        SettingBackLocation(Ca, CaX, CaY, CaZ);
        SettingBackLocation(Se, SeX, SeY, SeZ);
        SettingBackLocation(Fe, FeX, FeY, FeZ);
        SettingBackLocation(Li, LiX, LiY, LiZ);
        SettingBackLocation(Rb, RbX, RbY, RbZ);
        SettingBackLocation(Zn, ZnX, ZnY, ZnZ);

        GameObject.Find("FireDark").GetComponentInChildren<ParticleSystem>().startColor = startFireDarkColor;
        GameObject.Find("Fire").GetComponentInChildren<ParticleSystem>().startColor = startFireColor;
        GameObject.Find("Sparks").GetComponentInChildren<ParticleSystem>().startColor = startSparkColor;
        GameObject.Find("Result").GetComponent<Text>().text = " ";
    }

    private void EstablishingColor()
    {
        if (ColorCondition(Na, NaX, NaY, NaZ) || ColorCondition(Fe, FeX, FeY, FeZ))
        {
            YellowColorDefinition();
            GameObject.Find("FireDark").GetComponentInChildren<ParticleSystem>().startColor = Color.yellow;
            GameObject.Find("Fire").GetComponentInChildren<ParticleSystem>().startColor = Color.yellow;
            GameObject.Find("Sparks").GetComponentInChildren<ParticleSystem>().startColor = Color.yellow;
        }

        if (ColorCondition(Ca, CaX, CaY, CaZ) || ColorCondition(Li, LiX, LiY, LiZ) || ColorCondition(Sr, SrX, SrY, SrZ))
        {
            RedColorDefinition();
            GameObject.Find("FireDark").GetComponentInChildren<ParticleSystem>().startColor = Color.red;
            GameObject.Find("Fire").GetComponentInChildren<ParticleSystem>().startColor = Color.red;
            GameObject.Find("Sparks").GetComponentInChildren<ParticleSystem>().startColor = Color.red;
        }

        if (ColorCondition(Cs, CsX, CsY, CsZ) || ColorCondition(K, KX, KY, KZ) || ColorCondition(Rb, RbX, RbY, RbZ))
        {
            PurpleColorDefinition();
            GameObject.Find("FireDark").GetComponentInChildren<ParticleSystem>().startColor = Color.magenta;
            GameObject.Find("Fire").GetComponentInChildren<ParticleSystem>().startColor = Color.magenta;
            GameObject.Find("Sparks").GetComponentInChildren<ParticleSystem>().startColor = Color.magenta;

        }

        if (ColorCondition(B, BX, BY, BZ) || ColorCondition(Cu, CuX, CuY, CuZ)
            || ColorCondition(Ba, BaX, BaY, BaZ) || ColorCondition(Zn, ZnX, ZnY, ZnZ))
        {
            GreenColorDefinition();
            GameObject.Find("FireDark").GetComponentInChildren<ParticleSystem>().startColor = Color.green;
            GameObject.Find("Fire").GetComponentInChildren<ParticleSystem>().startColor = Color.green;
            GameObject.Find("Sparks").GetComponentInChildren<ParticleSystem>().startColor = Color.green;
        }

        if (ColorCondition(Se, SeX, SeY, SeZ) || ColorCondition(Pb, PbX, PbY, PbZ))
        {
            BlueColorDefinition();
            GameObject.Find("FireDark").GetComponentInChildren<ParticleSystem>().startColor = Color.blue;
            GameObject.Find("Fire").GetComponentInChildren<ParticleSystem>().startColor = Color.blue;
            GameObject.Find("Sparks").GetComponentInChildren<ParticleSystem>().startColor = Color.blue;
        }
    }

    private void YellowColorDefinition()
    {
        if (ColorCondition(Na, NaX, NaY, NaZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Na - yellow flame";
        if (ColorCondition(Fe, FeX, FeY, FeZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Fe - yellow flame";
    }

    private void RedColorDefinition()
    {
        if (ColorCondition(Ca, CaX, CaY, CaZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Ca - red flame";
        if (ColorCondition(Li, LiX, LiY, LiZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Li - red flame";
        if (ColorCondition(Sr, SrX, SrY, SrZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Sr - red flame";
    }

    private void PurpleColorDefinition()
    {
        if (ColorCondition(Cs, CsX, CsY, CsZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Cs - purple flame";
        if (ColorCondition(K, KX, KY, KZ))
            GameObject.Find("Result").GetComponent<Text>().text = "K - purple flame";
        if (ColorCondition(Rb, RbX, RbY, RbZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Rb - purple flame";
    }

    private void GreenColorDefinition()
    {
        if (ColorCondition(B, BX, BY, BZ))
            GameObject.Find("Result").GetComponent<Text>().text = "B - green flame";
        if (ColorCondition(Cu, CuX, CuY, CuZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Cu - green flame";
        if (ColorCondition(Ba, BaX, BaY, BaZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Ba - green flame";
        if (ColorCondition(Zn, ZnX, ZnY, ZnZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Zn - green flame";
    }

    private void BlueColorDefinition()
    {
        if (ColorCondition(Se, SeX, SeY, SeZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Se - blue flame";
        if (ColorCondition(Pb, PbX, PbY, PbZ))
            GameObject.Find("Result").GetComponent<Text>().text = "Pb - blue flame";
    }

    private bool ColorCondition(GameObject go, float elemX, float elemY, float elemZ)
    {
        return go.transform.position.x != elemX && go.transform.position.y != elemY
         && go.transform.position.z != elemZ;
    }

    private void SettingLocation(GameObject go, ref float elemX, ref float elemY, ref float elemZ)
    {
        elemX = go.transform.position.x;
        elemY = go.transform.position.y;
        elemZ = go.transform.position.z;
    }

    private void SettingBackLocation(GameObject go, float elemX, float elemY, float elemZ)
    {
        go.transform.position = new Vector3(elemX, elemY, elemZ);
    }
#pragma warning restore CS0618 // Тип или член устарел
}
