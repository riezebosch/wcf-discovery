using DiscoveryDemo.Contract;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DiscoveryDemo.Service
{
    public class MagicOracleService : IMagicOracle
    {
        static string[] _answers = {
            "Het is tijd",
            "Je mag naar huis",
            "Kruisboog 42",
            "Git is the best",
            "TDD forever",
            "There is no design like test driven design"
        };

        static Random rand = new Random();

        public string Answer(string question)
        {
            return _answers[rand.Next(_answers.Length - 1)];
        }
    }
}