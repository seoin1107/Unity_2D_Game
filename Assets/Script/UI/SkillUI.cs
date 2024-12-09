using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : UIManager
{

    public Image skillImage;
    public Image dodgeImage;
    public Image parringImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //skillImage.fillAmount = player.characterStat.skillCool / player.characterStat.skillCool;
        dodgeImage.fillAmount = player.characterStat.curDodgeCool / player.characterStat.dodgeCool;
        parringImage.fillAmount = player.characterStat.curParryingCool / player.characterStat.parryingCool;
    }
}
