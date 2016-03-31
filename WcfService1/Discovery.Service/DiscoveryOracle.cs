using System;
using Discovery.Contracts;
using System.Linq;

namespace Discovery.Service
{
    public class DiscoveryOracle : IDiscoveryOracle
    {
        static Random random = new Random();

        public string Ask(string question)
        {
            string[] answers =
            {
                "Je mag naar huis",
                "Het is te duur",
                "Tijd voor cup-a-soup",
                "Tijd voor koffie",
                "Ik zou voor een Apple gaan"
            };

            return answers.Skip(random.Next(answers.Length - 1)).First();
        }
    }
}