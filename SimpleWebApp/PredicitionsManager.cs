using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
    public class PredicitionsManager
    {
        private List<string> _answers = new List<string>() { "Тебе повезёт!", "Тебя ждёт что-то интересное.", "Не думай.", "Да", "Нет", "Не знаю" };
        
        public string GetRandomPrediction()
        {
            return _answers[new Random().Next(_answers.Count)];
        }

        public void AddPrediction(string prediction)
        {
            _answers.Add(prediction);
        }
    }
}
