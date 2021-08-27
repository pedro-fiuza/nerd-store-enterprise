namespace NSE.Identidade.API.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpireInHours { get; set; }
        public string Emitter { get; set; }
        public string ValidFor { get; set; }
    }
}
