# AssetRegistryTool
 A tool for editing the Asset Registry in Unreal games
Uses [QueenIO](https://github.com/Code-Vein-Tool-Hub/QueenIO) to handle AssetRegistry file.

### Usage
Converting - Converts the given registry between bin and json.  
`AssetRegistryInjector.exe [AssetRegistry.bin/AssetRegistry.json]`  

Dummping - Dumps each entry in the given registry into json files.  
`AssetRegistryInjector.exe -dump [AssetRegistry.bin]`

Injecting - Injects the json files in the Input folder into the given registry.  
`AssetRegistryInjector.exe -inject [AssetRegistry.bin] [InputFolder]`

### Credits
atenfyr - [UAssetAPI](https://github.com/atenfyr/UAssetAPI)
