using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Repository
{
    public interface IPredictionsRepository
    {
        void InsertPrediction(string text);

        void UpdatePrediction(PredictionDto prediction);

        void DeletePrediction(PredictionDto prediction);

        PredictionDto GetPredictionById(int id);

        PredictionDto[] GetAllPredictions();
    }
}
