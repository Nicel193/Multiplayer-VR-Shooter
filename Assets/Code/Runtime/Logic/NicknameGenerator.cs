using UnityEngine;

namespace Code.Runtime.Logic
{
    public class NicknameGenerator
    {
        private string[] Prefixes = {"Cool", "Awesome", "Fantastic", "Mighty", "Super", "Epic", "Legendary"};
        private string[] Suffixes = {"Player", "Gamer", "Ninja", "Master", "Champion", "Warrior", "Legend"};

        public string GenerateNickname()
        {
            string prefix = Prefixes[Random.Range(0, Prefixes.Length)];
            string suffix = Suffixes[Random.Range(0, Suffixes.Length)];
            return $"{prefix}{suffix}";
        }
    }
}