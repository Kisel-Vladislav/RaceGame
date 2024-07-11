using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _scope;

        public void Construct(string Name, int Scope)
        {
            _name.text = Name;
            _scope.text = Scope.ToString();
        }
    }
}