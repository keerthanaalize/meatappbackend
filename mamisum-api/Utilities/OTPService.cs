namespace mamisum_api.Utilities
{
    public class OTPService
    {
        private readonly Dictionary<string, string> _otpStore = new Dictionary<string, string>();
        private readonly Random _random = new Random();
        private readonly EmailService _emailService;

        public OTPService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<string> GenerateOtp(string email)
        {
            string otp = _random.Next(100000, 999999).ToString();

            _otpStore[email] = otp;

            await _emailService.SendEmailAsync(email, "Your OTP Code", $"Your OTP code is: <b>{otp}</b>");

            return otp;
        }

        public bool VerifyOtp(string email, string otp)
        {
            return _otpStore.ContainsKey(email) && _otpStore[email] == otp;
        }

        public void ClearOtp(string email)
        {
            _otpStore.Remove(email);
        }
    }
}
