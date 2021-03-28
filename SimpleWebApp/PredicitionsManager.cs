using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
    public class PredicitionsManager
    {
        private List<Prediction> _answers = new List<Prediction>() { new Prediction("Тебе повезёт!")/*, "Тебя ждёт что-то интересное.", "Не думай.", "Да.", "Нет.", "Не знаю."*/ };

        public Prediction GetRandomPrediction()
        {
            if (_answers.Count != 0)
                return _answers[new Random().Next(_answers.Count)];
            return new Prediction("В данный момент предсказания отсутствуют.");
        }

        public Prediction[] GetAllPreditictions()
        {
            return _answers.ToArray();
        }

        public void AddPrediction(Prediction prediction)
        {
            _answers.Add(prediction);
        }

        public void DeletePrediction(Prediction prediction)
        {
            if (_answers.Contains(prediction))
                _answers.Remove(prediction);
        }

        public void UpdatePrediction(int i, string text)
        {
            _answers[i].prediction = text;
        }
    }
}
