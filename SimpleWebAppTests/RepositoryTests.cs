using System;
using Xunit;

using SimpleWebApp.Repository;
using System.Diagnostics;

namespace SimpleWebAppTests
{
    public class RepositoryTests
    {
        [Fact]
        public void InsertPredictionToDbTest()
        {
            PredictionDto prediction = new PredictionDto();
            prediction.PredictionText = "Hello!";

            IPredictionsRepository repository = new PredictionsDb();
            repository.InsertPrediction(prediction);
        }

        [Fact]
        public void GetPredictionFromDbTest()
        {
            IPredictionsRepository repository = new PredictionsDb();
            Assert.Equal("Hello!", repository.GetPredictionById(1).PredictionText);
        }

        [Fact]
        public void UpdatePredictionInDbTest()
        {
            IPredictionsRepository repository = new PredictionsDb();
            repository.UpdatePrediction(new PredictionDto() { Id=1, PredictionText="Not hello!"});
        }

        [Fact]
        public void DeletePredictionInDbTest()
        {
            IPredictionsRepository repository = new PredictionsDb();
            repository.DeletePrediction(new PredictionDto() { Id = 2 });
        }
    }
}
