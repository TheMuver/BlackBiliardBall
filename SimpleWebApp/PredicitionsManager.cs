using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
    public class PredicitionsManager
    {
        private List<string> _answers = new List<string>() { "Тебе повезёт!", "Тебя ждёт что-то интересное.", "Не думай.", "Да", "Нет", "Не знаю" };
        private static PredicitionsManager _instance;

        private PredicitionsManager()
        { }


        public static PredicitionsManager GetInstance()
        {
            if (_instance == null)
                _instance = new PredicitionsManager();
            return _instance;
        }


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
