using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image HealthBar;

    public float HealthAmount;

    private void Update()
    {
        HealthBar.fillAmount = HealthAmount / 100;
    }
}
