using PANS.Entities;
using System.Collections.Generic;

namespace PANS.Services
{
    internal interface IQuizDataRepository
    {
        List<QuizData> GetData();
    }
}
