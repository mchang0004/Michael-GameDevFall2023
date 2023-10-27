using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    #region References
    public GameManager gameManager;

    #endregion

    public List<Effect> currentEffects;

    public List<Effect> effectDatabase;


    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        /*foreach(Effect effect in currentEffects)
        {
            Debug.Log("Test");
        }*/
    }


    public void applyEffectsFromCard(Card card)
    {
        float duration = card.effectDuration;

        if(card.effects.Count > 0)
        {
            foreach (Effect effect in card.effects)
            {
                Debug.Log("Test 1");
                givePlayerEffects(effect, duration);
            }
        } else
        {
            Debug.Log("Card has No Effects");
        }
       

    }

    private void givePlayerEffects(Effect effect, float duration)
    {
        if(currentEffects != null && currentEffects.Contains(effect))
        {
            currentEffects.Remove(effect);
            currentEffects.Add(effect);
            Debug.Log("Effect: " + effect.name + " Was Added");

            StartCoroutine(removeEffect(effect, duration));

        }
        else
        {
            currentEffects.Add(effect);
            Debug.Log("Effect: " + effect.name + " Was Added");


            StartCoroutine(removeEffect(effect, duration));


        }



    }

 
    IEnumerator removeEffect(Effect effect, float duration)
    {
        if (currentEffects != null)
        {
            yield return new WaitForSeconds(duration);
            currentEffects.Remove(effect);
            Debug.Log("Effect: " + effect.name + " Was Removed");

        }

    }

}
