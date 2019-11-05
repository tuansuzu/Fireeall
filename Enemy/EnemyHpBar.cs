using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{

    public Slider hpSlider;
    public Slider backHpSlider;
    public bool backHpHit = false;

    public Transform enemy;
    public float maxHp = 1000f;
    public float currentHp = 1000f;
    public Vector3 hpBarOffset = new Vector3(0f,1.8f,0f);
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.position + hpBarOffset;
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp /maxHp, Time.deltaTime * 5f);

        if (backHpHit)
        {
            backHpSlider.value = Mathf.Lerp(backHpSlider.value, hpSlider.value, Time.deltaTime * 10f);

            if (hpSlider.value >= backHpSlider.value - 0.01f)
            {
                backHpHit = false;
                backHpSlider.value = hpSlider.value;
            }
                 
        }
    }

   public void Dmg()
    {
        Debug.Log("Enemy Damaged!");
        currentHp -= 300f;
        Invoke("BackHpFun", 0.5f);
    }

    void BackHpFun()
    {
        backHpHit = true;
    }
         
}
