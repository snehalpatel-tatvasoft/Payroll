using System.Security.Cryptography;

namespace PalladiumPayroll.Helper.License
{
    public class Licensing
    {
        public static string ComputeProductID(string installKey, string hddSerial)
        {
            try
            {
                if (installKey.Length == 0)
                {
                    return "";
                }
                if (hddSerial.Length == 0)
                {
                    return "";
                }
                string strBlock = hddSerial + installKey;
                string strResults = Compute(strBlock, GenOp.ghdoSHA384);
                return strResults;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public enum GenOp
        {
            ghdoMD5 = 0,
            ghdoSHA160 = 1,
            ghdoSHA384 = 2,
            ghdoSHA512 = 3
        }
        public static string Compute(string strSource, GenOp cryMethod)
        {
            System.Text.UnicodeEncoding uEncode = new System.Text.UnicodeEncoding();
            // Create a byte array from the source text passed as an argument.
            byte[] bytProducts = uEncode.GetBytes(strSource);
            byte[] hash;
            switch (cryMethod)
            {
                case GenOp.ghdoMD5:
                    {
                        var md5 = MD5.Create();
                        hash = md5.ComputeHash(bytProducts);
                        break;
                    }

                case GenOp.ghdoSHA160:
                    {
                        var sha1 = SHA1.Create();
                        hash = sha1.ComputeHash(bytProducts);
                        break;
                    }

                case GenOp.ghdoSHA384:
                    {
                        var sha384 = SHA384.Create();
                        hash = sha384.ComputeHash(bytProducts);
                        break;
                    }

                case GenOp.ghdoSHA512:
                    {
                        var sha512 = SHA512.Create();
                        hash = sha512.ComputeHash(bytProducts);
                        break;
                    }

                default:
                    {
                        hash = null;
                        break;
                    }
            }
            return Convert.ToBase64String(hash);
        }
    }
    public static class mPalladWeb
    {
        private const string Vala = "PallSrvUsr*#&!v335641231asdfRRadf65841g4dds333f";
        private const string Val2a = "PallS3320^^9f44add34151ADFereeD443!@aeee";

        public static string PalladVal()
        {
            string response = Licensing.Compute(Vala, Licensing.GenOp.ghdoSHA384);
            return response;
        }

        public static string PalladVal2()
        {
            string response = Licensing.Compute(Val2a, Licensing.GenOp.ghdoSHA384);
            return response;
        }
    }
}
