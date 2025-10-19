using Microsoft.AspNetCore.Mvc;
using FGP.Server.Protocol;

namespace FGP.Server.Controllers;

[ApiController]
[Route("fgp/inbox")]
public class InboxController : ControllerBase
{
    [HttpPost]
    public IActionResult Receive([FromBody] FgpMessage message)
    {
        // Basic validation
        if (message.Protocol != "fgp.v0")
            return BadRequest(new { error = "Unsupported protocol version" });

        // For now, we only handle ping
        if (message.Type == "ping")
        {
            var ack = new FgpAck(
                Protocol: "fgp.v0",
                Type: "ack",
                InReplyTo: message.Id,
                ReceivedAt: DateTime.UtcNow
            );
            return Ok(ack);
        }

        // If unknown message type
        return BadRequest(new { error = $"Unknown message type: {message.Type}" });
    }
}
