using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using Dapper;

namespace SimpleWebApp.Repository
{
    public class PredictionsDb : IPredictionsRepository
    {
        private const string CONNECTIONSTRING = "Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;";

        public void InsertPrediction(string text)
        {
            using (IDbConnection db = new MySqlConnection(CONNECTIONSTRING))
            {
                // Может выстрелить в коленку (я не знаю как называется столбик с текстом)
                string sqlQuery = "INSERT INTO predictions (prediction_text) VALUES (@PredictionText);";
                
                int rowsAffected = db.Execute(sqlQuery, text);
            }
        }

        public PredictionDto GetPredictionById(int id)
        {
            PredictionDto prediction;
            using (IDbConnection db = new MySqlConnection(CONNECTIONSTRING))
            {
                string sqlQuery = "SELECT * FROM predictions WHERE id=@Id";

                prediction = db.Query<PredictionDto>(sqlQuery, new { id }).FirstOrDefault();
            }
            return prediction;
        }
        
        public void UpdatePrediction(PredictionDto prediction)
        {
            using (IDbConnection db = new MySqlConnection(CONNECTIONSTRING))
            {
                string sqlQuery = "UPDATE predictions SET predictionText=@PredictionText WHERE id=@Id";

                int rowsAffected = db.Execute(sqlQuery, prediction);
            }
        }

        public void DeletePrediction(PredictionDto prediction)
        {
            using (IDbConnection db = new MySqlConnection(CONNECTIONSTRING))
            {
                string sqlQuery = "DELETE FROM predictions WHERE id=@Id";

                int rowsAffected = db.Execute(sqlQuery, prediction);
            }
        }

        public PredictionDto[] GetAllPredictions()
        {
            PredictionDto[] predictions;
            using (IDbConnection db = new MySqlConnection(CONNECTIONSTRING))
            {
                string sqlQuery = "SELECT * FROM predictions";

                predictions = db.Query<PredictionDto>(sqlQuery).ToArray();
            }
            return predictions;
        }
    }
}
