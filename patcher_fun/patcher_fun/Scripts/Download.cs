// Papix Work ~ https://metin2.dev/profile/47045-papix/
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms;
using patcher_fun;
using patcher_fun.Settings;
using patcher_fun.Languagues;

public class FileDownloader
{
    public static void UpdateFilesFromRemote()
    {
        string remoteDirectory = Remote.Files;
        string localDirectory = ".";

        try
        {
            string indexFile = Path.GetTempFileName();
            DownloadFile(Remote.FilesList, indexFile);

            try
            {
                string[] remoteFileNames = File.ReadAllLines(indexFile);
                File.Delete(indexFile);

                foreach (var fileName in remoteFileNames)
                {
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        continue; // Skip to next line if value is null or blank
                    }

                    string localPath = Path.Combine(localDirectory, fileName);
                    string remotePath = remoteDirectory + fileName;

                    // Create subdirectories if necessary
                    string localDir = Path.GetDirectoryName(localPath);
                    if (!Directory.Exists(localDir))
                    {
                        Directory.CreateDirectory(localDir);
                    }

                    if (File.Exists(localPath))
                    {
                        string localHash = CalculateFileHash(localPath);
                        string tempFile = Path.GetTempFileName();
                        DownloadFile(remotePath, tempFile);

                        string remoteHash = CalculateFileHash(tempFile);

                        if (localHash != remoteHash)
                        {
                            File.Copy(tempFile, localPath, true);
                            // MessageBox.Show($"File {fileName} was updated.");
                            Form1.Instance.SetLabelText($"{fileName} " + Text.FileUpdated);
                        }
                        else
                        {
                            //MessageBox.Show($"File {fileName} is identical, it has not been updated.");
                            Form1.Instance.SetLabelText($"{fileName} " + Text.FileDontNeedUpdate);
                        }

                        File.Delete(tempFile);
                    }
                    else
                    {
                        DownloadFile(remotePath, localPath);
                        //MessageBox.Show($"File {fileName} " + Text.FileDownload);
                        Form1.Instance.SetLabelText($"{fileName} " + Text.FileDownload);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"FileNotFoundException occurred while reading index file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading index file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (WebException ex)
        {
            MessageBox.Show($"WebException occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static string CalculateFileHash(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        {
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }
    }

    private static void DownloadFile(string remotePath, string localPath)
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(remotePath, localPath);
            }
        }
        catch (WebException ex)
        {
            MessageBox.Show($"WebException occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
