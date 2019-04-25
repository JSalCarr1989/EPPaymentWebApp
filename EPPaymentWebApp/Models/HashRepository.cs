using EPPaymentWebApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using EPPaymentWebApp.Utilities;

namespace EPPaymentWebApp.Models
{
    public class HashRepository : IHashRepository
    {

        private readonly IConfiguration _config;
        private readonly IDbLoggerRepository _loggerRepo;
        private readonly IDbLoggerErrorRepository _loggerErrorRepo;
        private readonly IEnvironmentSettingsRepository _environmentSettingsRepo;

        public HashRepository(IConfiguration config,
                              IDbLoggerRepository loggerRepo,
                              IDbLoggerErrorRepository loggerErrorRepo,
                              IEnvironmentSettingsRepository environmentSettingsRepo)
        {
            _config = config;
            _loggerRepo = loggerRepo;
            _loggerErrorRepo = loggerErrorRepo;
            _environmentSettingsRepo = environmentSettingsRepo;
        }

        public string GetHashStatus(MultiPagosResponsePaymentDTO multipagosResponse)
        {
            return ValidateMultipagosHash(multipagosResponse);
        }

        private string ValidateMultipagosHash(MultiPagosResponsePaymentDTO multipagosResponse)
        {
            bool result = false;
            string hashStatus = null;

            try
            {
                var rawData = multipagosResponse.mp_order + multipagosResponse.mp_reference + multipagosResponse.mp_amount + multipagosResponse.mp_authorization;

                string environmentMpSk = _environmentSettingsRepo.GetMpSK();

                var _mpsk = !string.IsNullOrEmpty(environmentMpSk)
                                       ? environmentMpSk
                                       : _config["MpSk"];

                var generatedHash = ComputeSha256Hash(rawData, _mpsk);

                result = (generatedHash == multipagosResponse.mp_signature) ? true : false;

                 hashStatus = (result) ? StaticResponsePaymentProperties.VALID_HASH
                 : StaticResponsePaymentProperties.INVALID_HASH;

                _loggerRepo.LogHashValidationToDb(multipagosResponse, rawData, generatedHash, hashStatus);

            }
            catch (Exception ex)
            {
                _loggerErrorRepo.LogValidateMultipagosHashError(ex.ToString(), multipagosResponse.mp_reference, multipagosResponse.mp_order);
            }

       
            return hashStatus;
        }

        private  string ComputeSha256Hash(string rawData, string secret)
        {
            string computedHash = string.Empty;

            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    secret = secret ?? "";
                    var encoding = new System.Text.ASCIIEncoding();
                    byte[] keyByte = encoding.GetBytes(secret);
                    byte[] messageBytes = encoding.GetBytes(rawData);

                    using (var hmacsha256 = new HMACSHA256(keyByte))
                    {
                        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                        computedHash = ByteToString(hashmessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerErrorRepo.LogCompute256HashError(ex.ToString());
            }

            return computedHash;
        }

        private  string ByteToString(byte[] buff)
        {
            string sbinary = "";

            try
            {
                for (int i = 0; i < buff.Length; i++)
                {
                    sbinary += buff[i].ToString("X2"); // hex format
                }
            }
            catch (Exception ex)
            {
                _loggerErrorRepo.LogByteToStringError(ex.ToString());
            }

            return (sbinary).ToLower();
        }


    }
}
