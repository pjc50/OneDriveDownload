using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.WindowsForms;

namespace OneDriveDownload
{
    public partial class Form1 : Form
    {
        IOneDriveClient oneDriveClient;
        private const string MsaClientId = "000000004817F8B9";
        private const string MsaReturnUrl = "https://login.live.com/oauth20_desktop.srf";
        private static readonly string[] Scopes = { "onedrive.readonly", "wl.offline_access", "wl.signin" };
        //private static readonly string[] Scopes = { "onedrive.readonly"};
        private static readonly string[] skipDirs = { };

        //string localfileroot = "C:\\TEMP";
        string localfileroot = "E:\\Laura OneDrive Backup";

        CancellationTokenSource cancelToken;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblFolderTarget.Text = localfileroot;
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            btnSync.Enabled = false;
            await listRootFiles();
            btnSync.Enabled = true;
            progressLabel.Text = "finished or cancelled.";
        }

        // From MS sample code
        private async Task SignIn()
        {
            if (this.oneDriveClient == null)
            {
                this.oneDriveClient =
                    OneDriveClient.GetMicrosoftAccountClient(
                        MsaClientId,
                        MsaReturnUrl,
                        Scopes,
                        webAuthenticationUi: new FormsWebAuthenticationUi());
                    
            }

            try
            {
                if (!this.oneDriveClient.IsAuthenticated)
                {
                    await this.oneDriveClient.AuthenticateAsync();
                }


                //UpdateConnectedStateUx(true);
            }
            catch (OneDriveException exception)
            {
                // Swallow authentication cancelled exceptions
                if (!exception.IsMatch(OneDriveErrorCode.AuthenticationCancelled.ToString()))
                {
                    if (exception.IsMatch(OneDriveErrorCode.AuthenticationFailure.ToString()))
                    {
                        MessageBox.Show(
                            "Authentication failed",
                            "Authentication failed",
                            MessageBoxButtons.OK);

                        var httpProvider = this.oneDriveClient.HttpProvider as HttpProvider;
                        httpProvider.Dispose();
                        this.oneDriveClient = null;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        } // end signin

        private void validateOrCreateDirectory(string path)
        {
            string localDir = Path.Combine(localfileroot, path);
            if(!System.IO.Directory.Exists(localDir))
            {
                System.IO.Directory.CreateDirectory(localDir);
            }
        }

        private async Task validateOrDownloadFile(string path, Item item)
        {
            string localFile = Path.Combine(localfileroot,path);
            bool doDownload = false;
            DateTime itemDateUTC = item.FileSystemInfo.LastModifiedDateTime.GetValueOrDefault().UtcDateTime;
            progressLabel.Text = "validating file " + localFile + " last modified " + itemDateUTC.ToString();
            if(System.IO.File.Exists(localFile))
            {
                FileInfo f = new FileInfo(localFile);
                System.Console.WriteLine("exists, size " + f.Length + " expected " + item.Size);
                if(f.LastWriteTimeUtc == itemDateUTC
                    && f.Length != 0
                   /* f.Length == item.Size*/)
                {
                    System.Console.WriteLine("date time size match for " + localFile+ " " + item.Size + " bytes at " + itemDateUTC);
                }
                else
                {
                    if (item.Size > 2147483648L)
                    {
                        System.Console.WriteLine("too damn large");
                        doDownload = false;
                    }
                    else if (item.Size == 0)
                    {
                        doDownload = false;
                    }
                    else if (overwriteAll.Checked)
                    {
                        doDownload = true;
                    }
                    else
                    {
                        DialogResult yn = MessageBox.Show(
                                "Date/Time/Size mismatch for " + localFile +
                                "\n OneDrive: " + item.Size + " bytes at " + itemDateUTC +
                                "\n Local: " + f.Length + " bytes at " + f.LastWriteTimeUtc +
                                "\nOverwrite this file?",
                                "File mismatch",
                                MessageBoxButtons.YesNo);
                        if (yn == DialogResult.Yes)
                            doDownload = true;
                    }
                }
            }
            else
            {
                doDownload = true;
                System.Console.WriteLine("does not exist");
            }

            if(doDownload)
            {
                progressLabel.Text = "downloading file " + localFile;
                try
                {
                    using (FileStream newFile = System.IO.File.Create(localFile, 1024 * 1024))
                    {
                        using (var contentStream = await oneDriveClient
                                      .Drive
                                      .Items[item.Id]
                                      .Content
                                      .Request()
                                      .GetAsync())
                        {
                            Task dl = contentStream.CopyToAsync(newFile);
                            dl.Wait(); // fixme cancel token?
                            newFile.Close();
                        }
                    }
                    System.IO.File.SetLastWriteTimeUtc(localFile, itemDateUTC);
                    System.IO.File.SetCreationTimeUtc(localFile, itemDateUTC);
                    System.Console.WriteLine("finished downloading " + localFile);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Exception " + e.Message + " while downloading " + localFile);
                    FileInfo fi = new FileInfo(localFile);
                    if(fi.Length == 0)
                        System.IO.File.Delete(localFile);
                }
            }
        }

        private async Task retrieveChildren(string path, string rootId, TreeNodeCollection inTree)
        {
            validateOrCreateDirectory(path);
            TreeNodeCollection subTree = /*path.Length > 0 ? inTree.Add(path).Nodes :*/ inTree;
            progressLabel.Text = "retrieving children of " + rootId + " at " + path;
            List<Item> childItems;
            var rootDir = await oneDriveClient.Drive
                            .Items[rootId]
                            .Children
                            .Request()
                            .GetAsync();
            childItems = rootDir.ToList();
            while(rootDir.NextPageRequest != null)
            {
                if (cancelToken.IsCancellationRequested) return;
                rootDir = await rootDir.NextPageRequest.GetAsync();
                childItems = childItems.Concat(rootDir).ToList();
            }
            foreach (var child in childItems)
            {
                if (cancelToken.IsCancellationRequested) return;
                string childPath = path + child.Name;

                if (child.Folder != null)
                {
                    if (skipDirs.Contains(child.Name))
                    {
                        System.Console.WriteLine("Skipping " + childPath);
                    }
                    else
                    {
                        System.Console.WriteLine("Folder " + childPath);
                        await retrieveChildren(childPath + "\\", child.Id, subTree);
                    }
                }
                else
                {
                    System.Console.WriteLine("File " + childPath);
                    await validateOrDownloadFile(childPath, child);
                }
            }
        }

        private async Task listRootFiles()
        {
            var rootItem = await oneDriveClient
                             .Drive
                             .Root
                             .Request()
                             .GetAsync();
            await retrieveChildren("", rootItem.Id, treeView1.Nodes);
        }

        private void btnPickFolder_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblFolderTarget.Text = folderBrowserDialog1.SelectedPath;
                localfileroot = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cancelToken.Cancel();
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            if (oneDriveClient != null && oneDriveClient.IsAuthenticated)
            {
                    oneDriveClient.SignOutAsync();
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            cancelToken = new CancellationTokenSource();
            await SignIn();
        }
    }
}
