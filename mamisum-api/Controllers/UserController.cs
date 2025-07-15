using mamisum_api.Models.Users;
using mamisum_api.Services;
using mamisum_api.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace mamisum_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly OTPService _otpService;
        private readonly AuthService _authService;
        private readonly UserService _userService;

        private static Dictionary<string, RegisterRequest> _pendingRegistrations = new Dictionary<string, RegisterRequest>();
        private static Dictionary<string, UpdatePasswordRequest> _pendingPasswordUpdates = new Dictionary<string, UpdatePasswordRequest>();

        public UserController(OTPService otpService, AuthService authService, UserService userService)
        {
            _otpService = otpService;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userService.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                return Conflict(new { Success = false, Message = "User already exists." });
            }

            _pendingRegistrations[request.Email] = request;
            var otp = await _otpService.GenerateOtp(request.Email);

            return Ok(new { Success = true, Message = "OTP sent to email. Please verify OTP.", Otp = otp });
        }

        [HttpPost("verifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isValid = _otpService.VerifyOtp(request.Email, request.Otp);
            if (!isValid)
            {
                return Unauthorized(new { Success = false, Message = "Invalid OTP" });
            }

            if (_pendingRegistrations.TryGetValue(request.Email, out var registrationRequest))
            {
                bool isRegistered = await _userService.RegisterUser(registrationRequest);
                if (isRegistered)
                {
                    _pendingRegistrations.Remove(request.Email);
                    var user = await _userService.GetUserByEmail(request.Email);
                    var token = _authService.GenerateToken(user.Id, request.Email, user.Role);
                    return Ok(new { Success = true, UserId= user.Id,  Message = "Registration successful", Token = token, Role =  user.Role });
                }
            }
            return NotFound(new { Success = false, Message = "No pending request found." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.GetUserByEmail(request.Email);
            if (user == null)
            {
                return NotFound(new { Success = false, Message = "User not registered. Please register first." });
            }

            if (!string.Equals(user.Role, request.Role, StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized(new { Success = false, Message = "Selected role does not match your account role." });
            }

            bool isPasswordValid = _userService.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return Unauthorized(new { Success = false, Message = "Invalid email or password." });
            }

            var token = _authService.GenerateToken(user.Id, user.Email, user.Role);
            return Ok(new { Success = true, Message = "Login successful", Token = token, UserId = user.Id, Role = user.Role });
        }

        [HttpPost("updatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            var user = await _userService.GetUserByEmail(request.Email);
            if (user == null)
            {
                return NotFound(new { Success = false, Message = "User not registered." });
            }

            bool isPasswordValid = _userService.VerifyPassword(request.CurrentPassword, user.PasswordHash);
            if (!isPasswordValid)
            {
                return Unauthorized(new { Success = false, Message = "Invalid current password." });
            }

            _pendingPasswordUpdates[request.Email] = request;
            var otp = await _otpService.GenerateOtp(request.Email);

            return Ok(new { Success = true, Message = "OTP sent to email for password update", Otp = otp });
        }

        [HttpPost("updatepassword-otpverify")]
        public async Task<IActionResult> VerifyOtpForPasswordUpdate([FromBody] OtpVerificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isValid = _otpService.VerifyOtp(request.Email, request.Otp);
            if (!isValid)
            {
                return Unauthorized(new { Success = false, Message = "Invalid OTP" });
            }

            if (_pendingPasswordUpdates.TryGetValue(request.Email.ToLower(), out var updateRequest))
            {
                var success = await _userService.UpdatePassword(request.Email, updateRequest.NewPassword);
                if (success)
                {
                    _pendingPasswordUpdates.Remove(request.Email.ToLower());
                    return Ok(new { Success = true, Message = "Password updated successfully." });
                }
            }

            return NotFound(new { Success = false, Message = "No pending password update found." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var isDeleted = await _userService.DeleteUserById(id);

            if (isDeleted)
                return Ok(new { Success = true, Message = "User deleted successfully." });

            return NotFound(new { Success = false, Message = "User not found." });
        }
    }
}
