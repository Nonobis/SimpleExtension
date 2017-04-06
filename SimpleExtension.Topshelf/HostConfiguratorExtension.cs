using System;
using System.Collections.Generic;
using System.ServiceProcess.Design;
using System.ServiceProcess;
using System.DirectoryServices.AccountManagement;
using Topshelf.Builders;
using Topshelf.Configurators;
using Topshelf.HostConfigurators;
using Topshelf.Logging;

namespace SimpleExtension.Topshelf
{
    #region Internal Class
    internal class RunAsFirstUserHostConfigurator : HostBuilderConfigurator
    {

        #region Logger

        private static readonly LogWriter Log;

        #endregion

        #region Property

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">builder</exception>
        public HostBuilder Configure(HostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Match<InstallBuilder>(x =>
            {
                bool valid = false;
                while (!valid)
                {
                    using (ServiceInstallerDialog serviceInstallerDialog = new ServiceInstallerDialog())
                    {
                        serviceInstallerDialog.Username = Username;
                        serviceInstallerDialog.ShowInTaskbar = true;
                        serviceInstallerDialog.ShowDialog();
                        switch (serviceInstallerDialog.Result)
                        {
                            case ServiceInstallerDialogResult.OK:
                                Username = serviceInstallerDialog.Username;
                                Password = serviceInstallerDialog.Password;
                                valid = CheckCredentials(Username, Password);
                                break;
                            case ServiceInstallerDialogResult.Canceled:
                                throw new InvalidOperationException("User canceled installation.");
                        }
                    }
                }
                x.RunAs(Username, Password, ServiceAccount.User);
            });
            return builder;
        }

        /// <summary>
        /// Checks the credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private bool CheckCredentials(string username, string password)
        {
            try
            {
                if (username.StartsWith(@".\", StringComparison.Ordinal))
                {
                    using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                    {
                        return context.ValidateCredentials(username.Remove(0, 2), password);
                    }
                }
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    return context.ValidateCredentials(username, password);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Exception: {0}", ex);
                return false;
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidateResult> Validate()
        {
            yield return this.Success("Specified credentials are valid !");
        }

        #endregion
    }

    #endregion

    public static class HostConfiguratorExtension
    {
        /// <summary>
        /// Runs as first prompt.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">configurator</exception>
        public static HostConfigurator RunAsFirstPrompt(this HostConfigurator configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator), "HostConfigurator not specified");

            RunAsFirstUserHostConfigurator hostConfigurator = new RunAsFirstUserHostConfigurator();
            configurator.AddConfigurator(hostConfigurator);
            return configurator;
        }
    }
}
