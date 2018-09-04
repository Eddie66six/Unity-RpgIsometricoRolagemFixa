using Assets.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class Game : MonoBehaviour {
        public GameObject Player;
        public Text AttrPoints;
        public Text Experience;
        public Text Level;
        public Text Life;
        public Text Mana;
        public Text skillPoints;
        // Use this for initialization
        void Awake()
        {
            PlayerMove.OnUpdate += _UpdateDisplay;
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        void _UpdateDisplay(PlayerModel playerScript)
        {
            AttrPoints.text = "Pontos de atributos: " + playerScript.AttributePoints;
            skillPoints.text = "Pontos de skill: " + playerScript.SkillPoints;
            Experience.text = "XP: " + playerScript.CurrentExperience + "/" + playerScript.NextExperience;
            Level.text = "LVL: " + playerScript.Level;
            Life.text = "Life: " + playerScript.Attributes.CurrentLife + "/" + playerScript.Attributes.TotalLife;
            Mana.text = "Mana: " + playerScript.Attributes.CurrentMana + "/" + playerScript.Attributes.TotalMana;
        }
    }
}
