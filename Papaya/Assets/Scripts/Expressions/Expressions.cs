using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Expression
{
    public string name;
    public Sprite image; 
}

[System.Serializable]
public class CharacterExpression
{
    public string characterName;
    public Image uiImageToChange;
    public Expression[] expressions;

    public void SetExpression(string expressionname)
    {
        if (uiImageToChange == null)
        {
            return;
        }
        Expression e = null;
        foreach (Expression expression in expressions)
        {
            if (expression.name == expressionname)
            {
                e = expression;
                break;
            }
        }
        if (e != null)
        {
            uiImageToChange.sprite = e.image;
        }
    }
}

public class Expressions : MonoBehaviour
{
    public CharacterExpression[] characterExpressions;
    
    public void SetCharacterExpression(string charname, string expressionname)
    {
        foreach (CharacterExpression ce  in characterExpressions)
        {
            if (ce.characterName == charname)
            {
                ce.SetExpression(expressionname);
                break;
            }
        }
    }
}
