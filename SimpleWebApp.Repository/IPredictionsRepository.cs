using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Repository
{
    public interface IPredictionsRepository
    {
        void SavePrediction(PredictionDto prediction);

        
    }
}
