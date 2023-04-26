using PANS.Entities;
using System.Collections.Generic;

namespace PANS.Services
{
    internal class QuizDataRepository : IQuizDataRepository
    {
        public List<QuizData> GetData()
        {
            return new List<QuizData>
            {
                new QuizData
                {
                    Type = "1",
                    Question = "What is C#?",
                    Answers = "Programing language/TV Show/US rapper",
                    Correct_Answer = "Programing language"
                },
                new QuizData
                {
                    Type = "1",
                    Question = "What language is used by most users on the web, besides English",
                    Answers = "Italian/Korean/Chinese",
                    Correct_Answer = "Chinese"
                },
                new QuizData
                {
                    Type = "1",
                    Question = "How many bits are in byte?",
                    Answers = "32/16/8",
                    Correct_Answer = "8"
                },
                new QuizData
                {
                    Type = "1",
                    Question = "What's a web browser?",
                    Answers = "A computer/A kind of spider/A program that allow you to access sites on the internet",
                    Correct_Answer = "A program that allow you to access sites on the internet"
                },
                new QuizData
                {
                    Type = "1",
                    Question = "Where is the headquarters of Apple",
                    Answers = "Cupertino/London/New York",
                    Correct_Answer = "Cupertino"
                },
                new QuizData
                {
                    Type = "2",
                    Question = "Choose all parts of hardware",
                    Answers = "Motherboard/Skatebord/Local disk/Hard disk",
                    Correct_Answer = "Motherboard/Hard disk"
                },
                new QuizData
                {
                    Type = "2",
                    Question = "Who is co-founders disk of Google",
                    Answers = "Sergey Brin/Stive Jobs/Scot Hassan/Larry Page",
                    Correct_Answer = "Sergey Brin/Scot Hassan/Larry Page"
                },
                new QuizData
                {
                    Type = "2",
                    Question = "Select structure type on c#",
                    Answers = "object/DateTime/string/double",
                    Correct_Answer = "double/DateTime"
                },
                new QuizData
                {
                    Type = "2",
                    Question = "Select reference type on c#",
                    Answers = "float/string/int/object",
                    Correct_Answer = "string/object"
                },
                new QuizData
                {
                    Type = "2",
                    Question = "Which of these data bases",
                    Answers = "FireBird/C#/Oracle/Java",
                    Correct_Answer = "FireBird/Oracle"
                },
                new QuizData
                {
                    Type = "3",
                    Question = "Supports all classes in the .NET class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all .NET classes; it is the root of the type hierarchy",
                    Correct_Answer = "object"
                },
                new QuizData
                {
                    Type = "3",
                    Question = "At the foundation of any unit of this type is a clear set of learning goals drawn from whatever curriculum or set of standards is in use",
                    Correct_Answer = "core curriculum"
                },
                new QuizData
                {
                    Type = "3",
                    Question = "Under Essential Project Design Elements, Teachers create or adapt a project for their context and students, and plan its implementation from launch to culmination while allowing for some degree of student voice and choice.",
                    Correct_Answer = "design and plan"
                },
                new QuizData
                {
                    Type = "3",
                    Question = "These include math, reading, and problem-solving mastered at a higher level than previously expected of high school graduates.",
                    Correct_Answer = "hard skills"
                },
                new QuizData
                {
                    Type = "3",
                    Question = "It includes the ability to work in a group and to make effective oral and written presentations, and the ability to use a personal computer to carry out routine tasks.",
                    Correct_Answer = "soft skills"
                },
            };
        }
    }
}