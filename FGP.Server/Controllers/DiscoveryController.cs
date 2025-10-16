using Microsoft.AspNetCore.Mvc;
using FGP.Server.Protocol;
using System;
using System.IO;

namespace FGP.Server.Controllers;

[ApiController]
[Route(".well-known/fgp")]
public class DiscoveryController : ControllerBase
{
    private readonly IConfiguration _config;

    public DiscoveryController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // Load public key from file
        var keyPath = Path.Combine(AppContext.BaseDirectory, "Keys", "public.key");
        if (!System.IO.File.Exists(keyPath))
            return NotFound(new { error = "Public key not found" });

        var publicKeyBase64 = System.IO.File.ReadAllText(keyPath);

        // Values from appsettings.json (or defaults)
        var baseUrl = _config["Fgp:BaseUrl"] ?? "https://localhost:5001";
        var keyId = _config["Fgp:KeyId"] ?? "2025-10-14";

        // Build discovery object
        var discovery = new FgpDiscovery(
            Protocol: "fgp.v0",
            Instance: baseUrl,
            Inbox: $"{baseUrl}/fgp/inbox",
            Keys: new List<FgpKey> {
                new(keyId, "ed25519", publicKeyBase64)
            }
        );

        return Ok(discovery);
    }
}
