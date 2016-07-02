using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TraitHUB : MonoBehaviour
{

    public string traitDescription;
    public GameObject toolTip;

    void OnMouseEnter()
    {
        toolTip.SetActive(true);
        toolTip.GetComponent<Tooltip>().textContainer.GetComponent<Text>().text = traitDescription;
        toolTip.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
    }

    void OnMouseExit()
    {
        toolTip.SetActive(false);
        toolTip.GetComponent<Tooltip>().textContainer.GetComponent<Text>().text ="";
    }

}
