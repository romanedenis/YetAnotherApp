Le développement asynchrone en C# permet d'exécuter des opérations qui prennent du temps sans bloquer le thread principal de l'application. Par exemple, si on veut télécharger un fichier depuis un serveur, on peut utiliser une méthode asynchrone qui va lancer le téléchargement sur un thread secondaire et retourner immédiatement le contrôle au thread principal. Le thread principal peut alors continuer à exécuter d'autres tâches pendant que le téléchargement se fait en arrière-plan. Quand le téléchargement est terminé, on peut utiliser un mot-clé await pour attendre le résultat de la méthode asynchrone et continuer le traitement.

Voici un exemple de code en C# qui illustre le développement asynchrone:


using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExempleAsynchrone
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Créer un client HTTP pour envoyer des requêtes au serveur
            HttpClient client = new HttpClient();

            // Définir l'URL du fichier à télécharger
            string url = "https://example.com/file.zip";

            // Appeler la méthode asynchrone DownloadFileAsync pour lancer le téléchargement
            Task<byte[]> downloadTask = DownloadFileAsync(client, url);

            // Afficher un message pendant que le téléchargement se fait en arrière-plan
            Console.WriteLine("Le téléchargement est en cours...");

            // Attendre que la méthode asynchrone se termine et récupérer le résultat
            byte[] fileContent = await downloadTask;

            // Afficher la taille du fichier téléchargé
            Console.WriteLine($"Le fichier fait {fileContent.Length} octets.");

            // Écrire le contenu du fichier dans un fichier local
            File.WriteAllBytes("file.zip", fileContent);

            // Afficher un message de confirmation
            Console.WriteLine("Le fichier a été enregistré.");
        }

        // Définir une méthode asynchrone qui prend un client HTTP et une URL en paramètres et qui retourne un tableau d'octets
        static async Task<byte[]> DownloadFileAsync(HttpClient client, string url)
        {
            // Envoyer une requête GET au serveur et attendre la réponse
            HttpResponseMessage response = await client.GetAsync(url);

            // Vérifier que la réponse est valide (code 200)
            response.EnsureSuccessStatusCode();

            // Lire le contenu de la réponse et le convertir en tableau d'octets
            byte[] content = await response.Content.ReadAsByteArrayAsync();

            // Retourner le tableau d'octets
            return content;
        }
    }
}
