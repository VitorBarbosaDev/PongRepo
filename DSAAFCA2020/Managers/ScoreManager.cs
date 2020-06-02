using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace DSAAFCA2020.Managers
{
    public class ScoreManager
    {
        private static string _fileName = "Scor.xml"; // Since we don't give a path, this'll be saved in the "bin" folder

        public List<GameScores> Highscores { get; private set; }

        public List<GameScores> Scores { get; private set; }

        public ScoreManager()
          : this(new List<GameScores>())
        {

        }

        public ScoreManager(List<GameScores> scores)
        {
            Scores = scores;

            UpdateHighscores();
        }

        public void Add(GameScores score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.Games).ToList(); // Orders the list so that the higher scores are first

            UpdateHighscores();
        }

        public static ScoreManager Load()
        {
            // If there isn't a file to load - create a new instance of "ScoreManager"
            if (!File.Exists(_fileName))
                return new ScoreManager();

            // Otherwise we load the file

            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<GameScores>));

                var scores = (List<GameScores>)serilizer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList(); // Takes the first 5 elements
        }

        public static void Save(ScoreManager scoreManager)
        {
            // Overrides the file if it alreadt exists
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<GameScores>));

                serilizer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}