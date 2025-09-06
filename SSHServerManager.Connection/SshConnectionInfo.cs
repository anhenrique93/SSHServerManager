using Renci.SshNet;

namespace SSHServerManager.Connection
{
    public static class SshConnectionInfo
    {
        public static ConnectionInfo Build(
            string host,
            string username,
            int? port = 22,
            string? keyPath = null,
            string? keyPassphrase = null,
            string? password = null,
            TimeSpan? timeout = null
        )
        {
            var methods = new List<AuthenticationMethod>();

            // Private key authentication
            if (!string.IsNullOrEmpty(keyPath))
            { 
                var keyFile = string.IsNullOrEmpty(keyPassphrase) 
                    ? new PrivateKeyFile(keyPath) 
                    : new PrivateKeyFile(keyPath, keyPassphrase);

                methods.Add(new PrivateKeyAuthenticationMethod(username, keyFile));
            }

            // Password authentication and keyboard interactive
            if (!string.IsNullOrEmpty(password))
            { 
                methods.Add(new PasswordAuthenticationMethod(username, password));

                var keyboardAuth = new KeyboardInteractiveAuthenticationMethod(username);
                keyboardAuth.AuthenticationPrompt += (_, e) =>
                {
                    foreach (var prompt in e.Prompts)
                    {
                        if (prompt.Request.Contains("Password:", StringComparison.InvariantCultureIgnoreCase))
                        {
                            prompt.Response = password;
                        }
                    }
                };
                methods.Add(keyboardAuth);
            }

            if (methods.Count == 0)
                throw new ArgumentException("At least one authentication method (key or password) must be provided.");

            var info = new ConnectionInfo(
                host,
                username,
                methods.ToArray()
            )
            {
                Timeout = timeout ?? TimeSpan.FromSeconds(30)
            };

            return info;
        }

        // Overloads
        public static ConnectionInfo ForPassword(
            string host, string username, string password, int? port = 22, TimeSpan? timeout = null)
            => Build(host, username, password: password, port: port, timeout: timeout);

        public static ConnectionInfo ForPrivateKey(
            string host, string username, string keyPath, int? port = 22, string? keyPassphrase = null, TimeSpan? timeout = null)
            => Build(host, username, keyPath: keyPath, port: port, keyPassphrase: keyPassphrase, timeout: timeout);
    }
}
