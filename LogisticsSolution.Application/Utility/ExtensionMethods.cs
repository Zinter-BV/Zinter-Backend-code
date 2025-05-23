using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Dtos;
using LogisticsSolution.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LogisticsSolution.Application.Utility
{
    public static class ExtensionMethods
    {
        private static readonly Random _random = new Random();
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const string Numbers = "0123456789";

        public static PasswordEncriptResponseModel EncriptPassword(this string password)
        {
            var result = new PasswordEncriptResponseModel();

            using (var hmac = new HMACSHA512())
            {
                result.PasswordSalt = hmac.Key;
                result.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return result;
        }

        public static bool VerifyPassword(this string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (passwordHash == null || passwordHash.Length == 0 || passwordSalt == null || passwordSalt.Length == 0)
                return false;

            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var rehashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return rehashedPassword.SequenceEqual(passwordHash);
            }
        }

        public static bool IsStrongPassword(this string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;

            bool hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, "[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        public static string GenerateEmailVerificationToken(int? number = null)
        {
            if(number is null)
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
           
            if(number < 1 )
                throw new ArgumentOutOfRangeException(nameof(number));

            var result = new StringBuilder();
            for (int i = 0; i < number; i++)
            {
                result.Append(Numbers[_random.Next(Numbers.Length)]);
            }
            return result.ToString();

        }

        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, emailPattern);
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            string pattern = @"^\d{11}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static string CreateJwtToken(int userId, int role, string appSettingtoken, double expirytime)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Convert.ToString(userId)),
                new Claim(ClaimTypes.Role, Convert.ToString(role))

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingtoken));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirytime),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public static HttpContextContent GetHttpContextValues(this IHttpContextAccessor context)
        {
            try
            {
                var userClaims = context.HttpContext?.User?.Claims;

                if (userClaims == null || !userClaims.Any())
                {
                    return null;
                }

                return new HttpContextContent
                {
                    userId = Convert.ToInt32(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value),
                    role = (RoleEnum)Convert.ToInt16(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value)
                };
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static ResponseModel<T> FailResponse<T>(this string message, T result = default)
        {
            return new ResponseModel<T>
            {
                ResponseStatus = false,
                ResponseMessage = message,
                Result = result
            };
        }

        public static ResponseModel<T> SuccessfulResponse<T>(this T result)
        {
            return new ResponseModel<T>
            {
                ResponseStatus = true,
                ResponseMessage = "Successful",
                Result = result
            };
        }

        public static string GenerateMovingCode()
        {
            var result = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                result.Append(Characters[_random.Next(Characters.Length)]);
            }
            return result.ToString();

        }

        public static string GetFileType(this IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "None"; // No file uploaded

            // Read the first few bytes (file signature) to identify type
            using (var stream = file.OpenReadStream())
            {
                byte[] header = new byte[4];
                stream.Read(header, 0, header.Length);

                // Convert to hex string
                string fileSignature = BitConverter.ToString(header).Replace("-", "").ToUpper();

                // Check for known file signatures
                if (IsImage(fileSignature, file.ContentType)) return "Image";
                if (IsExcel(fileSignature, file.FileName)) return "Excel";
                if (IsPdf(fileSignature)) return "PDF";
            }

            return "None"; // File type not recognized
        }

        private static bool IsImage(string fileSignature, string contentType)
        {
            string[] imageSignatures = { "FFD8FF", "89504E47", "47494638", "424D" }; // JPG, PNG, GIF, BMP
            string[] allowedMimeTypes = { "image/jpeg", "image/png", "image/gif", "image/bmp" };

            return Array.Exists(imageSignatures, sig => fileSignature.StartsWith(sig)) ||
                   Array.Exists(allowedMimeTypes, mime => contentType.Equals(mime, StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsExcel(string fileSignature, string fileName)
        {
            string[] excelSignatures = { "504B0304", "D0CF11E0" }; // XLSX, XLS
            string[] allowedExtensions = { ".xlsx", ".xls" };

            return Array.Exists(excelSignatures, sig => fileSignature.StartsWith(sig)) ||
                   Array.Exists(allowedExtensions, ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsPdf(string fileSignature)
        {
            return fileSignature.StartsWith("25504446"); // PDF file signature
        }
    }
}
