using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScriptMercury : MonoBehaviour
{
    public TextMeshProUGUI dialougueText;
    public string[] lines;
    public float textSpeed = 0.01f;
    int index;

    public RaycastGun showText; // Mostrar operacion

    // Start is called before the first frame update
    void Start()
    {
        showText = FindObjectOfType<RaycastGun>();
        lines = new string[] { "Bienvenido al Nivel 3. (Presiona L para continuar)",
            "Para este nivel aparecio un Monstruo que esta \" hambriento de fracciones, y tienes que darle de comer.",
             "Tienes que completar la operacion correspondiente \" que aparece abajo de la pantalla de modo que el \" resultado de la fracción es el que el monstruo exige",
            "Para agarrar un bloque de fración puedes disparar un rayo y mantenerlo con (Click izquierdo).",
            "Puedes moverte con: a, w, s, d, z, space, q, e.",
            "Tienes un limite del tiempo para completar las operaciones."};
        dialougueText.text = string.Empty;
        startDialougue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
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
