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
        // Use this for initialization
        void Start ()
        {
            PlayerMove.OnUpdate += _UpdateDisplay;
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        void _UpdateDisplay(PlayerModel playerScript)
        {
            AttrPoints.text = "Pontos de atributos: " + playerScript.AttributePoints;
            Experience.text = "XP: " + playerScript.CurrentExperience + "/" + playerScript.NextExperience;
            Level.text = "LVL: " + playerScript.Level;
            Life.text = "Life: " + playerScript.Attributes.CurrentLife + "/" + playerScript.Attributes.TotalLife;
            Mana.text = "Mana: " + playerScript.Attributes.CurrentMana + "/" + playerScript.Attributes.TotalMana;
        }
    }
}
