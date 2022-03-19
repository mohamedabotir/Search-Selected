using Microsoft.VisualStudio.Shell.Interop;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;
using EnvDTE;
using Microsoft;
using SearchExtension.Options;


namespace SearchExtension.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class SearchCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 256;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("9fe7d6b8-faec-4f03-9598-0106f2108e90");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        public static DTE DteInstance;
        public static IVsOutputWindowPane OutputWindow;


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private SearchCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static SearchCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in SearchCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            OutputWindow = await package.GetServiceAsync(typeof(SVsGeneralOutputWindowPane)) as IVsOutputWindowPane;
            DteInstance = await package.GetServiceAsync(typeof(DTE)) as DTE;
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new SearchCommand(package, commandService);
            Assumes.Present(DteInstance);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e)
        {
            var options = this.package.GetDialogPage(typeof(SearchOptions)) as SearchOptions;
            TextSelection textSelection = DteInstance?.ActiveDocument?.Selection as TextSelection;
            if (textSelection == null)
            {
                DteInstance.StatusBar.Text = "Selection is null or Empty";
                return;
            }

            var text = textSelection?.Text?.Trim();
            if (!String.IsNullOrWhiteSpace(text))
            {
              
                DteInstance.StatusBar.Text = $"Searching {text}";
                OutputWindow.OutputString($"\nLog {DateTime.Now} Searching:{text}\n");
                string Url = string.Format(options.Url, text.Replace(" ","%20"));
                if (options.UseVSBrowser)
                    DteInstance.ItemOperations.Navigate(Url, vsNavigateOptions.vsNavigateOptionsDefault);
                else
                    System.Diagnostics.Process.Start(new ProcessStartInfo(@options.BrowserPath)
                    {

                        UseShellExecute = true,
                        Arguments = Url
                    });
            }
            else
            {
                DteInstance.StatusBar.Text = "some ";
            }
        }
    }
}
