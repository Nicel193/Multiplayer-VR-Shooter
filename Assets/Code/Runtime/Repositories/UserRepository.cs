using Code.Runtime.Logic;

namespace Code.Runtime.Repositories
{
    public class UserRepository
    {
        public string Nickname { get; private set; } = "Test";

        private readonly NicknameGenerator _nicknameGenerator = new NicknameGenerator();

        public void GenerateNickname()
        {
            Nickname = _nicknameGenerator.GenerateNickname();
        }

        public void SetNickname(string nickname)
        {
            Nickname = nickname;
        }
    }
}