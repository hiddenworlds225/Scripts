using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class HudController : MonoBehaviour
    {
        public Canvas hud;

        public PlayerHealth health;
        public WeaponData weaponData;
        public WeaponFunctions weaponFunctions;

        public int ammoHold;
        public int currentAmmo;
        public int index;

        public TMP_Text interact;
        public TMP_Text ammoCount;
        public Slider healthSlider;
        public Slider shieldSlider;

        private Transform _hud1;

        // Start is called before the first frame update
        void Start()
        {
            weaponFunctions = transform.GetChild(0).GetChild(0).GetComponent<WeaponFunctions>();

            _hud1 = hud.transform.GetChild(0);
            health = GetComponent<PlayerHealth>();
            interact = _hud1.transform.Find("InteractText").GetComponent<TMP_Text>();
            ammoCount = _hud1.transform.Find("AmmoText").GetComponent<TMP_Text>();
            healthSlider = _hud1.transform.Find("HealthSlider").GetComponent<Slider>();
            shieldSlider = _hud1.transform.Find("ShieldSlider").GetComponent<Slider>();
        }

        // Update is called once per frame
        public void Update()
        {
            InteractionText();
            AmmoText();
            HealthText();
        }

        private void AmmoText()
        {
            if (weaponData == null)
            {
                ammoCount.text = "";
                return;
            }


            ammoCount.text = !weaponFunctions.noWeapons ? $"{currentAmmo}/{ammoHold} <sprite=\"AmmoSpriteList\" index={index}>" : "";
        }

        private void InteractionText()
        {
            interact.text = "";
        }

        private void HealthText()
        {
            healthSlider.value = health.health / 100;
            shieldSlider.value = health.shield / 100;
        }
    }
}