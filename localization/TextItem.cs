using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextItem : MonoBehaviour
{
  public string language;
  Text text;

  public string textRus;
  public string textEng;

  void Start()
  {
      text = GetComponent<Text>();
      language = PlayerPrefs.GetString("Language");

      if (language == "" || language == "Rus")
      {
        text.text = textRus;
      }

      else if (language == "Eng")
      {
        text.text = textEng;
      }

  }
}
