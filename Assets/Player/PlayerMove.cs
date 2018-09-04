using Assets.Player.Skills;
using UnityEngine;

namespace Assets.Player
{
    public class PlayerMove : MonoBehaviour
    {
        //event
        public delegate void UpdateDisplay(PlayerModel playerScript);
        public static event UpdateDisplay OnUpdate;

        private PlayerModel _player;

        // Use this for initialization
        void Start()
        {
            _player = new PlayerModel("EddieMage", PlayerType.Mage, this.gameObject);
            UpdateStatus();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
                MoveUp();
            if (Input.GetKey(KeyCode.A))
                MoveLeft();
            if (Input.GetKey(KeyCode.S))
                MoveDown();
            if (Input.GetKey(KeyCode.D))
                MoveRight();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _player.AddExperience(10.0f);
                UpdateStatus();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _player.AddAttributes(0, 0, 1);
                UpdateStatus();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _player.AddAttributes(0, 1, 0);
                UpdateStatus();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _player.AddAttributes(1, 0, 0);
                UpdateStatus();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                var skills = GetAllSkills();
                _player.AddUpdateSkill(skills[0]);
                _player.UseSkill(skills[0].GetComponent<PlayerSkill>().IdSkill);
                UpdateStatus();
            }
        }

        void UpdateStatus()
        {
            OnUpdate?.Invoke(_player);
        }

        private void MoveUp()
        {
            transform.eulerAngles = new Vector3(0,0,90);
            transform.position += (Vector3.up * _player.Attributes.MovementSpeed) * Time.deltaTime;
        }
        private void MoveDown()
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
            transform.position += (Vector3.down * _player.Attributes.MovementSpeed) * Time.deltaTime;
        }
        private void MoveLeft()
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            transform.position += (Vector3.left * _player.Attributes.MovementSpeed) * Time.deltaTime;
        }
        private void MoveRight()
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position += (Vector3.right * _player.Attributes.MovementSpeed) * Time.deltaTime;
        }
        private void UseSkill()
        {
            transform.position += (Vector3.up * _player.Attributes.MovementSpeed) * Time.deltaTime;
        }

        private GameObject[] GetAllSkills()
        {
            return Resources.LoadAll<GameObject>("Skills");
        }
    }
}
