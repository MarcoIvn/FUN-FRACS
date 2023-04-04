using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialougueText;
    public string[] lines;
    public float textSpeed = 0.1f;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        dialougueText.text = string.Empty;
        startDialougue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialougueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialougueText.text = lines[index];
            }
        }
    }

    public void startDialougue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialougueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialougueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
