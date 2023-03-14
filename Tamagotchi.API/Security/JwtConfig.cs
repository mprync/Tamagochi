using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.API.Security
{
    /// <summary>
    /// Holds jwt configurations.
    /// </summary>
    public class JwtConfig
    {
        /// <summary>
        /// Jwt Secret
        /// </summary>
        [Required]
        public string Secret { get; set; }

        /// <summary>
        /// Jwt duration in milliseconds.
        /// </summary>
        [Required]
        public int Expiry { get; set; }

        /// <summary>
        /// Jwt audience.
        /// </summary>
        [Required]
        public string Audience { get; set; }

        /// <summary>
        /// Jwt issuer.
        /// </summary>
        [Required]
        public string Issuer { get; set; }


        /*
         * To create jwt secret
         * 
            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var jwtSecret = Convert.ToBase64String(key);
            Console.WriteLine(jwtSecret);
         */
    }
}