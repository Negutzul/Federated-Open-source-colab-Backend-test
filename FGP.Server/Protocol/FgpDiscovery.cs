namespace FGP.Server.Protocol;

public record FgpKey(string Kid, string Alg, string PublicKey);
public record FgpDiscovery(string Protocol, string Instance, string Inbox, List<FgpKey> Keys);
