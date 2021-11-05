using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

using Microsoft.Win32;

using DomainObjects;
using DVBServices;

namespace EPGCentre
{
    /// <summary>
    /// The class that creates a debug package file.
    /// </summary>
    public partial class CreateDebugPackage : Form
    {
        private string baseKeyName { get { return (@"Software\Microsoft\Windows\CurrentVersion\Media Center\Service\Epg"); } }

        /// <summary>
        /// Create a debug package file.
        /// </summary>
        public CreateDebugPackage()
        {
            InitializeComponent();

            string iniFile = Path.Combine(RunParameters.DataDirectory, "EPG Collector.ini");
            if (File.Exists(iniFile))
                tbIniFile.Text = iniFile;

            string xmltvFile = Path.Combine(RunParameters.DataDirectory, "TVGuide.xml");
            if (File.Exists(xmltvFile))
                tbXmltvFile.Text = xmltvFile;
        }

        private void cbIniFile_CheckedChanged(object sender, EventArgs e)
        {
            tbIniFile.Enabled = cbIniFile.Checked;
            btIniFileBrowse.Enabled = cbIniFile.Checked;
        }

        private void btIniFileBrowse_Click(object sender, EventArgs e)
        {
            string iniFile = browseForFile("Find EPG Collector Parameter File", RunParameters.DataDirectory, "INI Files (*.ini)|*.ini");
            if (iniFile == null)
                return;

            tbIniFile.Text = iniFile;
        }

        private void cbXmltvFile_CheckedChanged(object sender, EventArgs e)
        {
            tbXmltvFile.Enabled = cbXmltvFile.Checked;
            btXmltvFileBrowse.Enabled = cbXmltvFile.Checked;
        }

        private void btXmltvFileBrowse_Click(object sender, EventArgs e)
        {
            string xmltvFile = browseForFile("Find Output XMLTV File", RunParameters.DataDirectory, "XML Files (*.xml)|*.xml");
            if (xmltvFile == null)
                return;

            tbXmltvFile.Text = xmltvFile;
        }

        private void cbAreaRegionFile_CheckedChanged(object sender, EventArgs e)
        {
            tbAreaRegionFile.Enabled = cbAreaRegionFile.Checked;
            btAreaRegionFileBrowse.Enabled = cbAreaRegionFile.Checked;
        }

        private void btAreaRegionFileBrowse_Click(object sender, EventArgs e)
        {
            string areaRegionFile = browseForFile("Find Area/Region File", RunParameters.DataDirectory, "Area/Region Files (*.xml)|*.xml");
            if (areaRegionFile == null)
                return;

            tbAreaRegionFile.Text = areaRegionFile;
        }

        private void cbBladeRunnerFile_CheckedChanged(object sender, EventArgs e)
        {
            tbBladeRunnerFile.Enabled = cbBladeRunnerFile.Checked;
            btBladeRunnerFileBrowse.Enabled = cbBladeRunnerFile.Checked;
        }

        private void btBladeRunnerFileBrowse_Click(object sender, EventArgs e)
        {
            string bladeRunnerFile = browseForFile("Find BladeRunner File", RunParameters.DataDirectory, "BladeRunner Files (*.xml)|*.xml");
            if (bladeRunnerFile == null)
                return;

            tbBladeRunnerFile.Text = bladeRunnerFile;
        }

        private void cbSageTVFile_CheckedChanged(object sender, EventArgs e)
        {
            tbSageTVFile.Enabled = cbSageTVFile.Checked;
            btSageTVFileBrowse.Enabled = cbSageTVFile.Checked;
        }

        private void btSageTVFileBrowse_Click(object sender, EventArgs e)
        {
            string sageTVFile = browseForFile("Find Sage TV File", RunParameters.DataDirectory, "Sage TV Files (*.frq)|*.frq");
            if (sageTVFile == null)
                return;

            tbSageTVFile.Text = sageTVFile;
        }

