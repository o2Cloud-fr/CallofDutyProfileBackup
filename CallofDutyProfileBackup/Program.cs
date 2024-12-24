using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace CallofDutyProfileBackup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Application de sauvegarde du profil Call of Duty");
                Console.WriteLine("---------------------------------------------");

                // Chemin du dossier du profil Call of Duty
                string userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
                string codProfilePath = Path.Combine(userProfile, "Documents", "Call of Duty");

                // Vérifier si le dossier existe
                if (!Directory.Exists(codProfilePath))
                {
                    throw new DirectoryNotFoundException("Le dossier du profil Call of Duty n'a pas été trouvé.");
                }

                // Demander le nom du fichier de sauvegarde
                Console.Write("\nEntrez le nom du fichier de sauvegarde (sans .zip) : ");
                string backupName = Console.ReadLine().Trim();

                // Créer le chemin complet du fichier zip sur le bureau
                string desktopPath = Path.Combine(userProfile, "Desktop");
                string zipPath = Path.Combine(desktopPath, $"{backupName}.zip");

                // Vérifier si le fichier existe déjà
                if (File.Exists(zipPath))
                {
                    Console.Write("\nLe fichier existe déjà. Voulez-vous le remplacer ? (O/N) : ");
                    if (Console.ReadLine().Trim().ToUpper() != "O")
                    {
                        Console.WriteLine("Opération annulée.");
                        return;
                    }
                    File.Delete(zipPath);
                }

                Console.WriteLine("\nCréation de la sauvegarde en cours...");

                // Créer le fichier ZIP
                ZipFile.CreateFromDirectory(codProfilePath, zipPath,
                                         CompressionLevel.Optimal, false);

                Console.WriteLine($"\nSauvegarde terminée avec succès !");
                Console.WriteLine($"Fichier créé : {zipPath}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"\nErreur : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUne erreur est survenue : {ex.Message}");
            }

            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }
}