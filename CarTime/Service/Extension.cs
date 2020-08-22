namespace CarTime.Service
{
    public static class Extension
    {
        public static string CutController(this string str)
        {
            return str.Replace("Controller", "");
        }
    }
}
