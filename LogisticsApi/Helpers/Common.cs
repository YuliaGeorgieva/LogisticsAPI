using System.Text;

namespace LogisticsApi.Helpers
{
    public static class Common
    {
        public static string GenerateTrackingNumber()
        {
            int length = 10; // Length of the tracking number
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            StringBuilder trackingNumber = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                trackingNumber.Append(chars[index]);
            }

            return trackingNumber.ToString();
        }
    }
}
