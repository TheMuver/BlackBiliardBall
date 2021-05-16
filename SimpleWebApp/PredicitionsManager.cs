using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SimpleWebApp.Repository;

namespace SimpleWebApp
{
    public class PredicitionsManager
    {
        private IPredictionsRepository _repository;

        public PredicitionsManager(IPredictionsRepository repository)
        {
            _repository = repository;
        }

        public Prediction GetRandomPrediction()
        {
            List<Prediction> list = _repository.GetAllPredictions().Select(x => new Prediction(x.PredictionText)).ToList();
            return list[new Random().Next(list.Count)];
        }

        public List<Prediction> GetAllPreditictions()
        {
            return _repository.GetAllPredictions().Select(x => new Prediction(x)).ToList();
        }

        public void AddPrediction(Prediction prediction)
        {
            _repository.InsertPrediction(prediction.PredictionText);
        }

        public void DeletePrediction(Prediction prediction)
        {
            _repository.DeletePrediction(prediction.GetDto());
        }

        public void UpdatePrediction(Prediction prediction)
        {
            _repository.UpdatePrediction(prediction.GetDto());
        }
    }
}
