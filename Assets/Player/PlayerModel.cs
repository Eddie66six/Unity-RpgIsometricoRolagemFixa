using System.Collections.Generic;
using System.Linq;
using Assets.Player.Skills;
using UnityEngine;

namespace Assets.Player
{
    public enum PlayerType
    {
        Mage = 0
    }

    public enum PlayerRace
    {
        Human = 0
    }
    public sealed class PlayerModel : MonoBehaviour
    {
        public PlayerModel(string name, PlayerType playerType, GameObject player)
        {
            PlayerType = playerType;
            Attributes = new PlayerAttribute(playerType);
            AttributePoints = 1;
            SkillPoints = 1;
            Name = name;
            Level = 1;
            CurrentExperience = 0;
            NextExperience = 100;
            Skills = new List<GameObject>();
            _Player = player;
        }

        //controls
        private float _TimeCurrentSkill { get; set; }
        private GameObject _Player { get; set; }

        public PlayerType PlayerType { get; private set; }
        public string Name { get; private set; }
        public float CurrentExperience { get; private set; }
        public float NextExperience { get; private set; }
        public int Level { get; private set; }
        public PlayerAttribute Attributes { get; private set; }
        public int AttributePoints { get; private set; }
        public int SkillPoints { get; private set; }
        public List<GameObject> Skills { get; private set; }

        public void AddUpdateSkill(GameObject skill)
        {
            if (SkillPoints <= 0) return;

            SkillPoints -= 1;

            var id = skill.GetComponent<PlayerSkill>().IdSkill;
            if (Skills.All(p => p.GetComponent<PlayerSkill>().IdSkill != id))
                Skills.Add(skill);
            else
            {
                foreach (var item in Skills)
                {
                    if (item.GetComponent<PlayerSkill>().IdSkill == id)
                    {
                        item.GetComponent<PlayerSkill>().UpdateLevel();
                        break;
                    }
                }
            }
        }

        public void AddExperience(float experience)
        {
            _UpdateLevel(experience);
        }

        private void _UpdateLevel(float experience)
        {
            if (CurrentExperience + experience >= NextExperience)
            {
                AttributePoints += 1;
                SkillPoints += 1;
                Level += 1;
                CurrentExperience = NextExperience;
                NextExperience *= 2;
                Attributes.Update(true);
                var tmpRemnant = CurrentExperience + experience - NextExperience;
                if (tmpRemnant > 0)
                    _UpdateLevel(tmpRemnant);
            }
            else
                CurrentExperience += experience;
        }

        public void AddAttributes(int strength = 0, int dexterity = 0, int intelligence = 0)
        {
            var total = strength + dexterity + intelligence;
            if (total <= 0 || total > AttributePoints) return;
            AttributePoints -= total;
            Attributes.Add(strength, dexterity, intelligence);
        }

        public void UseSkill(int idSkill)
        {
            var skill = Skills.FirstOrDefault(p => p.GetComponent<PlayerSkill>().IdSkill == idSkill);
            if (skill == null || Time.time < _TimeCurrentSkill) return;
            if (Attributes.UseManaLife(skill.GetComponent<PlayerSkill>().ManaCost, skill.GetComponent<PlayerSkill>().LifeCost))
            {
                _TimeCurrentSkill = Time.time + Attributes.SkillSpeed;
                Object.Instantiate(skill, _Player.transform.position, _Player.transform.rotation);
            }
        }
    }
}