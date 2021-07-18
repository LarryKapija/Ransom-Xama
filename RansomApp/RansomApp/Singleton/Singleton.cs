namespace RansomApp.Singleton
{
    public class Singleton
    {
        private static Singleton _instance = null;
        public string rsaKey = "";

        protected Singleton()
        {
            rsaKey = "";
        }

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }
    }
}
