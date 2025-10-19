namespace FGP.Server.Protocol;

public record FgpMessage(
    string Protocol,
    string Type,
    string Id,
    DateTime SentAt,
    string From,
    string To,
    object Payload
);

public record FgpAck(
    string Protocol,
    string Type,
    string InReplyTo,
    DateTime ReceivedAt
);