        private void cbWmcDatabase_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWmcDatabase.Checked)
                cbWmcDatabaseBackup.Checked = false;
        }

        private void cbWmcDatabaseBackup_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWmcDatabaseBackup.Checked)
                cbWmcDatabase.Checked = false;
        }

        private void tbOtherFile_TextChanged(object sender, EventArgs e)
        {
            btnAddOtherFile.Enabled = !string.IsNullOrWhiteSpace(tbOtherFile.Text);
        }

        private void btnBrowseOtherFile_Click(object sender, EventArgs e)
        {
            string otherFile = browseForFile("Find Other File", RunParameters.DataDirectory, "Other Files (*.*)|*.*");
            if (otherFile == null)
                return;

            tbOtherFile.Text = otherFile;
        }

        private void lbOtherFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteOtherFile.Enabled = true;
        }

        private void btnAddOtherFile_Click(object sender, EventArgs e)
        {
            if (checkFileNameValid("Other File", tbOtherFile.Text))
            {
                tbOtherFile.Focus();
                return;
            }

            if (lbOtherFiles.Items.Contains(tbOtherFile.Text))
            {
                MessageBox.Show("The file has already been selected", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lbOtherFiles.Items.Add(tbOtherFile.Text);
            tbOtherFile.Text = null;
        }

        private void btnDeleteOtherFile_Click(object sender, EventArgs e)
        {
            lbOtherFiles.Items.Remove(lbOtherFiles.SelectedItem);
            lbOtherFiles.SelectedItem = null;

            btnDeleteOtherFile.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbIniFile.Checked)
            {
                if (!checkFileNameValid("Parameter File", tbIniFile.Text))
                {
                    tbIniFile.Focus();
                    return;
                }
            }

            if (cbImportFiles.Checked && !cbIniFile.Checked)
            {
                DialogResult result = MessageBox.Show("The parameter file must also be included if import files are included.", 
                    "EPG Collector - Create Debug Package", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Cancel)
                    return;

                cbIniFile.Focus();
            }

            if (cbAreaRegionFile.Checked)
            {
                if (!checkFileNameValid("Area/Region", tbAreaRegionFile.Text))
                {
                    tbAreaRegionFile.Focus();
                    return;
                }
            }

            if (cbBladeRunnerFile.Checked)
            {
                if (!checkFileNameValid("BladeRunner", tbBladeRunnerFile.Text))
                {
                    tbBladeRunnerFile.Focus();
                    return;
                }
            }

            if (cbSageTVFile.Checked)
            {
                if (!checkFileNameValid("Sage TV", tbSageTVFile.Text))
                {
                    tbSageTVFile.Focus();
                    return;
                }
            }

            if (cbWmcImportFile.Checked && !File.Exists(Path.Combine(RunParameters.DataDirectory, "TVGuide.mxf")))
            {
                DialogResult result = MessageBox.Show("The Windows Media Center import file does not exist." + Environment.NewLine + Environment.NewLine +
                    "It will be omitted from the package", "EPG Collector - Create Debug Package", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Cancel)
                    return;                
            }
            
            Collection<string> fileNames = getFileNames();
            if (fileNames == null)
                return;

            createPackage(fileNames);
            Close();
        }

        private bool checkFileNameValid(string identity, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                MessageBox.Show("No " + identity + " file specified.", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (char invalidChar in Path.GetInvalidPathChars())
            {
                if (fileName.Contains(invalidChar.ToString()))
                {
                    MessageBox.Show("The " + identity + " file does not exist.", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!File.Exists(fileName))
            {
                MessageBox.Show("The " + identity + " file does not exist.", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private Collection<string> getFileNames()
        {
            Logger.Instance.Write("Creating file list for debug package");

            Collection<string> fileNames = new Collection<string>();

            if (cbGeneralLog.Checked)
            {
                fileNames.Add(Path.Combine(RunParameters.DataDirectory, "EPG Collector.log"));
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            if (cbNetworkLog.Checked)
            {
                fileNames.Add(Path.Combine(RunParameters.DataDirectory, "EPG Collector Network.log"));
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            if (cbStreamLog.Checked)
            {
                fileNames.Add(Path.Combine(RunParameters.DataDirectory, "EPG Collector Stream.log"));
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            if (cbHistoryLog.Checked)
            {
                fileNames.Add(Path.Combine(RunParameters.DataDirectory, "EPG Collector.hst"));
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            if (cbIniFile.Checked)
            {
                fileNames.Add(tbIniFile.Text);
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            if (cbImportFiles.Checked)
            {
                if (!addImportFiles(fileNames))
                    return null;
            }

            if (cbCategoryFiles.Checked)
                addCategoryFiles(fileNames);

            if (cbXmltvFile.Checked)
            {
                fileNames.Add(tbXmltvFile.Text);
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            if (cbAreaRegionFile.Checked)
            {
                fileNames.Add(tbAreaRegionFile.Text);
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }
            
            if (cbBladeRunnerFile.Checked)
            {
                fileNames.Add(tbBladeRunnerFile.Text);
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            if (cbSageTVFile.Checked)
            {
                fileNames.Add(tbSageTVFile.Text);
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            if (cbWmcDatabase.Checked)
            {
                if (!addWmcDatabase(fileNames))
                    return null;
            }

            if (cbWmcDatabaseBackup.Checked)
            {
                if (!addWmcDatabaseBackup(fileNames))
                    return null;
            }

            if (cbWmcImportFile.Checked)
            {
                fileNames.Add(Path.Combine(RunParameters.DataDirectory, "TVGuide.mxf"));
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            foreach (string otherFile in lbOtherFiles.Items)
            {
                fileNames.Add(otherFile);
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            return fileNames;
        }

        private bool addImportFiles(Collection<string> fileNames)
        {
            RunParameters runParameters = new RunParameters(ParameterSet.Collector, RunType.Centre);
            ExitCode exitCode = runParameters.Process(tbIniFile.Text);
            if (exitCode != ExitCode.OK)
            {
                MessageBox.Show("The Parameters File contains invalid parameters.", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (runParameters.ImportFiles == null || runParameters.ImportFiles.Count == 0)
            {
                MessageBox.Show("The Parameters File does not specify any import files.", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (ImportFileSpec importFileSpec in runParameters.ImportFiles)
            {
                fileNames.Add(importFileSpec.FileName);
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            return true;    
        }

        private void addCategoryFiles(Collection<string> fileNames)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(RunParameters.ConfigDirectory, "Program Categories"));
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.cfg");

            foreach (FileInfo fileInfo in fileInfos)
                processCategoryFile(fileNames, fileInfo);
        }

        private void processCategoryFile(Collection<string> fileNames, FileInfo categoryFileInfo)
        {
            fileNames.Add(categoryFileInfo.FullName);

            string userPath = Path.Combine(RunParameters.DataDirectory, categoryFileInfo.Name);
            if (File.Exists(userPath))
                fileNames.Add(userPath);
        }

        private bool addWmcDatabase(Collection<string> fileNames)
        {
            string dbName = getWmcDbName();
            if (dbName == null)
                return false;

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "ehome", dbName + ".db"); 

            if (!File.Exists(dbPath))
            {
                MessageBox.Show("The Windows Media Center database " + dbName + " does not exist.", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            fileNames.Add(dbPath);
            Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);

            object clientIdObject = getWmcRegistryValue("clientid");
            if (clientIdObject == null)
                return false;

            string clientIdPath = Path.Combine(RunParameters.DataDirectory, "WMC Client ID.txt");
            StreamWriter clientIdWriter = new StreamWriter(clientIdPath);
            clientIdWriter.Write((string)clientIdObject);
            clientIdWriter.Close();

            fileNames.Add(clientIdPath);
            Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);

            return true;
        }

        private int compareDbVersion(string dbName1, string dbName2)
        {
            string namePrefix = "mcepg";
            string nameSuffix = ".db";

            string[] name1Parts = dbName1.Replace(nameSuffix, "").Split(new char[] { '-' });
            int dbName1Version = Int32.Parse(name1Parts[0].Replace(namePrefix, ""));
            int dbName1Subversion = Int32.Parse(name1Parts[1]);
            
            string[] name2Parts = dbName2.Replace(nameSuffix, "").Split(new char[] { '-' });
            int dbName2Version = Int32.Parse(name2Parts[0].Replace(namePrefix, ""));
            int dbName2Subversion = Int32.Parse(name2Parts[1]);

            int reply = dbName1Version.CompareTo(dbName2Version);
            if (reply == 0)
                reply = dbName1Subversion.CompareTo(dbName2Subversion);
            
            return reply;
        }

        private string getWmcDbName()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "ehome");
            DirectoryInfo ehomeDirectory = new DirectoryInfo(dbPath);
            if (!ehomeDirectory.Exists)
            {
                MessageBox.Show("The Windows Media Center directory does not exist.", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            FileInfo[] fileInfos = ehomeDirectory.GetFiles("mcepg*.db");
            if (fileInfos.Length == 0)
            {
                MessageBox.Show("The Windows Media Center database does not exist.", "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            FileInfo selectedDb = null;

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (selectedDb == null)
                    selectedDb = fileInfo;
                else
                {
                    if (compareDbVersion(selectedDb.Name, fileInfo.Name) < 0)
                        selectedDb = fileInfo;
                }
            }

            return selectedDb.Name.Replace(".db", "");
        }

        private bool addWmcDatabaseBackup(Collection<string> fileNames)
        {
            string dbName = getWmcDbName();
            if (dbName == null)
                return false;

            if (!backupDatabase())
                return false;

            Collection<string> backupFileNames = getBackupFileNames(dbName);
            foreach (string backupFileName in backupFileNames)
            {
                fileNames.Add(backupFileName);
                Logger.Instance.Write("Added file: " + fileNames[fileNames.Count - 1]);
            }

            return true;
        }

        private object getWmcRegistryValue(string name)
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(baseKeyName, RegistryKeyPermissionCheck.ReadSubTree);
                object registryValue = registryKey.GetValue(name);
                if (registryValue == null)
                    MessageBox.Show("Failed to find the Windows Media Center database registry value for '" + name + "'.", 
                        "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return registryValue;
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to get the Windows Media Center database registry value for '" + name + "'." + Environment.NewLine + Environment.NewLine +
                    e.Message, "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        private bool backupDatabase()
        {
            string runDirectory = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "ehome");
            Logger.Instance.Write("Running Windows Media Centre backup task 'mcupdate'");

            Process backupProcess = new Process();
            backupProcess.StartInfo.FileName = Path.Combine(runDirectory, "mcUpdate.exe");
            backupProcess.StartInfo.WorkingDirectory = runDirectory + Path.DirectorySeparatorChar;
            backupProcess.StartInfo.Arguments = "-b";
            backupProcess.StartInfo.UseShellExecute = false;
            backupProcess.StartInfo.CreateNoWindow = true;

            try
            {
                bool reply = backupProcess.Start();
                if (!reply)
                {
                    Logger.Instance.Write("Windows Media Centre backup task has started");
                    return false;
                }
                
                backupProcess.WaitForExit();

                if (backupProcess.ExitCode != 0)
                    Logger.Instance.Write("Windows Media Centre backup task failed - exit code " + backupProcess.ExitCode);
                else
                    Logger.Instance.Write("Windows Media Centre backup task completed successfully");

                return backupProcess.ExitCode == 0;
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<e> Failed to run Windows Media Centre backup task");
                Logger.Instance.Write("<e> " + e.Message);
                return false;
            }
        }

        private Collection<string> getBackupFileNames(string storeName)
        {
            Collection<string> backupFileNames = new Collection<string>();
            
            string lineupFileName = processBackupDirectory(storeName, "lineup");
            if (lineupFileName != null)
                backupFileNames.Add(lineupFileName);

            string recordingsFileName = processBackupDirectory(storeName, "recordings");
            if (recordingsFileName != null)
                backupFileNames.Add(recordingsFileName);

            string subscriptionsFileName = processBackupDirectory(storeName, "subscriptions");
            if (subscriptionsFileName != null)
                backupFileNames.Add(subscriptionsFileName);

            return backupFileNames;
        }

        private string processBackupDirectory(string storeName, string backupName)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "ehome", storeName, "backup", backupName); 
            if (!Directory.Exists(path))
            {
                Logger.Instance.Write("<e> Backup directory does not exist - " + path);
                return null;
            }

            Logger.Instance.Write("Getting latest backup file from " + path);

            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            FileInfo selectedFile = null;

            foreach (FileInfo file in files)
            {
                if (selectedFile == null)
                    selectedFile = file;
                else
                {
                    if (file.Name.CompareTo(selectedFile.Name) > 0)
                        selectedFile = file;
                }
            }

            if (selectedFile == null)
                Logger.Instance.Write("<e> Backup directory is empty");
            else
                Logger.Instance.Write("Latest backup file is " + selectedFile.Name);

            return selectedFile.FullName;
        }

        private void createPackage(Collection<string> fileNames)
        {
            Cursor.Current = Cursors.WaitCursor;

            string packageDirectory = Path.Combine(RunParameters.DataDirectory, "Debug Packages");
            string packageFileName = "EPG Collector " + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".pkg";
            string fullPath = Path.Combine(packageDirectory, packageFileName);

            Logger.Instance.Write("Creating debug package " + fullPath);

            Directory.CreateDirectory(packageDirectory);            
            BinaryWriter writer = new BinaryWriter(File.Open(fullPath, FileMode.Create));

            foreach (string fileName in fileNames)
            {
                if (!processFile(writer, fileName))
                {
                    writer.Close();
                    File.Delete(fullPath);
                    return;
                }
            }

            long fileSize = writer.BaseStream.Length;

            Cursor.Current = Cursors.Default;
            writer.Close();
            Logger.Instance.Write("Debug package complete");

            MessageBox.Show("The debug package has been successfully created (files = " + fileNames.Count + " size = " + getSizeDescription(fileSize) + ")." + Environment.NewLine + Environment.NewLine +
                    fullPath, "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string getSizeDescription(long size)
        {
            if (size < 1024)
                return size + " bytes";

            if (size < 1024 * 1024)
                return (size / 1024).ToString() + " KB";

            return ((double)size / (1024 * 1024)).ToString("0.00") + " MB";
        }

        private bool processFile(BinaryWriter writer, string fileName)
        {
            Logger.Instance.Write("Processing file " + fileName);

            FileInfo fileInfo = new FileInfo(fileName);

            if (!writeNameBytes(writer, fileInfo.Name))
                return false;

            if (!writeDataBytes(writer, fileInfo))
                return false;

            Logger.Instance.Write("File added to package");

            return true;
        }

        private bool writeNameBytes(BinaryWriter writer, string name)
        {
            if (!writeLengthBytes(writer, name.Length, 1, false))
                return false;

            byte[] nameBytes = Encoding.ASCII.GetBytes(name);

            return writeBytes(writer, nameBytes);
        }

        private bool writeDataBytes(BinaryWriter writer, FileInfo fileInfo)
        {
            FileStream originalFileStream = fileInfo.OpenRead();

            if (originalFileStream.Length > 1024)
            {
                MemoryStream compressedStream = compress(originalFileStream);
                Logger.Instance.Write("Uncompressed length = " + originalFileStream.Length + " compressed length = " + compressedStream.Length);
                originalFileStream.Close();

                compressedStream.Position = 0;

                if (!writeLengthBytes(writer, compressedStream.Length, 4, true))
                {
                    compressedStream.Close();
                    return false;
                }

                byte[] writeBuffer = new byte[16 * 1024];
                int readLength = -1;

                while (readLength != 0)
                {
                    readLength = compressedStream.Read(writeBuffer, 0, writeBuffer.Length);
                    if (readLength > 0)
                    {
                        if (!writeBytes(writer, writeBuffer, readLength))
                            return false;
                    }
                }

                compressedStream.Close();
            }
            else
            {
                if (!writeLengthBytes(writer, originalFileStream.Length, 4, false))
                {
                    originalFileStream.Close();
                    return false;
                }

                byte[] byteBuffer = new byte[originalFileStream.Length];
                int bytesRead = originalFileStream.Read(byteBuffer, 0, byteBuffer.Length);
                if (bytesRead != byteBuffer.Length)
                {
                    originalFileStream.Close();
                    return false;
                }

                originalFileStream.Close();

                if (!writeBytes(writer, byteBuffer, bytesRead))
                    return false;
            }

            return true;
        }

        private MemoryStream compress(Stream stream)
        {
            MemoryStream outStream = new MemoryStream();
            
            using (var compressStream = new GZipStream(outStream, CompressionMode.Compress, true))
            {
                stream.CopyTo(compressStream);
            }

            outStream.Seek(0, SeekOrigin.Begin);
            
            return outStream;
        }

        private bool writeLengthBytes(BinaryWriter writer, long length, int lengthSize, bool compressedData)
        {
            byte[] lengthBytes = new byte[lengthSize + 1];

            for (int index = 0; index < lengthSize; index++)
            {
                lengthBytes[index] = (byte)(length & 0xff);
                length = length >> 8;
            }

            lengthBytes[lengthBytes.Length - 1] = compressedData ? (byte)1 : (byte)0;

            return writeBytes(writer, lengthBytes);
        }

        private bool writeBytes(BinaryWriter writer, byte[] dataBytes)
        {
            return writeBytes(writer, dataBytes, dataBytes.Length);
        }

        private bool writeBytes(BinaryWriter writer, byte[] dataBytes, int dataLength)
        {
            try
            {
                writer.Write(dataBytes, 0, dataLength);
                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.Write("Failed to write debug package: " + e.Message);

                Cursor.Current = Cursors.Default;

                MessageBox.Show("Failed to create debug package." + Environment.NewLine + Environment.NewLine +
                    e.Message, "EPG Collector - Create Debug Package", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private string browseForFile(string title, string initialDirectory, string filter)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = filter;
            openFile.InitialDirectory = initialDirectory;
            openFile.RestoreDirectory = true;
            openFile.Title = title;

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return null;

            return openFile.FileName;
        }
    }
}
