using System;
using Assets.Util;
using UnityEngine;

namespace Assets.Player.Skills
{
    public class PlayerSkill : MonoBehaviour
    {
        public int IdSkill;
        public EnumElementType Element;
        public float Damage;
        public int Level;
        public float CalculatorNumber;
        public float Duration;
        public float Speed;
        public int ManaCost;
        public int LifeCost;
        public float Interval;

        public GameObject Player;
        private Vector3 _Direction;
        public void UpdateLevel()
        {
            Level += 1;
            Damage = CalculatorNumber * Level;
            name = "skill-" + Element.ToString() + "-Damage_" + Damage.ToString("R");
        }

        // Use this for initialization
        void Start ()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            _Direction = new Vector3(
                Math.Round(Player.transform.rotation.eulerAngles.z) == 0 ? 1 : Math.Round(Player.transform.rotation.eulerAngles.z) == 180 ? -1 : 0,
                Math.Round(Player.transform.rotation.eulerAngles.z) == 90 ? 1 : Math.Round(Player.transform.rotation.eulerAngles.z) == 270 ? -1 : 0,
                0
            );
            UpdateLevel();
        }
	
        // Update is called once per frame
        void Update () {
            transform.position += (_Direction * Speed) * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player") return;
            GameObject.Destroy(gameObject);
        }
    }
}
