namespace SimpleWebApp
{
    public class Prediction
    {
        public string prediction { get; set; }

        public Prediction() { }

        public Prediction(string p)
        {
            prediction = p;
        }

        public override bool Equals(object obj)
        {
            if (obj is Prediction)
                return prediction == ((Prediction)obj).prediction;
            return base.Equals(obj);
        }
    }
}