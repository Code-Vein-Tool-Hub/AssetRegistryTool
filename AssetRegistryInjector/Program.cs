using Newtonsoft.Json;
using QueenIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetRegistryInjector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0].ToLower() == "-help" || args[0].ToLower() == "-h")
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("AssetRegistryInjector.exe -inject [AssetRegistry.bin] [InputFolder]   \n  -Injects the json files in the Input folder into the given registry.\n");
                Console.WriteLine("AssetRegistryInjector.exe -dump [AssetRegistry.bin]                   \n  -Dumps each entry in the given registry into json files.\n");
                Console.WriteLine("AssetRegistryInjector.exe [AssetRegistry.bin/AssetRegistry.json]      \n  -Converts the given registry between bin and json.");
                return;
            }

            //-inject AssetRegistry [Folder with things to inject]
            if (args[0].ToLower() == "-inject")
            {
                AssetRegistry assetRegistry = new AssetRegistry();
                assetRegistry.Read(File.ReadAllBytes(args[1]));

                string[] files = Directory.GetFiles(args[2], "*.json");

                foreach (string file in files)
                {
                    AssetRegistry.FAssetData fAssetData = JsonConvert.DeserializeObject<AssetRegistry.FAssetData>(File.ReadAllText(file));
                    assetRegistry.fAssetDatas.Add(fAssetData);
                }

                File.WriteAllBytes("AssetRegistry_out.bin", assetRegistry.Make());
                return;
            }

            if (args[0].ToLower() == "-dump")
            {
                AssetRegistry assetRegistry = new AssetRegistry();
                assetRegistry.Read(File.ReadAllBytes(args[1]));

                Console.WriteLine($"Dummping {assetRegistry.fAssetDatas.Count()} assets to jsons...");

                if (!Directory.Exists(Path.GetFileName(args[1])))
                    Directory.CreateDirectory(Path.GetFileNameWithoutExtension(args[1]));

                foreach (AssetRegistry.FAssetData fAssetData in assetRegistry.fAssetDatas)
                {
                    string json = JsonConvert.SerializeObject(fAssetData, Formatting.Indented);
                    File.WriteAllText($"{Path.GetFileNameWithoutExtension(args[1])}\\{fAssetData.ToString()}.json", json);
                }

                Console.WriteLine($"Done");
                return;
            }

            if (Path.GetExtension(args[0]) == ".bin")
            {
                AssetRegistry assetRegistry = new AssetRegistry();
                assetRegistry.Read(File.ReadAllBytes(args[0]));

                string json = JsonConvert.SerializeObject(assetRegistry, Formatting.Indented);
                File.WriteAllText(Path.ChangeExtension(args[0], ".json"), json);
            }
            else if (Path.GetExtension(args[0]) == ".json")
            {
                AssetRegistry assetRegistry = new AssetRegistry();
                assetRegistry = JsonConvert.DeserializeObject<AssetRegistry>(File.ReadAllText(args[0]));

                byte[] file = assetRegistry.Make();
                File.WriteAllBytes(Path.ChangeExtension(args[0], ".bin"), file);
            }
        }


    }
}
