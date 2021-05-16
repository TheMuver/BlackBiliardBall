using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApp.Repository
{
    class PredictionsDbInMemory : IPredictionsRepository
    {
        private List<PredictionDto> _predictions = new List<PredictionDto>();

        public void DeletePrediction(PredictionDto prediction)
        {
            _predictions.Remove(prediction);
        }

        public PredictionDto[] GetAllPredictions()
        {
            return _predictions.ToArray();
        }

        public PredictionDto GetPredictionById(int id)
        {
            return _predictions[id];
        }

        public void InsertPrediction(PredictionDto prediction)
        {
            _predictions.Add(prediction);
        }

        public void UpdatePrediction(PredictionDto prediction)
        {
            _predictions.Remove(prediction);
            _predictions.Add(prediction);
        }
    }
}
