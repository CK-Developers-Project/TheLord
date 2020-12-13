namespace Developers.Structure
{

    public enum TransitionType
    {
        Blank,
        Slide,
        Loading01_Blank,
        Loading01_Slide,
    }

    public class SceneName
    {
        public const string Intro = "Intro";
        public const string Login = "Login";
        public const string MainLobby = "MainLobby";
        public const string MainLobby_Elf = "MainLobby_Elf";
        public const string MainLobby_Human = "MainLobby_Human";
        public const string MainLobby_Undead = "MainLobby_Undead";

        public static string GetMainLobby(Race race)
        {
            switch(race)
            {
                case Race.Elf: return MainLobby_Elf;
                case Race.Human: return MainLobby_Human;
                case Race.Undead: return MainLobby_Undead;
                default: return "NULL";
            }
        }
    }
}
