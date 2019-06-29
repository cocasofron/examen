namespace WebApplication3.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string OriginCountry { get; set; }
        public string Sender { get; set; }
        public string DestinationCountry { get; set;}
        public string Receiver { get; set; }
        public string Address { get; set; }
        public double Cost { get; set; }
        public string TrackingCode { get; set; }

    }
}
