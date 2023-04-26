using PANS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PANS.Services
{
    internal class TechDataRepository : ITechDataRepository
    {
        private static readonly Random _random = new Random();
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public List<TechData> GetData()
        {
            var data = new List<TechData>
            {
                new TechData
                {
                    Author = "Zeus", Date = DateTime.Now.AddDays(-1),
                    Data =
                        "JavaScript is a high-level programming language that is one of the core technologies of the World Wide Web. It is used as a client-side programming language by 97.8 percent of all websites. JavaScript was originally used only to develop web browsers, but they are now used for server-side website deployments and non-web browser applications as well. "
                },
                new TechData
                {
                    Author = "Gera", Date = DateTime.Now.AddDays(-22),
                    Data =
                        "Python is one of the most popular programming languages today and is easy for beginners to learn because of its readability. It is a free, open-source programming language with extensive support modules and community development, easy integration with web services, user-friendly data structures, and GUI-based desktop applications. It is a popular programming language for machine learning and deep learning applications. "
                },
                new TechData
                {
                    Author = "Neptun", Date = DateTime.Now.AddDays(-21),
                    Data =
                        "Go was developed by Google in 2007 for APIs and web applications. Go has recently become one of the fastest-growing programming languages due to its simplicity, as well as its ability to handle multicore and networked systems and massive codebases."
                },
                new TechData
                {
                    Author = "Pluto", Date = DateTime.Now.AddDays(-132),
                    Data = "Java is one of the most popular programming languages used today. "
                },
                new TechData
                {
                    Author = "Venus", Date = DateTime.Now.AddDays(-112),
                    Data =
                        "Kotlin is a general-purpose programming language originally developed and unveiled as Project Kotlin by JetBrains in 2011. The first version was officially released in 2016. It is interoperable with Java and supports functional programming languages."
                }
            };
            data.AddRange(AddRandom(25));

            return data;
        }

        private List<TechData> AddRandom(int count)
        {
            var data = new List<TechData>();
            for (int i = 0; i < count; i++)
            {
                data.Add(new TechData
                {
                    Author = RandomString(_random.Next(5, 14)),
                    Date = DateTime.Now.AddDays(_random.Next(1, 365) * -1),
                    Data = RandomString(_random.Next(50, 100))
                });
            }
            return data;
        }

        public static string RandomString(int length) => new string(Enumerable.Repeat(Chars.ToLower(), length).Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}