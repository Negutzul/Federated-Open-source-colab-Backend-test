using System;
using System.IO;
using NSec.Cryptography;

namespace FGP.Server.Services
{
    public static class KeyGenerator
    {
        public static void EnsureKeys()
        {
            var keyDir = Path.Combine(AppContext.BaseDirectory, "Keys");
            Directory.CreateDirectory(keyDir);

            var privPath = Path.Combine(keyDir, "private.key");
            var pubPath = Path.Combine(keyDir, "public.key");

            var algorithm = SignatureAlgorithm.Ed25519;

            if (!File.Exists(privPath))
            {
                // Create new keypair
                var key = Key.Create(algorithm, new KeyCreationParameters
                {
                    ExportPolicy = KeyExportPolicies.AllowPlaintextExport
                });

                // Save private key (Base64 text)
                File.WriteAllText(privPath,
                    Convert.ToBase64String(key.Export(KeyBlobFormat.NSecPrivateKey)));

                // Save public key (Base64 text)
                File.WriteAllText(pubPath,
                    Convert.ToBase64String(key.PublicKey.Export(KeyBlobFormat.RawPublicKey)));

                Console.WriteLine("✅ Generated new Ed25519 keypair in /Keys/");
            }
            else
            {
                Console.WriteLine("ℹ️ Keys already exist — skipping generation.");
            }
        }
    }
}
