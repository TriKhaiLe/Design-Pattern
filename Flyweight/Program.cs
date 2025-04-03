namespace Flyweight
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public string Name { get; private set; }
        public string Logo { get; private set; }
        public string Color { get; private set; }

        public Team(string name, string logo, string color)
        {
            Name = name;
            Logo = logo;
            Color = color;
        }
    }

    public class PlayerFactory
    {
        private static Dictionary<string, Team> _teams = [];

        public static Team GetTeam(string name, string logo, string color)
        {
            string key = name; 
            if (!_teams.ContainsKey(key))
            {
                _teams[key] = new Team(name, logo, color);
                Console.WriteLine($"Created new team: {name}");
            }
            return _teams[key];
        }

        public static int GetTeamCount()
        {
            return _teams.Count;
        }
    }

    public class Player
    {
        private string _name;
        private int _jerseyNumber;
        private string _position;
        private Team _team;

        public Player(string name, int jerseyNumber, string position, string teamName, string teamLogo, string teamColor)
        {
            _name = name;
            _jerseyNumber = jerseyNumber;
            _position = position;
            _team = PlayerFactory.GetTeam(teamName, teamLogo, teamColor);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Player: {_name}, Jersey: {_jerseyNumber}, Position: {_position}");
            Console.WriteLine($"Team: {_team.Name}, Logo: {_team.Logo}, Color: {_team.Color}");
            Console.WriteLine("-------------------");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new("Elonmusk", 10, "Attacker", "Barcelona", "BarcaLogo.png", "Red-Blue");
            Player player2 = new("Billgates", 8, "Defender", "Barcelona", "BarcaLogo.png", "Red-Blue");
            Player player3 = new("Timcook", 9, "Attacker", "Real Madrid", "RealLogo.png", "White");

            player1.DisplayInfo();
            player2.DisplayInfo();
            player3.DisplayInfo();

            Console.WriteLine($"Total unique teams created: {PlayerFactory.GetTeamCount()}");
        }
    }
}