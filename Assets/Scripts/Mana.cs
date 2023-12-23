using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public Image currentMana;
    public Text ratioText;

    private float mana = 100;
    private float maxMana = 100;

    // Start is called before the first frame update
    private void Start()
    {
        UpdateMana();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(IncreasesManaOverTime());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseMana(10); // Adjust the healing amount as needed
        }
    }


    private void UpdateMana()
    {
        float ratio = mana / maxMana;
        currentMana.rectTransform.localScale = new Vector3(ratio,1,1);
        ratioText.text = (ratio * 100).ToString("0") + '%';
    }

private IEnumerator IncreasesManaOverTime()
{
    while (Input.GetKey(KeyCode.Q))
    {
        mana += Time.deltaTime * 50;
        if (mana > maxMana)
        mana = maxMana;
        UpdateMana();
        yield return new WaitForSeconds(0.1f); // Adjust delay between damage ticks
    }
}

    private void UseMana(float refill)
    {
        mana -= refill;
        if (mana < 0)
        {
            mana = 0;
            Debug.Log("Dead!");
        }
        UpdateMana();
    }
}
