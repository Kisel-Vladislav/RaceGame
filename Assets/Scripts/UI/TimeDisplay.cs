using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _time;

        public string Title { set => _title.text = value; }
        public string Time { set => _time.text = value; }
    }
}