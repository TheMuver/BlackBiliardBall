using System;
using System.

using SimpleWebApp.Repository;

namespace SimpleWebApp
{
    public class Prediction
    {
        public string PredictionText { get; set; }
        public int? Id { get; set; }

        public Prediction() { }

        public Prediction(string p)
        {
            PredictionText = p;
        }
        public Prediction(int i, string p)
        {
            Id = i;
            PredictionText = p;
        }

        public Prediction(PredictionDto dto)
        {
            PredictionText = dto.PredictionText;
            Id = dto.Id;
        }

        public PredictionDto GetDto()
        {
            if (Id != null)
                return new PredictionDto() { PredictionText = PredictionText, Id = (int)Id };
            else
                throw new ArgumentException("prediction.Id is null (not defined)");
        }

        public override bool Equals(object obj)
        {
            if (obj is Prediction)
                return PredictionText == ((Prediction)obj).PredictionText;
            return base.Equals(obj);
        }
    }
}